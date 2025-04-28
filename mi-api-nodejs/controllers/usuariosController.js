// controllers/usuariosController.js
const dbPool = require('../config/db'); // Importamos el pool desde config
const bcrypt = require('bcrypt');       // Importamos bcrypt

// Función para OBTENER TODOS los usuarios
exports.getAllUsuarios = async (req, res) => {
    try {
        console.log('CONTROLLER: Petición GET /usuarios');
        const query = 'SELECT id_usuario, nombre_usuario, email, puntuacion_total, monedas, fecha_registro FROM Usuarios ORDER BY fecha_registro DESC';
        const [usuarios] = await dbPool.query(query);
        console.log(`CONTROLLER: Consulta ejecutada, ${usuarios.length} usuarios encontrados.`);
        res.json(usuarios);
    } catch (error) {
        console.error('Error en CONTROLLER GET /usuarios:', error);
        res.status(500).json({ message: 'Error al obtener la lista de usuarios.' });
    }
};

// Función para OBTENER UN usuario por ID
exports.getUsuarioById = async (req, res) => {
    const userId = req.params.id;
    console.log(`CONTROLLER: Petición GET /usuarios/${userId}`);
    try {
        const query = 'SELECT id_usuario, nombre_usuario, email, puntuacion_total, monedas, fecha_registro FROM Usuarios WHERE id_usuario = ?';
        const [rows] = await dbPool.query(query, [userId]);

        if (rows.length > 0) {
            console.log(`CONTROLLER: Usuario con ID ${userId} encontrado.`);
            res.json(rows[0]);
        } else {
            console.log(`CONTROLLER: Usuario con ID ${userId} no encontrado.`);
            res.status(404).json({ message: 'Usuario no encontrado' });
        }
    } catch (error) {
        console.error(`Error en CONTROLLER GET /usuarios/${userId}:`, error);
        res.status(500).json({ message: 'Error al obtener el usuario.' });
    }
};

// Función para CREAR un nuevo usuario
exports.createUsuario = async (req, res) => {
    const { nombre_usuario, email, contraseña } = req.body;
    console.log('CONTROLLER: Petición POST /usuarios con datos:', req.body);

    if (!nombre_usuario || !email || !contraseña) {
        return res.status(400).json({ message: 'Faltan campos obligatorios (nombre_usuario, email, contraseña)' });
    }

    try {
        const saltRounds = 10;
        const hashedPassword = await bcrypt.hash(contraseña, saltRounds);
        console.log('CONTROLLER: Contraseña hasheada.');

        const query = 'INSERT INTO Usuarios (nombre_usuario, email, contraseña) VALUES (?, ?, ?)';
        const [result] = await dbPool.query(query, [nombre_usuario, email, hashedPassword]);

        console.log(`CONTROLLER: Usuario creado con ID: ${result.insertId}`);
        res.status(201).json({
            id_usuario: result.insertId,
            nombre_usuario: nombre_usuario,
            email: email,
            message: 'Usuario creado exitosamente'
        });

    } catch (error) {
        if (error.code === 'ER_DUP_ENTRY') {
            console.error('CONTROLLER Error en POST /usuarios: Email ya existe.', error.message);
            return res.status(409).json({ message: 'El email proporcionado ya está registrado.' });
        }
        console.error('CONTROLLER Error en POST /usuarios:', error);
        res.status(500).json({ message: 'Error al crear el usuario.' });
    }
};

