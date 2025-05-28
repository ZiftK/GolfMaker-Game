import { Request, Response } from 'express';
import controllersRepository from './controllersRepository';

const { nivelRepository, ratingRepository } = controllersRepository;

export const getAllNiveles = (req: Request, res: Response): void => {
  res.send('Get all niveles');
};

export const createNivel = (req: Request, res: Response): void => {
  res.send('Create a new nivel');
};

export const updateNivel = (req: Request, res: Response): void => {
  res.send(`Update nivel with ID ${req.params.id}`);
};

export const deleteNivel = (req: Request, res: Response): void => {
  res.send(`Delete nivel with ID ${req.params.id}`);
};

export const getNivelById = async (req: Request, res: Response): Promise<void> => {
  const { id } = req.params;
  try {
    const nivel = await nivelRepository.getById(id);
    res.json(nivel);
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
};

export const getComentariosDeNivel = async (req: Request, res: Response): Promise<void> => {
  const { id } = req.params;
  try {
    const comentarios = await ratingRepository.getByNivelId(id);
    res.json(comentarios);
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
};