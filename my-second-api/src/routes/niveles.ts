/**
 * @fileoverview Configuración de rutas para la gestión de niveles del juego.
 * Este módulo define todas las rutas relacionadas con operaciones de niveles,
 * incluyendo CRUD básico, operaciones relacionadas con usuarios y gestión de comentarios.
 * 
 * @module routes/niveles
 * @requires express
 * @requires ../controllers/nivelesController
 */

import { Router } from 'express';
import {
  getAllNiveles,
  createNivel,
  updateNivel,
  deleteNivel,
  getNivelById,
  getNivelesByUsuario,
  getComentariosDeNivel
} from '../controllers/nivelesController';

/**
 * Router para la gestión de niveles
 * @constant {Router} router
 */
const router = Router();

/**
 * Operaciones CRUD básicas para niveles
 * @route GET / - Obtiene todos los niveles
 * @route POST / - Crea un nuevo nivel
 * @route GET /:id - Obtiene un nivel específico por ID
 * @route PUT /:id - Actualiza un nivel existente
 * @route DELETE /:id - Elimina un nivel
 */
router.get('/', getAllNiveles);                    // Get all levels
router.post('/', createNivel);                     // Create a new level
router.get('/:id', getNivelById);                  // Get a specific level by ID
router.put('/:id', updateNivel);                   // Update a level
router.delete('/:id', deleteNivel);                // Delete a level

/**
 * Operaciones relacionadas con usuarios
 * @route GET /usuario/:id - Obtiene todos los niveles creados por un usuario específico
 */
router.get('/usuario/:id', getNivelesByUsuario);   // Get all levels created by a user

/**
 * Operaciones de calificaciones y comentarios
 * @route GET /:id/comentarios - Obtiene los comentarios y calificaciones de un nivel específico
 */
router.get('/:id/comentarios', getComentariosDeNivel); // Get comments and ratings for a level

export default router;