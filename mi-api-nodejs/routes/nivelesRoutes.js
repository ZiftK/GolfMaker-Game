// routes/nivelesRoutes.js
const express = require('express');
const router = express.Router();
const nivelesController = require('../controllers/nivelesController');
const bloquesController = require('../controllers/bloquesController');
const ratingsController = require('../controllers/ratingsController'); // <-- AÑADIR ESTA LÍNEA

// --- Rutas para Niveles ---
router.get('/', nivelesController.getAllNiveles);
router.post('/', nivelesController.createNivel);
router.get('/:id', nivelesController.getNivelById);
router.put('/:id', nivelesController.updateNivel);
router.delete('/:id', nivelesController.deleteNivel);

// --- Rutas para Bloques (anidadas bajo niveles) ---
router.get('/:idNivel/bloques', bloquesController.getBloquesByNivelId);
router.post('/:idNivel/bloques', bloquesController.createBloqueForNivel);

// --- Rutas para Ratings (anidadas bajo niveles) --- // <-- AÑADIR ESTAS RUTAS
// GET /niveles/:idNivel/ratings -> Llama a getRatingsByNivelId
router.get('/:idNivel/ratings', ratingsController.getRatingsByNivelId);

// POST /niveles/:idNivel/ratings -> Llama a createRatingForNivel
router.post('/:idNivel/ratings', ratingsController.createRatingForNivel);


module.exports = router;