// routes/bloquesRoutes.js
const express = require('express');
const router = express.Router();
const bloquesController = require('../controllers/bloquesController'); // Importa el controlador

// Rutas para el recurso 'bloques' identificado por id_bloque
// La ruta base '/bloques' se define en app.js

// PUT /bloques/:idBloque -> Llama a updateBloque
router.put('/:idBloque', bloquesController.updateBloque);

// DELETE /bloques/:idBloque -> Llama a deleteBloque
router.delete('/:idBloque', bloquesController.deleteBloque);

// Podríamos añadir GET /bloques/:idBloque si fuera necesario

module.exports = router;