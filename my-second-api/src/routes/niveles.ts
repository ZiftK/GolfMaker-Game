import { Router } from 'express';
import {
  getAllNiveles,
  createNivel,
  updateNivel,
  deleteNivel,
  getNivelById,
  getNivelesByUsuario,
  getComentariosDeNivel
} from '../controllers/nivelesController';

const router = Router();

// Basic CRUD operations
router.get('/', getAllNiveles);                    // Get all levels
router.post('/', createNivel);                     // Create a new level
router.get('/:id', getNivelById);                  // Get a specific level by ID
router.put('/:id', updateNivel);                   // Update a level
router.delete('/:id', deleteNivel);                // Delete a level

// User-related operations
router.get('/usuario/:id', getNivelesByUsuario);   // Get all levels created by a user

// Rating and comments
router.get('/:id/comentarios', getComentariosDeNivel); // Get comments and ratings for a level

export default router;