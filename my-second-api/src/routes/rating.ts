/**
 * @fileoverview Configuración de rutas para la gestión de calificaciones.
 * Este módulo define todas las rutas relacionadas con operaciones de calificaciones,
 * incluyendo CRUD básico y cálculo de promedios por nivel.
 * 
 * @module routes/rating
 * @requires express
 * @requires ../controllers/ratingController
 */

import { Router } from 'express';
import {
  getAllRatings,
  createRating,
  updateRating,
  deleteRating,
  getAverageRatingByLevel
} from '../controllers/ratingController';

/**
 * Router para la gestión de calificaciones
 * @constant {Router} router
 */
const router = Router();

/**
 * Endpoints CRUD básicos para calificaciones
 * @route GET / - Obtiene todas las calificaciones
 * @route POST / - Crea una nueva calificación
 * @route PUT /:id - Actualiza una calificación existente
 * @route DELETE /:id - Elimina una calificación
 */
router.get('/', getAllRatings);
router.post('/', createRating);
router.put('/:id', updateRating);
router.delete('/:id', deleteRating);

/**
 * Endpoints adicionales para cálculos y estadísticas
 * @route GET /nivel/:id/promedio - Obtiene el promedio de calificaciones de un nivel específico
 */
router.get('/nivel/:id/promedio', getAverageRatingByLevel);

export default router;