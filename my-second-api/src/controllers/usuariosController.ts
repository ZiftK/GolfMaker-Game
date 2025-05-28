import { Request, Response } from 'express';
import { SupabaseUsuarioRepository } from '../repository/concrete/SupabaseUsuarioRepository';
import { SupabaseNivelRepository } from '../repository/concrete/SupabaseNivelRepository';
import { SupabaseEstadisticaRepository } from '../repository/concrete/SupabaseEstadisticaRepository';

const usuarioRepository = new SupabaseUsuarioRepository();
const nivelRepository = new SupabaseNivelRepository();
const estadisticaRepository = new SupabaseEstadisticaRepository();

export const getAllUsuarios = (req: Request, res: Response): void => {
  res.send('Get all usuarios');
};

export const createUsuario = (req: Request, res: Response): void => {
  res.send('Create a new usuario');
};

export const updateUsuario = (req: Request, res: Response): void => {
  res.send(`Update usuario with ID ${req.params.id}`);
};

export const deleteUsuario = (req: Request, res: Response): void => {
  res.send(`Delete usuario with ID ${req.params.id}`);
};

export const getUsuarioById = async (req: Request, res: Response) => {
  const { id } = req.params;
  try {
    const usuario = await usuarioRepository.getById(id);
    res.json(usuario);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

export const getNivelesCreadosPorUsuario = async (req: Request, res: Response) => {
  const { id } = req.params;
  try {
    const niveles = await nivelRepository.getByUserId(id);
    res.json(niveles);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

export const getNivelesJugadosPorUsuario = async (req: Request, res: Response) => {
  const { id } = req.params;
  try {
    const niveles = await estadisticaRepository.getByUserId(id);
    res.json(niveles);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};