// Función para ACTUALIZAR un usuario
exports.updateUsuario = async (req, res) => {
    const userId = req.params.id;
    const { nombre_usuario, email, monedas, puntuacion_total, niveles_creados, niveles_completados } = req.body;
    console.log(`CONTROLLER: Petición PUT /usuarios/${userId} con datos:`, req.body);

    if (!nombre_usuario || !email) {
        return res.status(400).json({ message: 'Faltan campos obligatorios (nombre_usuario, email)' });
    }

    try {
        const query = `UPDATE Usuarios SET
                       nombre_usuario = ?, email = ?, monedas = ?,
                       puntuacion_total = ?, niveles_creados = ?, niveles_completados = ?
                       WHERE id_usuario = ?`;
        const [result] = await dbPool.query(query, [
            nombre_usuario, email,
            monedas !== undefined ? monedas : 0,
            puntuacion_total !== undefined ? puntuacion_total : 0,
            niveles_creados !== undefined ? niveles_creados : 0,
            niveles_completados !== undefined ? niveles_completados : 0,
            userId
        ]);

        if (result.affectedRows > 0) {
            console.log(`CONTROLLER: Usuario con ID ${userId} actualizado.`);
            res.json({ message: 'Usuario actualizado exitosamente' });
        } else {
            console.log(`CONTROLLER: Usuario con ID ${userId} no encontrado para actualizar.`);
            res.status(404).json({ message: 'Usuario no encontrado para actualizar' });
        }
    } catch (error) {
        if (error.code === 'ER_DUP_ENTRY') {
            console.error(`CONTROLLER Error en PUT /usuarios/${userId}: Email ya existe.`, error.message);
            return res.status(409).json({ message: 'El email proporcionado ya está registrado por otro usuario.' });
        }
        console.error(`CONTROLLER Error en PUT /usuarios/${userId}:`, error);
        res.status(500).json({ message: 'Error al actualizar el usuario.' });
    }
};

// Función para ELIMINAR un usuario
exports.deleteUsuario = async (req, res) => {
    const userId = req.params.id;
    console.log(`CONTROLLER: Petición DELETE /usuarios/${userId}`);

    try {
        const query = 'DELETE FROM Usuarios WHERE id_usuario = ?';
        const [result] = await dbPool.query(query, [userId]);

        if (result.affectedRows > 0) {
            console.log(`CONTROLLER: Usuario con ID ${userId} eliminado.`);
            res.status(200).json({ message: 'Usuario eliminado exitosamente' });
        } else {
            console.log(`CONTROLLER: Usuario con ID ${userId} no encontrado para eliminar.`);
            res.status(404).json({ message: 'Usuario no encontrado para eliminar' });
        }
    } catch (error) {
        console.error(`CONTROLLER Error en DELETE /usuarios/${userId}:`, error);
        res.status(500).json({ message: 'Error al eliminar el usuario.' });
    }
};

// --- LOGIN de Usuario ---
exports.loginUsuario = async (req, res) => {
    const { email, contraseña } = req.body;
    console.log(`CONTROLLER: Petición POST /login con datos: { email: ${email} }`); // No loguear contraseña

    // Validación básica de entrada
    if (!email || !contraseña) {
        return res.status(400).json({ message: 'Faltan campos obligatorios (email, contraseña)' });
    }

    try {
        // 1. Buscar al usuario por email y OBTENER SU HASH DE CONTRASEÑA
        const query = 'SELECT id_usuario, nombre_usuario, email, contraseña FROM Usuarios WHERE email = ?';
        const [rows] = await dbPool.query(query, [email]);

        // 2. Verificar si el usuario existe
        if (rows.length === 0) {
            console.log(`CONTROLLER: Login fallido - Email no encontrado: ${email}`);
            // Nota: Devolvemos 401 genérico por seguridad, no decimos si falló el email o la pass.
            return res.status(401).json({ message: 'Credenciales inválidas' });
        }

        const usuario = rows[0];

        // 3. Comparar la contraseña enviada con el hash guardado en la BD
        console.log(`CONTROLLER: Comparando contraseña para usuario ${usuario.email}`);
        const match = await bcrypt.compare(contraseña, usuario.contraseña); // Compara texto plano con hash

        // 4. Verificar si la contraseña coincide
        if (!match) {
            console.log(`CONTROLLER: Login fallido - Contraseña incorrecta para ${usuario.email}`);
            // Nota: Devolvemos 401 genérico por seguridad.
            return res.status(401).json({ message: 'Credenciales inválidas' });
        }

        // 5. ¡Login Exitoso!
        console.log(`CONTROLLER: Login exitoso para usuario ${usuario.email} (ID: ${usuario.id_usuario})`);

        // **Próximo Paso (Importante):** Aquí es donde generarías un Token JWT (JSON Web Token)
        // y lo enviarías de vuelta al cliente. El cliente usaría ese token
        // para autenticarse en las siguientes peticiones a rutas protegidas.
        // Por AHORA, solo devolvemos un mensaje de éxito y datos básicos del usuario.

        res.status(200).json({
            message: 'Login exitoso',
            usuario: { // No devolver NUNCA la contraseña hasheada
                id_usuario: usuario.id_usuario,
                nombre_usuario: usuario.nombre_usuario,
                email: usuario.email
            }
            // token: 'aqui_iria_el_jwt_generado' // <-- Futuro paso
        });

    } catch (error) {
        console.error('CONTROLLER Error en POST /login:', error);
        res.status(500).json({ message: 'Error interno del servidor durante el login.' });
    }
};

