import { Request, Response } from 'express';
import controllersRepository from './controllersRepository';

const { nivelRepository, ratingRepository } = controllersRepository;

export const getAllNiveles = async (req: Request, res: Response): Promise<void> => {
  try {
    const niveles = await nivelRepository.getAll();
    res.json(niveles);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

export const createNivel = async (req: Request, res: Response): Promise<void> => {
  try {
    const newNivel = await nivelRepository.create(req.body);
    res.status(201).json(newNivel);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

export const updateNivel = async (req: Request, res: Response): Promise<void> => {
  try {
    const { id } = req.params;
    const updatedNivel = await nivelRepository.update(id, req.body);
    res.json(updatedNivel);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

export const deleteNivel = async (req: Request, res: Response): Promise<void> => {
  try {
    const { id } = req.params;
    await nivelRepository.delete(id);
    res.status(204).send();
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

export const getNivelById = async (req: Request, res: Response): Promise<void> => {
  try {
    const { id } = req.params;
    const nivel = await nivelRepository.getById(id);
    res.json(nivel);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

export const getNivelesByUserId = async (req: Request, res: Response): Promise<void> => {
  try {
    const { userId } = req.params;
    const niveles = await nivelRepository.getByUserId(userId);
    res.json(niveles);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

export const getComentariosDeNivel = async (req: Request, res: Response): Promise<void> => {
  try {
    const { id } = req.params;
    const comentarios = await ratingRepository.getAverageRatingByLevel(id);
    res.json(comentarios);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};