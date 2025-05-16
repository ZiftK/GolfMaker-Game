// controllers/bloquesController.js
const dbPool = require('../config/db');

// --- OBTENER Bloques por ID de Nivel ---
exports.getBloquesByNivelId = async (req, res) => {
    // Obtenemos el ID del nivel desde los parámetros de la URL
    const { idNivel } = req.params;
    console.log(`CONTROLLER: Petición GET /niveles/${idNivel}/bloques`);

    try {
        const query = 'SELECT * FROM Bloques WHERE id_nivel = ? ORDER BY id_bloque ASC';
        const [bloques] = await dbPool.query(query, [idNivel]);

        console.log(`CONTROLLER: Consulta ejecutada, ${bloques.length} bloques encontrados para nivel ${idNivel}.`);
        // Devolvemos siempre un array, aunque esté vacío
        res.json(bloques);

    } catch (error) {
        console.error(`Error en CONTROLLER GET /niveles/${idNivel}/bloques:`, error);
        res.status(500).json({ message: 'Error al obtener los bloques del nivel.' });
    }
};

// --- CREAR un Bloque para un Nivel Específico ---
exports.createBloqueForNivel = async (req, res) => {
    // Obtenemos el ID del nivel desde los parámetros de la URL
    const { idNivel } = req.params;
    // Obtenemos los datos del bloque desde el cuerpo de la petición
    const { tipo, pos_x, pos_y, propiedades, friccion, rebote } = req.body;
    console.log(`CONTROLLER: Petición POST /niveles/${idNivel}/bloques con datos:`, req.body);

    // Validación básica (tipo, pos_x, pos_y son obligatorios según el schema)
    if (tipo === undefined || pos_x === undefined || pos_y === undefined) {
        return res.status(400).json({ message: 'Faltan campos obligatorios (tipo, pos_x, pos_y)' });
    }
    // Validación del ENUM (básica)
    const tiposPermitidos = ['agua', 'arena', 'césped', 'piedra', 'madera'];
    if (!tiposPermitidos.includes(tipo)) {
         return res.status(400).json({ message: `Valor de 'tipo' inválido. Permitidos: ${tiposPermitidos.join(', ')}` });
    }


    try {
        // Asignar valores por defecto si no vienen o null si aplica
        const props = propiedades !== undefined ? propiedades : null;
        const fric = friccion !== undefined ? friccion : 1.0;
        const rebot = rebote !== undefined ? rebote : 1.0;

        const query = `INSERT INTO Bloques
                         (id_nivel, tipo, pos_x, pos_y, propiedades, friccion, rebote)
                       VALUES (?, ?, ?, ?, ?, ?, ?)`;
        const [result] = await dbPool.query(query, [idNivel, tipo, pos_x, pos_y, props, fric, rebot]);

        console.log(`CONTROLLER: Bloque creado con ID: ${result.insertId} para nivel ${idNivel}`);
        res.status(201).json({
            id_bloque: result.insertId,
            message: 'Bloque creado exitosamente'
        });

    } catch (error) {
        // Podría fallar si idNivel no existe (Foreign Key constraint)
        console.error(`CONTROLLER Error en POST /niveles/${idNivel}/bloques:`, error);
         if (error.code === 'ER_NO_REFERENCED_ROW_2') {
             return res.status(404).json({ message: `Error al crear el bloque: El nivel con ID ${idNivel} no existe.` });
        }
        res.status(500).json({ message: 'Error al crear el bloque.' });
    }
};

