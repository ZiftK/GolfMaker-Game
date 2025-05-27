import { Router } from 'express';
import { getAllRatings, createRating, updateRating, deleteRating } from '../controllers/ratingController';

const router = Router();

router.get('/', getAllRatings);
router.post('/', createRating);
router.put('/:id', updateRating);
router.delete('/:id', deleteRating);

export default router;