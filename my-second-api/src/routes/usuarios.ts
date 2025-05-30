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

/**
 * Router para la gestión de usuarios
 * @constant {Router} router
 */
const router = Router();

/**
 * Endpoints CRUD básicos para usuarios
 * @route GET / - Obtiene todos los usuarios
 * @route POST / - Crea un nuevo usuario
 * @route PUT /:id - Actualiza un usuario existente
 * @route DELETE /:id - Elimina un usuario
 */
router.get('/', getAllUsuarios);
router.post('/', createUsuario);
router.put('/:id', updateUsuario);
router.delete('/:id', deleteUsuario);

/**
 * Endpoints adicionales para funcionalidades específicas
 * @route GET /:id - Obtiene un usuario por su ID
 * @route GET /:id/niveles-creados - Obtiene los niveles creados por un usuario
 * @route GET /:id/niveles-jugados - Obtiene los niveles jugados por un usuario
 * @route GET /nombre/:nombreUsuario - Busca un usuario por su nombre de usuario
 */
router.get('/:id', getUsuarioById);
router.get('/:id/niveles-creados', getNivelesCreadosPorUsuario);
router.get('/:id/niveles-jugados', getNivelesJugadosPorUsuario);
router.get('/nombre/:nombreUsuario', getUsuarioByUsername);

export default router;