import { Request, Response } from 'express';
import controllersRepository from './controllersRepository';

const { estadisticaRepository } = controllersRepository;

export const getAllEstadisticas = async (req: Request, res: Response): Promise<void> => {
  try {
    const estadisticas = await estadisticaRepository.getAll();
    res.json(estadisticas);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

export const createEstadistica = async (req: Request, res: Response): Promise<void> => {
  try {
    const newEstadistica = await estadisticaRepository.create(req.body);
    res.status(201).json(newEstadistica);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

export const updateEstadistica = async (req: Request, res: Response): Promise<void> => {
  try {
    const { id } = req.params;
    const updatedEstadistica = await estadisticaRepository.update(id, req.body);
    res.json(updatedEstadistica);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

export const deleteEstadistica = async (req: Request, res: Response): Promise<void> => {
  try {
    const { id } = req.params;
    await estadisticaRepository.delete(id);
    res.status(204).send();
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

export const getEstadisticasByUserId = async (req: Request, res: Response): Promise<void> => {
  try {
    const { userId } = req.params;
    const estadisticas = await estadisticaRepository.getByUserId(userId);
    res.json(estadisticas);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

export const getEstadisticasByLevelId = async (req: Request, res: Response): Promise<void> => {
  try {
    const { levelId } = req.params;
    const estadisticas = await estadisticaRepository.getByLevelId(levelId);
    res.json(estadisticas);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

export const getEstadisticasByUserIdAndLevelId = async (req: Request, res: Response): Promise<void> => {
  try {
    const { userId, levelId } = req.params;
    const estadisticas = await estadisticaRepository.getByUserIdAndLevelId(userId, levelId);
    res.json(estadisticas);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};