// --- ACTUALIZAR un Bloque específico por su ID ---
exports.updateBloque = async (req, res) => {
    const { idBloque } = req.params; // Obtenemos id_bloque de la URL
    // Obtenemos los campos que se pueden actualizar del cuerpo
    const { tipo, pos_x, pos_y, propiedades, friccion, rebote } = req.body;
    console.log(`CONTROLLER: Petición PUT /bloques/${idBloque} con datos:`, req.body);

    // Validación básica (al menos debe venir algo para actualizar)
    if (tipo === undefined && pos_x === undefined && pos_y === undefined && propiedades === undefined && friccion === undefined && rebote === undefined) {
        return res.status(400).json({ message: 'No se proporcionaron datos para actualizar.' });
    }
     // Validación del ENUM si se proporciona 'tipo'
     if (tipo !== undefined) {
        const tiposPermitidos = ['agua', 'arena', 'césped', 'piedra', 'madera'];
        if (!tiposPermitidos.includes(tipo)) {
             return res.status(400).json({ message: `Valor de 'tipo' inválido. Permitidos: ${tiposPermitidos.join(', ')}` });
        }
     }

    // Construir la consulta dinámicamente es más complejo.
    // Por simplicidad ahora, actualizaremos los campos que lleguen.
    // ¡CUIDADO! Esto requiere enviar todos los campos o la lógica sería más compleja.
    // Una mejor aproximación usaría PATCH o construiría el SET dinámicamente.
    // Por ahora, asumimos que se envían los campos necesarios para la actualización.
     if (tipo === undefined || pos_x === undefined || pos_y === undefined ) {
         // Podríamos permitir actualizaciones parciales, pero por ahora pedimos estos
         // return res.status(400).json({ message: 'Faltan campos clave para actualizar (tipo, pos_x, pos_y)' });
         // Vamos a permitir actualizacion parcial simple por ahora
         console.warn("Advertencia: Actualización parcial de bloque. Asegúrate de que la lógica sea la deseada.");
     }


    try {
        // Construimos la parte SET de la consulta (simplificado)
        // Una versión más robusta verificaría qué campos vienen en req.body
        const query = `UPDATE Bloques SET
                       tipo = COALESCE(?, tipo),
                       pos_x = COALESCE(?, pos_x),
                       pos_y = COALESCE(?, pos_y),
                       propiedades = COALESCE(?, propiedades),
                       friccion = COALESCE(?, friccion),
                       rebote = COALESCE(?, rebote)
                       WHERE id_bloque = ?`;
        // COALESCE usa el nuevo valor si no es NULL, sino mantiene el valor existente
        const [result] = await dbPool.query(query, [
            tipo, pos_x, pos_y,
            propiedades, friccion, rebote,
            idBloque
        ]);

        if (result.affectedRows > 0) {
            console.log(`CONTROLLER: Bloque con ID ${idBloque} actualizado.`);
            // Podríamos devolver el bloque actualizado si quisiéramos
            res.json({ message: 'Bloque actualizado exitosamente' });
        } else {
            console.log(`CONTROLLER: Bloque con ID ${idBloque} no encontrado para actualizar.`);
            res.status(404).json({ message: 'Bloque no encontrado para actualizar' });
        }
    } catch (error) {
         // Podría haber un error si el tipo ENUM es inválido en la actualización
        console.error(`CONTROLLER Error en PUT /bloques/${idBloque}:`, error);
        res.status(500).json({ message: 'Error al actualizar el bloque.' });
    }
};

// --- ELIMINAR un Bloque específico por su ID ---
exports.deleteBloque = async (req, res) => {
    const { idBloque } = req.params;
    console.log(`CONTROLLER: Petición DELETE /bloques/${idBloque}`);

    try {
        const query = 'DELETE FROM Bloques WHERE id_bloque = ?';
        const [result] = await dbPool.query(query, [idBloque]);

        if (result.affectedRows > 0) {
            console.log(`CONTROLLER: Bloque con ID ${idBloque} eliminado.`);
            res.status(200).json({ message: 'Bloque eliminado exitosamente' });
        } else {
            console.log(`CONTROLLER: Bloque con ID ${idBloque} no encontrado para eliminar.`);
            res.status(404).json({ message: 'Bloque no encontrado para eliminar' });
        }
    } catch (error) {
        console.error(`CONTROLLER Error en DELETE /bloques/${idBloque}:`, error);
        res.status(500).json({ message: 'Error al eliminar el bloque.' });
    }
};