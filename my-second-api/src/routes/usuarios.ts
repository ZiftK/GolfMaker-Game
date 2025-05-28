import { Router } from 'express';
import { getAllUsuarios, createUsuario, updateUsuario, deleteUsuario, getUsuarioById, getNivelesCreadosPorUsuario, getNivelesJugadosPorUsuario } from '../controllers/usuariosController';

const router = Router();

router.get('/', getAllUsuarios);
router.post('/', createUsuario);
router.put('/:id', updateUsuario);
router.delete('/:id', deleteUsuario);
router.get('/:id', getUsuarioById);
router.get('/:id/niveles-creados', getNivelesCreadosPorUsuario);
router.get('/:id/niveles-jugados', getNivelesJugadosPorUsuario);

export default router;