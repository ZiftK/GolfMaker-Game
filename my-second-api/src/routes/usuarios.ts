/**
 * @fileoverview Configuración de rutas para la gestión de usuarios.
 * Este módulo define todas las rutas relacionadas con operaciones de usuarios,
 * incluyendo CRUD básico y endpoints adicionales para obtener información específica.
 * 
 * @module routes/usuarios
 * @requires express
 * @requires ../controllers/usuariosController
 */

import { Router } from 'express';
import {
  getAllUsuarios,
  createUsuario,
  updateUsuario,
  deleteUsuario,
  getUsuarioById,
  getNivelesCreadosPorUsuario,
  getNivelesJugadosPorUsuario,
  getUsuarioByUsername
} from '../controllers/usuariosController';

const router = Router();

/**
 * @openapi
 * /usuarios:
 *   get:
 *     summary: Obtiene todos los usuarios
 *     tags:
 *       - Usuarios
 *     responses:
 *       200:
 *         description: Lista de usuarios
 *   post:
 *     summary: Crea un nuevo usuario
 *     tags:
 *       - Usuarios
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             properties:
 *               nombre_usuario:
 *                 type: string
 *               email:
 *                 type: string
 *               contrasenna:
 *                 type: string
 *     responses:
 *       201:
 *         description: Usuario creado
 */
// Rutas básicas - EXACTAMENTE como antes
router.get('/', getAllUsuarios);
router.post('/', createUsuario);

/**
 * @openapi
 * /usuarios/nombre/{nombreUsuario}:
 *   get:
 *     summary: Busca un usuario por su nombre de usuario
 *     tags:
 *       - Usuarios
 *     parameters:
 *       - in: path
 *         name: nombreUsuario
 *         required: true
 *         schema:
 *           type: string
 *     responses:
 *       200:
 *         description: Usuario encontrado
 */
router.get('/nombre/:nombreUsuario', getUsuarioByUsername);

/**
 * @openapi
 * /usuarios/{id}/niveles-creados:
 *   get:
 *     summary: Obtiene los niveles creados por un usuario
 *     tags:
 *       - Usuarios
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         schema:
 *           type: string
 *     responses:
 *       200:
 *         description: Lista de niveles creados
 */
router.get('/:id/niveles-creados', getNivelesCreadosPorUsuario);

/**
 * @openapi
 * /usuarios/{id}/niveles-jugados:
 *   get:
 *     summary: Obtiene los niveles jugados por un usuario
 *     tags:
 *       - Usuarios
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         schema:
 *           type: string
 *     responses:
 *       200:
 *         description: Lista de niveles jugados
 */
router.get('/:id/niveles-jugados', getNivelesJugadosPorUsuario);

/**
 * @openapi
 * /usuarios/{id}:
 *   get:
 *     summary: Obtiene un usuario por su ID
 *     tags:
 *       - Usuarios
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         schema:
 *           type: string
 *     responses:
 *       200:
 *         description: Datos del usuario
 *   put:
 *     summary: Actualiza un usuario existente
 *     tags:
 *       - Usuarios
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
 *               nombre:
 *                 type: string
 *               correo:
 *                 type: string
 *     responses:
 *       200:
 *         description: Usuario actualizado
 *   delete:
 *     summary: Elimina un usuario
 *     tags:
 *       - Usuarios
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         schema:
 *           type: string
 *     responses:
 *       204:
 *         description: Usuario eliminado
 */
router.get('/:id', getUsuarioById);
router.put('/:id', updateUsuario);
router.delete('/:id', deleteUsuario);

export default router;