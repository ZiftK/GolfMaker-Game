import { Router } from 'express';
import { getAllEstadisticas, createEstadistica, updateEstadistica, deleteEstadistica } from '../controllers/estadisticasController';

const router = Router();

router.get('/', getAllEstadisticas);
router.post('/', createEstadistica);
router.put('/:id', updateEstadistica);
router.delete('/:id', deleteEstadistica);

export default router;