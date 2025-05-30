/**
 * @fileoverview Controlador de calificaciones que maneja todas las operaciones relacionadas con las valoraciones de niveles.
 * Este módulo proporciona funciones para crear, leer, actualizar y eliminar calificaciones,
 * así como para calcular promedios de calificaciones por nivel.
 * 
 * @module controllers/ratingController
 * @requires express
 * @requires ./controllersRepository
 */

import { Request, Response } from 'express';
import controllersRepository from './controllersRepository';
import { Rating } from '../repository/abstract/RatingRepository';

const { ratingRepository } = controllersRepository;

/**
 * Obtiene todas las calificaciones registradas en el sistema
 * @async
 * @param {Request} req - Objeto de solicitud Express
 * @param {Response} res - Objeto de respuesta Express
 * @returns {Promise<void>} Responde con un array de calificaciones o un error
 */
export const getAllRatings = async (req: Request, res: Response): Promise<void> => {
  try {
    const ratings = await ratingRepository.getAll();
    res.json(ratings);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

/**
 * Crea una nueva calificación en el sistema
 * @async
 * @param {Request} req - Objeto de solicitud Express con los datos de la calificación en el body
 * @param {Response} res - Objeto de respuesta Express
 * @returns {Promise<void>} Responde con la calificación creada o un error
 * @throws {Error} Si la calificación está fuera del rango válido (1-5)
 */
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

/**
 * Actualiza una calificación existente
 * @async
 * @param {Request} req - Objeto de solicitud Express con el ID de la calificación en params y datos actualizados en body
 * @param {Response} res - Objeto de respuesta Express
 * @returns {Promise<void>} Responde con la calificación actualizada o un error
 * @throws {Error} Si la calificación está fuera del rango válido (1-5)
 */
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

/**
 * Elimina una calificación del sistema
 * @async
 * @param {Request} req - Objeto de solicitud Express con el ID de la calificación en params
 * @param {Response} res - Objeto de respuesta Express
 * @returns {Promise<void>} Responde con código 204 si se elimina correctamente o un error
 */
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

/**
 * Obtiene el promedio de calificaciones de un nivel específico
 * @async
 * @param {Request} req - Objeto de solicitud Express con el ID del nivel en params
 * @param {Response} res - Objeto de respuesta Express
 * @returns {Promise<void>} Responde con el promedio de calificaciones del nivel o un error
 */
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