// --- OBTENER los PowerUps de un Usuario Específico ---
exports.getUsuarioPowerUps = async (req, res) => {
    const { idUsuario } = req.params;
    console.log(`CONTROLLER: Petición GET /usuarios/${idUsuario}/powerups`);

    try {
        // Hacemos JOIN para obtener detalles del PowerUp y la cantidad disponible
        const query = `
            SELECT
                p.id_powerup,
                p.nombre,
                p.descripcion,
                p.duracion,
                p.cantidad_uso,
                p.efecto,
                p.desbloqueo,
                up.cantidad_disponible
            FROM Usuario_PowerUp up
            JOIN PowerUps p ON up.id_powerup = p.id_powerup
            WHERE up.id_usuario = ?
            ORDER BY p.nombre ASC;
        `;
        const [powerups] = await dbPool.query(query, [idUsuario]);

        console.log(`CONTROLLER: Consulta ejecutada, ${powerups.length} powerups encontrados para usuario ${idUsuario}.`);
        res.json(powerups); // Devolvemos el array de powerups que tiene el usuario

    } catch (error) {
        console.error(`Error en CONTROLLER GET /usuarios/${idUsuario}/powerups:`, error);
        res.status(500).json({ message: 'Error al obtener los power-ups del usuario.' });
    }
};

// --- AÑADIR/ASIGNAR un PowerUp a un Usuario (o incrementar cantidad) ---
exports.addPowerUpToUsuario = async (req, res) => {
    const { idUsuario } = req.params;
    // Esperamos el ID del powerup y opcionalmente la cantidad a añadir/establecer
    const { id_powerup, cantidad } = req.body;
    console.log(`CONTROLLER: Petición POST /usuarios/${idUsuario}/powerups con datos:`, req.body);

    // Validación básica
    if (id_powerup === undefined) {
        return res.status(400).json({ message: 'Falta el campo obligatorio: id_powerup' });
    }
    // Si no viene cantidad, usamos el default de la tabla (1) para el INSERT,
    // y si ya existe, quizás queramos solo añadir 1 (o no hacer nada si no se especifica cantidad?)
    // Vamos a asumir que si no viene cantidad, se añade/establece en 1.
    // Si viene cantidad, se usa esa cantidad.
    const cantidadInicial = cantidad !== undefined ? parseInt(cantidad, 10) : 1;
     if (isNaN(cantidadInicial) || cantidadInicial <= 0) {
         return res.status(400).json({ message: 'La cantidad debe ser un número positivo.' });
     }

    try {
        // Usamos INSERT ... ON DUPLICATE KEY UPDATE para manejar ambos casos:
        // 1. Si el usuario NO tiene el powerup -> lo inserta con la cantidad especificada (o 1).
        // 2. Si el usuario YA tiene el powerup -> actualiza la cantidad (la incrementa por ahora).
        // NOTA: La tabla debe tener PRIMARY KEY (id_usuario, id_powerup) para que esto funcione. ¡Ya la tiene!
        const query = `
            INSERT INTO Usuario_PowerUp (id_usuario, id_powerup, cantidad_disponible)
            VALUES (?, ?, ?)
            ON DUPLICATE KEY UPDATE cantidad_disponible = cantidad_disponible + VALUES(cantidad_disponible);
        `;
         // VALUES(cantidad_disponible) se refiere al valor que intentamos insertar (cantidadInicial)

        const [result] = await dbPool.query(query, [idUsuario, id_powerup, cantidadInicial]);

        console.log(`CONTROLLER: Relación Usuario-PowerUp afectada para Usuario ${idUsuario} y PowerUp ${id_powerup}. AffectedRows: ${result.affectedRows}, InsertId: ${result.insertId}`);

        // affectedRows será 1 si se insertó, 2 si se actualizó (en MySQL > 5.1), 0 si no cambió.
        if (result.affectedRows > 0) {
             // Si insertId es > 0, fue una inserción nueva. Si es 0 y affectedRows es > 0, fue una actualización.
            const actionMessage = result.insertId !== 0 ? 'asignado' : 'actualizado (cantidad incrementada)';
             res.status(result.insertId !== 0 ? 201 : 200).json({
                message: `PowerUp ${actionMessage} al usuario exitosamente.`
             });
        } else {
             // Esto podría pasar si la cantidad a sumar es 0 y la fila ya existía.
             res.status(200).json({ message: 'La cantidad del PowerUp no cambió.' });
        }


    } catch (error) {
        // Manejar error si idUsuario o idPowerup no existen en sus tablas respectivas
        if (error.code === 'ER_NO_REFERENCED_ROW_2') {
             console.error(`CONTROLLER Error en POST /usuarios/${idUsuario}/powerups: ID de Usuario o PowerUp no existe.`, error.message);
             return res.status(404).json({ message: 'Error al asignar el power-up: El usuario o el power-up especificado no existe.' });
        }
        console.error(`CONTROLLER Error en POST /usuarios/${idUsuario}/powerups:`, error);
        res.status(500).json({ message: 'Error al asignar el power-up al usuario.' });
    }
};

