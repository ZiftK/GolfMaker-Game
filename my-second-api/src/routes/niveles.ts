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

const router = Router();

/**
 * @openapi
 * /niveles:
 *   get:
 *     summary: Obtiene todos los niveles
 *     tags:
 *       - Niveles
 *     responses:
 *       200:
 *         description: Lista de niveles
 *         content:
 *           application/json:
 *             schema:
 *               type: array
 *               items:
 *                 type: object
 *   post:
 *     summary: Crea un nuevo nivel
 *     tags:
 *       - Niveles
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             properties:
 *               nombre:
 *                 type: string
 *               dificultad:
 *                 type: string
 *     responses:
 *       201:
 *         description: Nivel creado exitosamente
 */
router.route('/niveles')
  .get(getAllNiveles)
  .post(createNivel);

/**
 * @openapi
 * /niveles/{id}:
 *   get:
 *     summary: Obtiene un nivel específico por ID
 *     tags:
 *       - Niveles
 *     parameters:
 *       - name: id
 *         in: path
 *         description: ID del nivel
 *         required: true
 *         schema:
 *           type: string
 *     responses:
 *       200:
 *         description: Nivel encontrado
 *   put:
 *     summary: Actualiza un nivel existente
 *     tags:
 *       - Niveles
 *     parameters:
 *       - name: id
 *         in: path
 *         description: ID del nivel
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
 *               nombre:
 *                 type: string
 *               dificultad:
 *                 type: string
 *     responses:
 *       200:
 *         description: Nivel actualizado exitosamente
 *   delete:
 *     summary: Elimina un nivel
 *     tags:
 *       - Niveles
 *     parameters:
 *       - name: id
 *         in: path
 *         description: ID del nivel
 *         required: true
 *         schema:
 *           type: string
 *     responses:
 *       204:
 *         description: Nivel eliminado
 */
router.route('/niveles/:id')
  .get(getNivelById)
  .put(updateNivel)
  .delete(deleteNivel);

/**
 * @openapi
 * /niveles/usuario/{id}:
 *   get:
 *     summary: Obtiene todos los niveles creados por un usuario específico
 *     tags:
 *       - Niveles
 *     parameters:
 *       - name: id
 *         in: path
 *         description: ID del usuario
 *         required: true
 *         schema:
 *           type: string
 *     responses:
 *       200:
 *         description: Lista de niveles del usuario
 */
router.get('/niveles/usuario/:id', getNivelesByUsuario);

/**
 * @openapi
 * /niveles/{id}/comentarios:
 *   get:
 *     summary: Obtiene los comentarios y calificaciones de un nivel específico
 *     tags:
 *       - Niveles
 *     parameters:
 *       - name: id
 *         in: path
 *         description: ID del nivel
 *         required: true
 *         schema:
 *           type: string
 *     responses:
 *       200:
 *         description: Comentarios y calificaciones del nivel
 */
router.get('/niveles/:id/comentarios', getComentariosDeNivel);

export default router;
