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

const router = Router();

/**
 * @openapi
 * /ratings:
 *   get:
 *     summary: Obtiene todas las calificaciones
 *     tags:
 *       - Ratings
 *     responses:
 *       200:
 *         description: Lista de calificaciones
 *   post:
 *     summary: Crea una nueva calificación
 *     tags:
 *       - Ratings
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             properties:
 *               nivelId:
 *                 type: string
 *               usuarioId:
 *                 type: string
 *               puntuacion:
 *                 type: number
 *     responses:
 *       201:
 *         description: Calificación creada
 */
router.route('/')
  .get(getAllRatings)
  .post(createRating);

/**
 * @openapi
 * /ratings/{id}:
 *   put:
 *     summary: Actualiza una calificación existente
 *     tags:
 *       - Ratings
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         schema:
 *           type: string
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             properties:
 *               puntuacion:
 *                 type: number
 *     responses:
 *       200:
 *         description: Calificación actualizada
 *   delete:
 *     summary: Elimina una calificación
 *     tags:
 *       - Ratings
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         schema:
 *           type: string
 *     responses:
 *       204:
 *         description: Calificación eliminada
 */
router.route('/:id')
  .put(updateRating)
  .delete(deleteRating);

/**
 * @openapi
 * /ratings/nivel/{id}/promedio:
 *   get:
 *     summary: Obtiene el promedio de calificaciones de un nivel específico
 *     tags:
 *       - Ratings
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         description: ID del nivel
 *         schema:
 *           type: string
 *     responses:
 *       200:
 *         description: Promedio de calificaciones del nivel
 */
router.get('/nivel/:id/promedio', getAverageRatingByLevel);

export default router;
