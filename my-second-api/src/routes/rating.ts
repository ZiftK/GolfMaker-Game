import { Router } from 'express';
import {
  getAllRatings,
  createRating,
  updateRating,
  deleteRating,
  getAverageRatingByLevel
} from '../controllers/ratingController';

const router = Router();

// CRUD endpoints
router.get('/', getAllRatings);
router.post('/', createRating);
router.put('/:id', updateRating);
router.delete('/:id', deleteRating);

// Additional endpoints
router.get('/nivel/:id/promedio', getAverageRatingByLevel);

export default router;