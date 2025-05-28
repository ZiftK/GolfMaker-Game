import { Request, Response } from 'express';
import controllersRepository from './controllersRepository';

const { ratingRepository } = controllersRepository;

export const getAllRatings = async (req: Request, res: Response): Promise<void> => {
  try {
    const ratings = await ratingRepository.getAll();
    res.json(ratings);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

export const createRating = async (req: Request, res: Response): Promise<void> => {
  try {
    const newRating = await ratingRepository.create(req.body);
    res.status(201).json(newRating);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

export const updateRating = async (req: Request, res: Response): Promise<void> => {
  try {
    const { id } = req.params;
    const updatedRating = await ratingRepository.update(id, req.body);
    res.json(updatedRating);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

export const deleteRating = async (req: Request, res: Response): Promise<void> => {
  try {
    const { id } = req.params;
    await ratingRepository.delete(id);
    res.status(204).send();
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

export const getAverageRatingByLevel = async (req: Request, res: Response): Promise<void> => {
  try {
    const { levelId } = req.params;
    const averageRating = await ratingRepository.getAverageRatingByLevel(levelId);
    res.json({ averageRating });
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};