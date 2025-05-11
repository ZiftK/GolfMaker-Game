// routes/powerUpsRoutes.js
const express = require('express');
const router = express.Router();
const powerUpsController = require('../controllers/powerUpsController');

// GET /powerups
router.get('/', powerUpsController.getAllPowerUps);

// GET /powerups/:idPowerUp
router.get('/:idPowerUp', powerUpsController.getPowerUpById);

// POST /powerups -> Llama a createPowerUp // <-- AÑADIR
router.post('/', powerUpsController.createPowerUp);

// PUT /powerups/:idPowerUp -> Llama a updatePowerUp // <-- AÑADIR
router.put('/:idPowerUp', powerUpsController.updatePowerUp);

// DELETE /powerups/:idPowerUp -> Llama a deletePowerUp // <-- AÑADIR
router.delete('/:idPowerUp', powerUpsController.deletePowerUp);

module.exports = router;