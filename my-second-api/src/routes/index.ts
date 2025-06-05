/**
 * @fileoverview Configuración central de rutas de la API.
 * Este módulo agrupa y configura todas las rutas principales de la aplicación,
 * actuando como un punto central de enrutamiento.
 * 
 * @module routes/index
 * @requires express
 * @requires ./usuarios
 * @requires ./niveles
 * @requires ./rating
 * @requires ./estadisticas
 */

import { Router } from 'express';
import usuariosRoutes from './usuarios';
import nivelesRoutes from './niveles';
import ratingRoutes from './rating';
import estadisticasRoutes from './estadisticas';

/**
 * Router principal que agrupa todas las rutas de la API
 * @constant {Router} router
 */
const router = Router();

/**
 * Configuración de rutas principales
 * @description Monta las rutas de cada entidad en su respectivo endpoint base
 */
router.use('/usuarios', usuariosRoutes);    // Rutas para gestión de usuarios
router.use('/niveles', nivelesRoutes);      // Rutas para gestión de niveles
router.use('/rating', ratingRoutes);        // Rutas para gestión de calificaciones
router.use('/estadisticas', estadisticasRoutes); // Rutas para gestión de estadísticas

export default router;