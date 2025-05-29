import { Request, Response } from 'express';
import controllersRepository from './controllersRepository';
import { Nivel, Dificultad } from '../repository/abstract/NivelRepository';

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
    const nivelData = {
      id_usuario: req.body.id_usuario,
      nombre: req.body.nombre,
      dificultad: req.body.dificultad as Dificultad,
      descripcion: req.body.descripcion,
      estructura_nivel: req.body.estructura_nivel,
      cantidad_monedas: req.body.cantidad_monedas || 0
    };

    // Validar que la dificultad sea válida
    if (!['Facil', 'Medio', 'Dificil'].includes(nivelData.dificultad)) {
      res.status(400).json({ error: 'Dificultad inválida. Debe ser: fácil, medio o difícil' });
      return;
    }

    const newNivel = await nivelRepository.create(nivelData);
    res.status(201).json(newNivel);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

export const updateNivel = async (req: Request, res: Response): Promise<void> => {
  try {
    const id = parseInt(req.params.id);
    if (isNaN(id)) {
      res.status(400).json({ error: 'ID inválido' });
      return;
    }

    const updateData: Partial<Omit<Nivel, 'id_nivel' | 'fecha_creacion'>> = {
      nombre: req.body.nombre,
      dificultad: req.body.dificultad as Dificultad,
      descripcion: req.body.descripcion,
      estructura_nivel: req.body.estructura_nivel,
      cantidad_monedas: req.body.cantidad_monedas,
      jugado_veces: req.body.jugado_veces,
      completado_veces: req.body.completado_veces
    };

    // Validar que la dificultad sea válida si se proporciona
    if (updateData.dificultad && !['Facil', 'Medio', 'Dificil'].includes(updateData.dificultad)) {
      res.status(400).json({ error: 'Dificultad inválida. Debe ser: Facil, Medio o Dificil' });
      return;
    }

    const updatedNivel = await nivelRepository.update(id, updateData);
    res.json(updatedNivel);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

export const deleteNivel = async (req: Request, res: Response): Promise<void> => {
  try {
    const id = parseInt(req.params.id);
    if (isNaN(id)) {
      res.status(400).json({ error: 'ID inválido' });
      return;
    }

    await nivelRepository.delete(id);
    res.status(204).send();
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

export const getNivelById = async (req: Request, res: Response): Promise<void> => {
  try {
    const id = parseInt(req.params.id);
    if (isNaN(id)) {
      res.status(400).json({ error: 'ID inválido' });
      return;
    }

    const nivel = await nivelRepository.getById(id);
    if (!nivel) {
      res.status(404).json({ error: 'Nivel no encontrado' });
      return;
    }
    res.json(nivel);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

export const getNivelesByUsuario = async (req: Request, res: Response): Promise<void> => {
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

export const getComentariosDeNivel = async (req: Request, res: Response): Promise<void> => {
  try {
    const id = parseInt(req.params.id);
    if (isNaN(id)) {
      res.status(400).json({ error: 'ID inválido' });
      return;
    }

    const comentarios = await ratingRepository.getAverageRatingByLevel(id);
    res.json(comentarios);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};