// routes/usuariosRoutes.js
const express = require('express');
const router = express.Router(); // Creamos un router específico para usuarios
const usuariosController = require('../controllers/usuariosController'); // Importamos el controlador

// Definimos las rutas para el recurso 'usuarios'
// Nota: La ruta base '/usuarios' se define en app.js al usar este router

// GET /usuarios -> Llama a getAllUsuarios del controlador
router.get('/', usuariosController.getAllUsuarios);

// POST /usuarios -> Llama a createUsuario del controlador
router.post('/', usuariosController.createUsuario);

// GET /usuarios/:id -> Llama a getUsuarioById del controlador
router.get('/:id', usuariosController.getUsuarioById);

// PUT /usuarios/:id -> Llama a updateUsuario del controlador
router.put('/:id', usuariosController.updateUsuario);

// DELETE /usuarios/:id -> Llama a deleteUsuario del controlador
router.delete('/:id', usuariosController.deleteUsuario);

// POST /usuarios/login -> Llama a loginUsuario del controlador 
router.post('/login', usuariosController.loginUsuario);

// --- Rutas para PowerUps de un Usuario Específico --- // <--

// GET /usuarios/:idUsuario/powerups -> Llama a getUsuarioPowerUps
router.get('/:idUsuario/powerups', usuariosController.getUsuarioPowerUps);

// POST /usuarios/:idUsuario/powerups -> Llama a addPowerUpToUsuario
router.post('/:idUsuario/powerups', usuariosController.addPowerUpToUsuario);

// --- Ruta para USAR un PowerUp --- // <-- 
// POST /usuarios/:idUsuario/powerups/:idPowerup/use -> Llama a usePowerUp
router.post('/:idUsuario/powerups/:idPowerup/use', usuariosController.usePowerUp);

module.exports = router; // Exportamos el router configurado