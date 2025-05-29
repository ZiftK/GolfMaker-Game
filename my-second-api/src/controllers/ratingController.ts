import { Request, Response } from 'express';
import controllersRepository from './controllersRepository';
import { Rating } from '../repository/abstract/RatingRepository';

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
    const ratingData: Omit<Rating, 'id_rating' | 'fecha_rating'> = {
      id_usuario: req.body.id_usuario,
      id_nivel: req.body.id_nivel,
      calificacion: req.body.calificacion,
      comentario: req.body.comentario
    };

    // Validar que la calificación esté entre 1 y 5
    if (ratingData.calificacion < 1 || ratingData.calificacion > 5) {
      res.status(400).json({ error: 'La calificación debe estar entre 1 y 5' });
      return;
    }

    const newRating = await ratingRepository.create(ratingData);
    res.status(201).json(newRating);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

export const updateRating = async (req: Request, res: Response): Promise<void> => {
  try {
    const id = parseInt(req.params.id);
    if (isNaN(id)) {
      res.status(400).json({ error: 'ID inválido' });
      return;
    }

    const updateData: Partial<Omit<Rating, 'id_rating' | 'fecha_rating'>> = {
      id_usuario: req.body.id_usuario,
      id_nivel: req.body.id_nivel,
      calificacion: req.body.calificacion,
      comentario: req.body.comentario
    };

    // Validar que la calificación esté entre 1 y 5 si se proporciona
    if (updateData.calificacion && (updateData.calificacion < 1 || updateData.calificacion > 5)) {
      res.status(400).json({ error: 'La calificación debe estar entre 1 y 5' });
      return;
    }

    const updatedRating = await ratingRepository.update(id, updateData);
    res.json(updatedRating);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

export const deleteRating = async (req: Request, res: Response): Promise<void> => {
  try {
    const id = parseInt(req.params.id);
    if (isNaN(id)) {
      res.status(400).json({ error: 'ID inválido' });
      return;
    }

    await ratingRepository.delete(id);
    res.status(204).send();
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

export const getAverageRatingByLevel = async (req: Request, res: Response): Promise<void> => {
  try {
    const id = parseInt(req.params.id);
    if (isNaN(id)) {
      res.status(400).json({ error: 'ID inválido' });
      return;
    }

    const average = await ratingRepository.getAverageRatingByLevel(id);
    res.json({ average });
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};