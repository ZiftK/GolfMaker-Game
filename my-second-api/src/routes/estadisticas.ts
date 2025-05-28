import { Router } from 'express';
import {
  getAllEstadisticas,
  createEstadistica,
  updateEstadistica,
  deleteEstadistica,
  getEstadisticasByUserId,
  getEstadisticasByLevelId,
  getEstadisticasByUserIdAndLevelId
} from '../controllers/estadisticasController';

const router = Router();

// CRUD endpoints
router.get('/', getAllEstadisticas);
router.post('/', createEstadistica);
router.put('/:id', updateEstadistica);
router.delete('/:id', deleteEstadistica);

// Additional endpoints
router.get('/user/:userId', getEstadisticasByUserId);
router.get('/level/:levelId', getEstadisticasByLevelId);
router.get('/user/:userId/level/:levelId', getEstadisticasByUserIdAndLevelId);

export default router;