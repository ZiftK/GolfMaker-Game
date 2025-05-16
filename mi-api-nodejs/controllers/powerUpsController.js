// controllers/powerUpsController.js
const dbPool = require('../config/db');

// --- OBTENER TODOS los PowerUps ---
exports.getAllPowerUps = async (req, res) => {
    try {
        console.log('CONTROLLER: Petición GET /powerups');
        // Ordenamos por nombre para consistencia
        const query = 'SELECT * FROM PowerUps ORDER BY nombre ASC';
        const [powerups] = await dbPool.query(query);
        console.log(`CONTROLLER: Consulta ejecutada, ${powerups.length} powerups encontrados.`);
        res.json(powerups);
    } catch (error) {
        console.error('Error en CONTROLLER GET /powerups:', error);
        res.status(500).json({ message: 'Error al obtener la lista de power-ups.' });
    }
};

// --- OBTENER UN PowerUp por ID ---
exports.getPowerUpById = async (req, res) => {
    // Usamos :idPowerUp para ser más específicos que solo :id
    const { idPowerUp } = req.params;
    console.log(`CONTROLLER: Petición GET /powerups/${idPowerUp}`);
    try {
        const query = 'SELECT * FROM PowerUps WHERE id_powerup = ?';
        const [rows] = await dbPool.query(query, [idPowerUp]);

        if (rows.length > 0) {
            console.log(`CONTROLLER: PowerUp con ID ${idPowerUp} encontrado.`);
            res.json(rows[0]); // Devolver el power-up encontrado
        } else {
            console.log(`CONTROLLER: PowerUp con ID ${idPowerUp} no encontrado.`);
            res.status(404).json({ message: 'Power-up no encontrado' });
        }
    } catch (error) {
        console.error(`Error en CONTROLLER GET /powerups/${idPowerUp}:`, error);
        res.status(500).json({ message: 'Error al obtener el power-up.' });
    }
};

// --- CREAR un nuevo PowerUp ---
exports.createPowerUp = async (req, res) => {
    // Extraemos todos los campos posibles del body
    const { nombre, descripcion, duracion, cantidad_uso, efecto, desbloqueo } = req.body;
    console.log('CONTROLLER: Petición POST /powerups con datos:', req.body);

    // Validación de campos obligatorios (según tu schema)
    if (!nombre || !efecto) {
        return res.status(400).json({ message: 'Faltan campos obligatorios (nombre, efecto)' });
    }

    // Validación del ENUM para 'efecto'
    const efectosPermitidos = ['rebote_extra', 'vuelo', 'freno', 'aceleracion'];
    if (!efectosPermitidos.includes(efecto)) {
         return res.status(400).json({ message: `Valor de 'efecto' inválido. Permitidos: ${efectosPermitidos.join(', ')}` });
    }

    try {
        // Asignar valores por defecto de la BD si no vienen en la petición
        const desc = descripcion !== undefined ? descripcion : null;
        const dur = duracion !== undefined ? duracion : 0;
        const cant = cantidad_uso !== undefined ? cantidad_uso : 1;
        const desbl = desbloqueo !== undefined ? desbloqueo : null;

        const query = `INSERT INTO PowerUps
                         (nombre, descripcion, duracion, cantidad_uso, efecto, desbloqueo)
                       VALUES (?, ?, ?, ?, ?, ?)`;
        const [result] = await dbPool.query(query, [nombre, desc, dur, cant, efecto, desbl]);

        console.log(`CONTROLLER: PowerUp creado con ID: ${result.insertId}`);
        res.status(201).json({
            id_powerup: result.insertId,
            message: 'PowerUp creado exitosamente'
        });

    } catch (error) {
         // Podría haber un error si intentas insertar un 'nombre' duplicado y tienes una restricción UNIQUE
        console.error('CONTROLLER Error en POST /powerups:', error);
         if (error.code === 'ER_DUP_ENTRY') {
             return res.status(409).json({ message: 'Ya existe un PowerUp con ese nombre.' });
         }
        res.status(500).json({ message: 'Error al crear el PowerUp.' });
    }
};

