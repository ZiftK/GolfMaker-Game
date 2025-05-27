import { Router } from 'express';
import usuariosRoutes from './usuarios';
import nivelesRoutes from './niveles';
import ratingRoutes from './rating';
import estadisticasRoutes from './estadisticas';

const router = Router();

// Use entity routes
router.use('/usuarios', usuariosRoutes);
router.use('/niveles', nivelesRoutes);
router.use('/rating', ratingRoutes);
router.use('/estadisticas', estadisticasRoutes);

export default router;