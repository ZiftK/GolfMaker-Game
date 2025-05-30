/**
 * @fileoverview Configuración de rutas para la gestión de estadísticas.
 * Este módulo define todas las rutas relacionadas con operaciones de estadísticas,
 * incluyendo CRUD básico y consultas específicas por usuario y nivel.
 * 
 * @module routes/estadisticas
 * @requires express
 * @requires ../controllers/estadisticasController
 */

import { Router } from 'express';
import {
  getAllEstadisticas,
  createEstadistica,
  updateEstadistica,
  deleteEstadistica,
  getEstadisticasByUsuario,
  getEstadisticasByNivel,
  getEstadisticasByUsuarioAndNivel
} from '../controllers/estadisticasController';

/**
 * Router para la gestión de estadísticas
 * @constant {Router} router
 */
const router = Router();

/**
 * Operaciones CRUD básicas para estadísticas
 * @route GET / - Obtiene todas las estadísticas
 * @route POST / - Crea un nuevo registro de estadísticas
 * @route GET /:id - Obtiene estadísticas por ID
 * @route PUT /:id - Actualiza un registro de estadísticas
 * @route DELETE /:id - Elimina un registro de estadísticas
 */
router.get('/', getAllEstadisticas);                                    // Get all statistics
router.post('/', createEstadistica);                                    // Create new statistics
router.get('/:id', getEstadisticasByUsuario);                          // Get statistics by ID
router.put('/:id', updateEstadistica);                                 // Update statistics
router.delete('/:id', deleteEstadistica);                              // Delete statistics

/**
 * Operaciones específicas por usuario y nivel
 * @route GET /usuario/:id - Obtiene estadísticas de un usuario específico
 * @route GET /nivel/:id - Obtiene estadísticas de un nivel específico
 * @route GET /usuario/:userId/nivel/:levelId - Obtiene estadísticas específicas de un usuario en un nivel
 */
router.get('/usuario/:id', getEstadisticasByUsuario);                  // Get statistics by user ID
router.get('/nivel/:id', getEstadisticasByNivel);                      // Get statistics by level ID
router.get('/usuario/:userId/nivel/:levelId', getEstadisticasByUsuarioAndNivel); // Get statistics by user and level

export default router;