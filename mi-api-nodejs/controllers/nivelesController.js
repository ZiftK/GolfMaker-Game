// controllers/nivelesController.js
const dbPool = require('../config/db');

// --- OBTENER TODOS los niveles ---
exports.getAllNiveles = async (req, res) => {
    try {
        console.log('CONTROLLER: Petición GET /niveles');
        // Consulta básica, podríamos añadir JOIN con Usuarios para ver el creador después
        const query = 'SELECT * FROM Niveles ORDER BY fecha_creacion DESC';
        const [niveles] = await dbPool.query(query);
        console.log(`CONTROLLER: Consulta ejecutada, ${niveles.length} niveles encontrados.`);
        res.json(niveles);
    } catch (error) {
        console.error('Error en CONTROLLER GET /niveles:', error);
        res.status(500).json({ message: 'Error al obtener la lista de niveles.' });
    }
};

// --- OBTENER UN nivel por ID ---
exports.getNivelById = async (req, res) => {
    const nivelId = req.params.id; // Obtener el ID de la URL
    console.log(`CONTROLLER: Petición GET /niveles/${nivelId}`);
    try {
        const query = 'SELECT * FROM Niveles WHERE id_nivel = ?';
        const [rows] = await dbPool.query(query, [nivelId]);

        if (rows.length > 0) {
            console.log(`CONTROLLER: Nivel con ID ${nivelId} encontrado.`);
            res.json(rows[0]); // Devolver el nivel encontrado
        } else {
            console.log(`CONTROLLER: Nivel con ID ${nivelId} no encontrado.`);
            res.status(404).json({ message: 'Nivel no encontrado' }); // Error 404
        }
    } catch (error) {
        console.error(`Error en CONTROLLER GET /niveles/${nivelId}:`, error);
        res.status(500).json({ message: 'Error al obtener el nivel.' });
    }
};

// --- CREAR un nuevo nivel ---
exports.createNivel = async (req, res) => {
    const { id_usuario, nombre, dificultad, par, descripcion, monedas_recompensa } = req.body;
    console.log('CONTROLLER: Petición POST /niveles con datos:', req.body);

    if (!id_usuario || !nombre || !dificultad || !par) {
        return res.status(400).json({ message: 'Faltan campos obligatorios (id_usuario, nombre, dificultad, par)' });
    }

    try {
        const query = 'INSERT INTO Niveles (id_usuario, nombre, dificultad, par, descripcion, monedas_recompensa) VALUES (?, ?, ?, ?, ?, ?)';
        const [result] = await dbPool.query(query, [id_usuario, nombre, dificultad, par, descripcion, monedas_recompensa]);

        console.log(`CONTROLLER: Nivel creado con ID: ${result.insertId}`);
        res.status(201).json({
            id_nivel: result.insertId,
            message: 'Nivel creado exitosamente'
        });

    } catch (error) {
        console.error('CONTROLLER Error en POST /niveles:', error);
        res.status(500).json({ message: 'Error al crear el nivel.' });
    }
};

// --- ACTUALIZAR un nivel ---
exports.updateNivel = async (req, res) => {
    const nivelId = req.params.id;
    const { id_usuario, nombre, dificultad, par, descripcion, monedas_recompensa } = req.body;
    console.log(`CONTROLLER: Petición PUT /niveles/${nivelId} con datos:`, req.body);

    if (!nombre || !dificultad || !par) { // id_usuario no debería ser modificable
        return res.status(400).json({ message: 'Faltan campos obligatorios para la actualización (nombre, dificultad, par)' });
    }

    try {
        const query = `UPDATE Niveles SET
                       nombre = ?, dificultad = ?, par = ?,
                       descripcion = ?, monedas_recompensa = ?
                       WHERE id_nivel = ?`;
        const [result] = await dbPool.query(query, [
            nombre, dificultad, par,
            descripcion, monedas_recompensa,
            nivelId
        ]);

        if (result.affectedRows > 0) {
            console.log(`CONTROLLER: Nivel con ID ${nivelId} actualizado.`);
            res.json({ message: 'Nivel actualizado exitosamente' });
        } else {
            console.log(`CONTROLLER: Nivel con ID ${nivelId} no encontrado para actualizar.`);
            res.status(404).json({ message: 'Nivel no encontrado para actualizar' });
        }
    } catch (error) {
        console.error(`CONTROLLER Error en PUT /niveles/${nivelId}:`, error);
        res.status(500).json({ message: 'Error al actualizar el nivel.' });
    }
};

// --- ELIMINAR un nivel ---
exports.deleteNivel = async (req, res) => {
    const nivelId = req.params.id;
    console.log(`CONTROLLER: Petición DELETE /niveles/${nivelId}`);

    try {
        const query = 'DELETE FROM Niveles WHERE id_nivel = ?';
        const [result] = await dbPool.query(query, [nivelId]);

        if (result.affectedRows > 0) {
            console.log(`CONTROLLER: Nivel con ID ${nivelId} eliminado.`);
            res.status(200).json({ message: 'Nivel eliminado exitosamente' });
        } else {
            console.log(`CONTROLLER: Nivel con ID ${nivelId} no encontrado para eliminar.`);
            res.status(404).json({ message: 'Nivel no encontrado para eliminar' });
        }
    } catch (error) {
        console.error(`CONTROLLER Error en DELETE /niveles/${nivelId}:`, error);
        res.status(500).json({ message: 'Error al eliminar el nivel.' });
    }
};