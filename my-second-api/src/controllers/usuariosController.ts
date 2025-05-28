import { Request, Response } from 'express';
import controllersRepository from './controllersRepository';
import { Usuario } from '../repository/abstract/UsuarioRepository';

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
    const usuarioData: Omit<Usuario, 'id_usuario' | 'fecha_registro'> = {
      nombre_usuario: req.body.nombre_usuario,
      email: req.body.email,
      contrasenna: req.body.contrasenna,
      niveles_creados: req.body.niveles_creados || 0,
      niveles_completados: req.body.niveles_completados || 0,
      puntiacion_promedio_recibida: req.body.puntiacion_promedio_recibida || 0
    };
    const newUsuario = await usuarioRepository.create(usuarioData);
    res.status(201).json(newUsuario);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

export const updateUsuario = async (req: Request, res: Response): Promise<void> => {
  try {
    const id = parseInt(req.params.id);
    if (isNaN(id)) {
      res.status(400).json({ error: 'ID inválido' });
      return;
    }

    const updateData: Partial<Omit<Usuario, 'id_usuario' | 'fecha_registro'>> = {
      nombre_usuario: req.body.nombre_usuario,
      email: req.body.email,
      contrasenna: req.body.contrasenna,
      niveles_creados: req.body.niveles_creados,
      niveles_completados: req.body.niveles_completados,
      puntiacion_promedio_recibida: req.body.puntiacion_promedio_recibida
    };

    const updatedUsuario = await usuarioRepository.update(id, updateData);
    res.json(updatedUsuario);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

export const deleteUsuario = async (req: Request, res: Response): Promise<void> => {
  try {
    const id = parseInt(req.params.id);
    if (isNaN(id)) {
      res.status(400).json({ error: 'ID inválido' });
      return;
    }

    await usuarioRepository.delete(id);
    res.status(204).send();
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

export const getUsuarioById = async (req: Request, res: Response): Promise<void> => {
  try {
    const id = parseInt(req.params.id);
    if (isNaN(id)) {
      res.status(400).json({ error: 'ID inválido' });
      return;
    }

    const usuario = await usuarioRepository.getById(id);
    if (!usuario) {
      res.status(404).json({ error: 'Usuario no encontrado' });
      return;
    }
    res.json(usuario);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

export const getNivelesCreadosPorUsuario = async (req: Request, res: Response): Promise<void> => {
  try {
    const id = parseInt(req.params.id);
    if (isNaN(id)) {
      res.status(400).json({ error: 'ID inválido' });
      return;
    }

    const niveles = await nivelRepository.getByUserId(id);
    res.json(niveles);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

export const getNivelesJugadosPorUsuario = async (req: Request, res: Response): Promise<void> => {
  try {
    const id = parseInt(req.params.id);
    if (isNaN(id)) {
      res.status(400).json({ error: 'ID inválido' });
      return;
    }

    const niveles = await estadisticaRepository.getByUserId(id);
    res.json(niveles);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

export const getUsuarioByUsername = async (req: Request, res: Response): Promise<void> => {
  try {
    const { nombreUsuario } = req.params;
    if (!nombreUsuario) {
      res.status(400).json({ error: 'Nombre de usuario no proporcionado' });
      return;
    }

    const usuario = await usuarioRepository.getByUsername(nombreUsuario);
    if (!usuario) {
      res.status(404).json({ error: 'Usuario no encontrado' });
      return;
    }
    res.json(usuario);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};