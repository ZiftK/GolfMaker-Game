import { Router } from 'express';
import { getAllNiveles, createNivel, updateNivel, deleteNivel } from '../controllers/nivelesController';

const router = Router();

router.get('/', getAllNiveles);
router.post('/', createNivel);
router.put('/:id', updateNivel);
router.delete('/:id', deleteNivel);

export default router;