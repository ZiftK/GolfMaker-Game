import { Router } from 'express';
import { getAllNiveles, createNivel, updateNivel, deleteNivel, getNivelById, getComentariosDeNivel } from '../controllers/nivelesController';

const router = Router();

router.get('/', getAllNiveles);
router.post('/', createNivel);
router.put('/:id', updateNivel);
router.delete('/:id', deleteNivel);
router.get('/:id', getNivelById);
router.get('/:id/comentarios', getComentariosDeNivel);

export default router;