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

// CRUD endpoints
router.get('/', getAllUsuarios);
router.post('/', createUsuario);
router.put('/:id', updateUsuario);
router.delete('/:id', deleteUsuario);

// Additional endpoints
router.get('/:id', getUsuarioById);
router.get('/:id/niveles-creados', getNivelesCreadosPorUsuario);
router.get('/:id/niveles-jugados', getNivelesJugadosPorUsuario);
router.get('/nombre/:nombreUsuario', getUsuarioByUsername);

export default router;