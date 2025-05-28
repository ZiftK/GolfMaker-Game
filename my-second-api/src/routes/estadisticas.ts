import { Router } from 'express';
import {
  getAllEstadisticas,
  createEstadistica,
  updateEstadistica,
  deleteEstadistica,
  getEstadisticasByUsuario,
  getEstadisticasByNivel,
  getEstadisticasByUsuarioAndNivel
} from '../controllers/estadisticasController';

const router = Router();

// Basic CRUD operations
router.get('/', getAllEstadisticas);                                    // Get all statistics
router.post('/', createEstadistica);                                    // Create new statistics
router.get('/:id', getEstadisticasByUsuario);                          // Get statistics by ID
router.put('/:id', updateEstadistica);                                 // Update statistics
router.delete('/:id', deleteEstadistica);                              // Delete statistics

// User and Level specific operations
router.get('/usuario/:id', getEstadisticasByUsuario);                  // Get statistics by user ID
router.get('/nivel/:id', getEstadisticasByNivel);                      // Get statistics by level ID
router.get('/usuario/:userId/nivel/:levelId', getEstadisticasByUsuarioAndNivel); // Get statistics by user and level

export default router;