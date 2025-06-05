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

const router = Router();

/**
 * @openapi
 * /estadisticas:
 *   get:
 *     summary: Obtener todas las estadísticas
 *     tags: [Estadisticas]
 *     responses:
 *       200:
 *         description: Lista de estadísticas
 */
router.get('/', getAllEstadisticas);

/**
 * @openapi
 * /estadisticas:
 *   post:
 *     summary: Crear una nueva estadística
 *     tags: [Estadisticas]
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             properties:
 *               usuarioId:
 *                 type: string
 *               nivelId:
 *                 type: string
 *               tiempo:
 *                 type: number
 *     responses:
 *       201:
 *         description: Estadística creada
 */
router.post('/', createEstadistica);

/**
 * @openapi
 * /estadisticas/{id}:
 *   put:
 *     summary: Actualizar una estadística
 *     tags: [Estadisticas]
 *     parameters:
 *       - in: path
 *         name: id
 *         schema:
 *           type: string
 *         required: true
 *         description: ID de la estadística
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             properties:
 *               tiempo:
 *                 type: number
 *     responses:
 *       200:
 *         description: Estadística actualizada
 */
router.put('/:id', updateEstadistica);

/**
 * @openapi
 * /estadisticas/{id}:
 *   delete:
 *     summary: Eliminar una estadística
 *     tags: [Estadisticas]
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         schema:
 *           type: string
 *     responses:
 *       200:
 *         description: Estadística eliminada
 */
router.delete('/:id', deleteEstadistica);

/**
 * @openapi
 * /estadisticas/usuario/{id}:
 *   get:
 *     summary: Obtener estadísticas por usuario
 *     tags: [Estadisticas]
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         schema:
 *           type: string
 *     responses:
 *       200:
 *         description: Estadísticas del usuario
 */
router.get('/usuario/:id', getEstadisticasByUsuario);

/**
 * @openapi
 * /estadisticas/nivel/{id}:
 *   get:
 *     summary: Obtener estadísticas por nivel
 *     tags: [Estadisticas]
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         schema:
 *           type: string
 *     responses:
 *       200:
 *         description: Estadísticas del nivel
 */
router.get('/nivel/:id', getEstadisticasByNivel);

/**
 * @openapi
 * /estadisticas/usuario/{userId}/nivel/{levelId}:
 *   get:
 *     summary: Obtener estadísticas por usuario y nivel
 *     tags: [Estadisticas]
 *     parameters:
 *       - in: path
 *         name: userId
 *         required: true
 *         schema:
 *           type: string
 *       - in: path
 *         name: levelId
 *         required: true
 *         schema:
 *           type: string
 *     responses:
 *       200:
 *         description: Estadísticas del usuario en el nivel
 */
router.get('/usuario/:userId/nivel/:levelId', getEstadisticasByUsuarioAndNivel);

export default router;
