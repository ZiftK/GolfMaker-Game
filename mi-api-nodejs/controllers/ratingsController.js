// controllers/ratingsController.js
const dbPool = require('../config/db');

// --- OBTENER Ratings por ID de Nivel ---
exports.getRatingsByNivelId = async (req, res) => {
    const { idNivel } = req.params;
    console.log(`CONTROLLER: Petición GET /niveles/${idNivel}/ratings`);

    try {
        // Hacemos JOIN con Usuarios para obtener el nombre de quien hizo el rating
        const query = `
            SELECT r.id_rating, r.id_usuario, r.id_nivel, r.calificacion, r.comentario, r.fecha_rating, u.nombre_usuario
            FROM Rating r
            JOIN Usuarios u ON r.id_usuario = u.id_usuario
            WHERE r.id_nivel = ?
            ORDER BY r.fecha_rating DESC
        `;
        const [ratings] = await dbPool.query(query, [idNivel]);

        console.log(`CONTROLLER: Consulta ejecutada, ${ratings.length} ratings encontrados para nivel ${idNivel}.`);
        res.json(ratings); // Devolvemos array (puede estar vacío)

    } catch (error) {
        console.error(`Error en CONTROLLER GET /niveles/${idNivel}/ratings:`, error);
        res.status(500).json({ message: 'Error al obtener los ratings del nivel.' });
    }
};

// --- CREAR un Rating para un Nivel Específico ---
exports.createRatingForNivel = async (req, res) => {
    const { idNivel } = req.params;
    // **NOTA DE SEGURIDAD:** Idealmente, id_usuario debería venir de la sesión del usuario logueado (req.user.id),
    // no del cuerpo de la petición, para evitar que alguien envíe ratings por otros.
    // Por ahora, lo tomaremos del body para seguir avanzando.
    const { id_usuario, calificacion, comentario } = req.body;
    console.log(`CONTROLLER: Petición POST /niveles/${idNivel}/ratings con datos:`, req.body);

    // Validación básica
    if (id_usuario === undefined || calificacion === undefined) {
        return res.status(400).json({ message: 'Faltan campos obligatorios (id_usuario, calificacion)' });
    }
    if (typeof calificacion !== 'number' || calificacion < 1 || calificacion > 5) {
         return res.status(400).json({ message: 'El campo "calificacion" debe ser un número entre 1 y 5.' });
    }

    try {
        // Usamos COALESCE para manejar el comentario opcional
        const query = `INSERT INTO Rating
                         (id_nivel, id_usuario, calificacion, comentario)
                       VALUES (?, ?, ?, ?)`;
        const [result] = await dbPool.query(query, [idNivel, id_usuario, calificacion, comentario !== undefined ? comentario : null]);

        console.log(`CONTROLLER: Rating creado con ID: ${result.insertId} para nivel ${idNivel} por usuario ${id_usuario}`);
        // Podríamos añadir lógica aquí para recalcular y actualizar Niveles.rating_promedio

        res.status(201).json({
            id_rating: result.insertId,
            message: 'Rating creado exitosamente'
            // Podríamos devolver el rating completo si quisiéramos
        });

    } catch (error) {
        // Manejar errores comunes (FK = Foreign Key)
        if (error.code === 'ER_NO_REFERENCED_ROW_2') {
             // Puede ser que id_nivel o id_usuario no existan
             console.error(`CONTROLLER Error en POST /niveles/${idNivel}/ratings: ID de Nivel o Usuario no existe.`, error.message);
             return res.status(404).json({ message: 'Error al crear el rating: El nivel o el usuario especificado no existe.' });
        }
         if (error.code === 'ER_DUP_ENTRY') {
             // Podrías tener una restricción UNIQUE(id_usuario, id_nivel) para que un usuario solo califique una vez
             console.error(`CONTROLLER Error en POST /niveles/${idNivel}/ratings: Rating duplicado.`, error.message);
             return res.status(409).json({ message: 'Este usuario ya ha calificado este nivel.' });
        }
        console.error(`CONTROLLER Error en POST /niveles/${idNivel}/ratings:`, error);
        res.status(500).json({ message: 'Error al crear el rating.' });
    }
};

// --- ACTUALIZAR un Rating específico por su ID ---
exports.updateRating = async (req, res) => {
    const { idRating } = req.params; // ID del rating a actualizar
    const { calificacion, comentario } = req.body; // Nuevos datos
    console.log(`CONTROLLER: Petición PUT /ratings/${idRating} con datos:`, req.body);

    // Validación básica
    if (calificacion === undefined && comentario === undefined) {
        return res.status(400).json({ message: 'No se proporcionaron datos para actualizar (calificacion o comentario).' });
    }
    if (calificacion !== undefined && (typeof calificacion !== 'number' || calificacion < 1 || calificacion > 5)) {
         return res.status(400).json({ message: 'El campo "calificacion" debe ser un número entre 1 y 5.' });
    }
    // **NOTA DE SEGURIDAD/LÓGICA:** Aquí deberíamos verificar si el usuario que hace la petición
    // es el mismo que creó el rating (req.user.id === rating.id_usuario).
    // Como aún no tenemos autenticación, omitimos esa verificación por ahora.

    try {
        // Usamos COALESCE para permitir actualizar solo uno de los campos si se desea
        const query = `UPDATE Rating SET
                         calificacion = COALESCE(?, calificacion),
                         comentario = COALESCE(?, comentario)
                       WHERE id_rating = ?`;
        const [result] = await dbPool.query(query, [calificacion, comentario, idRating]);

        if (result.affectedRows > 0) {
            console.log(`CONTROLLER: Rating con ID ${idRating} actualizado.`);
             // Podríamos añadir lógica para recalcular y actualizar Niveles.rating_promedio
            res.json({ message: 'Rating actualizado exitosamente' });
        } else {
            console.log(`CONTROLLER: Rating con ID ${idRating} no encontrado para actualizar.`);
            res.status(404).json({ message: 'Rating no encontrado para actualizar' });
        }
    } catch (error) {
        console.error(`CONTROLLER Error en PUT /ratings/${idRating}:`, error);
        res.status(500).json({ message: 'Error al actualizar el rating.' });
    }
};

// --- ELIMINAR un Rating específico por su ID ---
exports.deleteRating = async (req, res) => {
    const { idRating } = req.params;
    console.log(`CONTROLLER: Petición DELETE /ratings/${idRating}`);
    // **NOTA DE SEGURIDAD/LÓGICA:** Similar a PUT, deberíamos verificar si el usuario
    // que hace la petición es quien creó el rating antes de permitir borrarlo.

    try {
        const query = 'DELETE FROM Rating WHERE id_rating = ?';
        const [result] = await dbPool.query(query, [idRating]);

        if (result.affectedRows > 0) {
            console.log(`CONTROLLER: Rating con ID ${idRating} eliminado.`);
             // Podríamos añadir lógica para recalcular y actualizar Niveles.rating_promedio
            res.status(200).json({ message: 'Rating eliminado exitosamente' });
        } else {
            console.log(`CONTROLLER: Rating con ID ${idRating} no encontrado para eliminar.`);
            res.status(404).json({ message: 'Rating no encontrado para eliminar' });
        }
    } catch (error) {
        console.error(`CONTROLLER Error en DELETE /ratings/${idRating}:`, error);
        res.status(500).json({ message: 'Error al eliminar el rating.' });
    }
};