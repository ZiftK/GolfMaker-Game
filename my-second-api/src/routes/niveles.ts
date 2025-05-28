import { Router } from 'express';
import {
  getAllNiveles,
  createNivel,
  updateNivel,
  deleteNivel,
  getNivelById,
  getNivelesByUserId,
  getComentariosDeNivel
} from '../controllers/nivelesController';

const router = Router();

// CRUD endpoints
router.get('/', getAllNiveles);
router.post('/', createNivel);
router.put('/:id', updateNivel);
router.delete('/:id', deleteNivel);

// Additional endpoints
router.get('/:id', getNivelById);
router.get('/user/:userId', getNivelesByUserId);
router.get('/:id/comentarios', getComentariosDeNivel);

export default router;