// --- USAR un PowerUp (Decrementar Cantidad) ---
exports.usePowerUp = async (req, res) => {
    const { idUsuario, idPowerup } = req.params; // Obtener IDs de la URL
    console.log(`CONTROLLER: Petición POST /usuarios/<span class="math-inline">\{idUsuario\}/powerups/</span>{idPowerup}/use`);

    // **NOTA DE SEGURIDAD:** Idealmente, verificaríamos que el idUsuario que hace la petición
    // (del token JWT) coincide con el idUsuario en la URL. Omitimos por ahora.

    let connection; // Definimos connection fuera del try para usarla en finally si es necesario
    try {
        connection = await dbPool.getConnection(); // Obtenemos una conexión para transacción
        await connection.beginTransaction(); // Iniciamos transacción

        // 1. Verificar si el usuario tiene el power-up y con cantidad > 0
        const checkQuery = 'SELECT cantidad_disponible FROM Usuario_PowerUp WHERE id_usuario = ? AND id_powerup = ? FOR UPDATE'; // FOR UPDATE bloquea la fila
        const [rows] = await connection.query(checkQuery, [idUsuario, idPowerup]);

        if (rows.length === 0) {
            await connection.rollback(); // Revertir transacción
            console.log(`CONTROLLER: Usuario ${idUsuario} no posee PowerUp ${idPowerup}.`);
            return res.status(404).json({ message: 'El usuario no posee este power-up.' });
        }

        const cantidadActual = rows[0].cantidad_disponible;

        if (cantidadActual <= 0) {
            await connection.rollback(); // Revertir transacción
            console.log(`CONTROLLER: Usuario ${idUsuario} no tiene usos disponibles de PowerUp ${idPowerup} (Cantidad: ${cantidadActual}).`);
            return res.status(400).json({ message: 'No quedan usos disponibles para este power-up.' });
        }

        // 2. Decrementar la cantidad
        const updateQuery = 'UPDATE Usuario_PowerUp SET cantidad_disponible = cantidad_disponible - 1 WHERE id_usuario = ? AND id_powerup = ?';
        const [result] = await connection.query(updateQuery, [idUsuario, idPowerup]);

        if (result.affectedRows > 0) {
            await connection.commit(); // Confirmar transacción
            const nuevaCantidad = cantidadActual - 1;
            console.log(`CONTROLLER: PowerUp ${idPowerup} usado por Usuario ${idUsuario}. Nueva cantidad: ${nuevaCantidad}`);
            res.status(200).json({
                message: 'Power-up utilizado exitosamente.',
                cantidad_restante: nuevaCantidad
            });
        } else {
            // Esto no debería pasar si la fila se bloqueó bien, pero es una salvaguarda
            await connection.rollback(); // Revertir transacción
            console.error(`CONTROLLER: No se pudo actualizar la cantidad para Usuario ${idUsuario}, PowerUp ${idPowerup}`);
            res.status(500).json({ message: 'Error al intentar usar el power-up.' });
        }

    } catch (error) {
        if (connection) await connection.rollback(); // Revertir transacción en caso de error
        console.error(`CONTROLLER Error en POST /usuarios/<span class="math-inline">\{idUsuario\}/powerups/</span>{idPowerup}/use:`, error);
        res.status(500).json({ message: 'Error interno del servidor al usar el power-up.' });
    } finally {
         if (connection) connection.release(); // Liberar siempre la conexión al pool
    }
};