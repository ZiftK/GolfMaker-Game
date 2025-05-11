// routes/ratingsRoutes.js
const express = require('express');
const router = express.Router();
const ratingsController = require('../controllers/ratingsController');

// Rutas para el recurso 'ratings' identificado por idRating
// La ruta base '/ratings' se define en app.js

// PUT /ratings/:idRating -> Llama a updateRating
router.put('/:idRating', ratingsController.updateRating);

// DELETE /ratings/:idRating -> Llama a deleteRating
router.delete('/:idRating', ratingsController.deleteRating);

module.exports = router;