// --- ACTUALIZAR un PowerUp existente ---
exports.updatePowerUp = async (req, res) => {
    const { idPowerUp } = req.params;
    const { nombre, descripcion, duracion, cantidad_uso, efecto, desbloqueo } = req.body;
    console.log(`CONTROLLER: Petición PUT /powerups/${idPowerUp} con datos:`, req.body);

     // Validación del ENUM si se proporciona 'efecto'
     if (efecto !== undefined && !['rebote_extra', 'vuelo', 'freno', 'aceleracion'].includes(efecto)) {
          return res.status(400).json({ message: `Valor de 'efecto' inválido.` });
     }
     // Validación básica: al menos un campo debe venir para actualizar
     if (nombre === undefined && descripcion === undefined && duracion === undefined && cantidad_uso === undefined && efecto === undefined && desbloqueo === undefined) {
         return res.status(400).json({ message: 'No se proporcionaron datos para actualizar.' });
     }

    try {
        // Usamos COALESCE para permitir actualizaciones parciales
        const query = `UPDATE PowerUps SET
                         nombre = COALESCE(?, nombre),
                         descripcion = COALESCE(?, descripcion),
                         duracion = COALESCE(?, duracion),
                         cantidad_uso = COALESCE(?, cantidad_uso),
                         efecto = COALESCE(?, efecto),
                         desbloqueo = COALESCE(?, desbloqueo)
                       WHERE id_powerup = ?`;
        const [result] = await dbPool.query(query, [
            nombre, descripcion, duracion, cantidad_uso, efecto, desbloqueo,
            idPowerUp
        ]);

        if (result.affectedRows > 0) {
            console.log(`CONTROLLER: PowerUp con ID ${idPowerUp} actualizado.`);
            res.json({ message: 'PowerUp actualizado exitosamente' });
        } else {
            console.log(`CONTROLLER: PowerUp con ID ${idPowerUp} no encontrado para actualizar.`);
            res.status(404).json({ message: 'PowerUp no encontrado para actualizar' });
        }
    } catch (error) {
        console.error(`CONTROLLER Error en PUT /powerups/${idPowerUp}:`, error);
         if (error.code === 'ER_DUP_ENTRY' && error.message.includes('nombre')) { // Asumiendo que 'nombre' es UNIQUE
             return res.status(409).json({ message: 'Ya existe otro PowerUp con ese nombre.' });
         }
        res.status(500).json({ message: 'Error al actualizar el PowerUp.' });
    }
};

// --- ELIMINAR un PowerUp ---
exports.deletePowerUp = async (req, res) => {
    const { idPowerUp } = req.params;
    console.log(`CONTROLLER: Petición DELETE /powerups/${idPowerUp}`);

    try {
         // La FK en Usuario_PowerUp tiene ON DELETE CASCADE, así que MySQL
         // debería borrar automáticamente las entradas relacionadas allí.
        const query = 'DELETE FROM PowerUps WHERE id_powerup = ?';
        const [result] = await dbPool.query(query, [idPowerUp]);

        if (result.affectedRows > 0) {
            console.log(`CONTROLLER: PowerUp con ID ${idPowerUp} eliminado.`);
            res.status(200).json({ message: 'PowerUp eliminado exitosamente' });
        } else {
            console.log(`CONTROLLER: PowerUp con ID ${idPowerUp} no encontrado para eliminar.`);
            res.status(404).json({ message: 'PowerUp no encontrado para eliminar' });
        }
    } catch (error) {
        console.error(`CONTROLLER Error en DELETE /powerups/${idPowerUp}:`, error);
        res.status(500).json({ message: 'Error al eliminar el PowerUp.' });
    }
};