import { Request, Response } from 'express';
import controllersRepository from './controllersRepository';

const { usuarioRepository, nivelRepository, estadisticaRepository } = controllersRepository;

export const getAllUsuarios = async (req: Request, res: Response): Promise<void> => {
  try {
    const usuarios = await usuarioRepository.getAll();
    res.json(usuarios);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

export const createUsuario = async (req: Request, res: Response): Promise<void> => {
  try {
    const newUsuario = await usuarioRepository.create(req.body);
    res.status(201).json(newUsuario);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

export const updateUsuario = async (req: Request, res: Response): Promise<void> => {
  try {
    const { id } = req.params;
    const updatedUsuario = await usuarioRepository.update(id, req.body);
    res.json(updatedUsuario);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

export const deleteUsuario = async (req: Request, res: Response): Promise<void> => {
  try {
    const { id } = req.params;
    await usuarioRepository.delete(id);
    res.status(204).send();
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

export const getUsuarioById = async (req: Request, res: Response): Promise<void> => {
  try {
    const { id } = req.params;
    const usuario = await usuarioRepository.getById(id);
    res.json(usuario);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

export const getNivelesCreadosPorUsuario = async (req: Request, res: Response): Promise<void> => {
  try {
    const { id } = req.params;
    const niveles = await nivelRepository.getByUserId(id);
    res.json(niveles);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

export const getNivelesJugadosPorUsuario = async (req: Request, res: Response): Promise<void> => {
  try {
    const { id } = req.params;
    const niveles = await estadisticaRepository.getByUserId(id);
    res.json(niveles);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};