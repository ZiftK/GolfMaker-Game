import { Request, Response } from 'express';
import controllersRepository from './controllersRepository';
import { Estadistica } from '../repository/abstract/EstadisticaRepository';

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
    const estadisticaData = {
      id_usuario: req.body.id_usuario,
      id_nivel: req.body.id_nivel,
      max_muertes: req.body.max_muertes || 0,
      min_muertes: req.body.min_muertes || 0,
      max_golpes: req.body.max_golpes || 0,
      min_golpes: req.body.min_golpes || 0,
      monedas_recolectadas: req.body.monedas_recolectadas || 0,
      calificacion_general: req.body.calificacion_general || 0
    };

    // Validar calificación general
    if (estadisticaData.calificacion_general < 0 || estadisticaData.calificacion_general > 5) {
      res.status(400).json({ error: 'La calificación general debe estar entre 0 y 5' });
      return;
    }

    const newEstadistica = await estadisticaRepository.create(estadisticaData);
    res.status(201).json(newEstadistica);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

export const updateEstadistica = async (req: Request, res: Response): Promise<void> => {
  try {
    const id = parseInt(req.params.id);
    if (isNaN(id)) {
      res.status(400).json({ error: 'ID inválido' });
      return;
    }

    const updateData: Partial<Omit<Estadistica, 'id_estadistica'>> = {
      max_muertes: req.body.max_muertes,
      min_muertes: req.body.min_muertes,
      max_golpes: req.body.max_golpes,
      min_golpes: req.body.min_golpes,
      monedas_recolectadas: req.body.monedas_recolectadas,
      calificacion_general: req.body.calificacion_general
    };

    // Validar calificación general si se proporciona
    if (updateData.calificacion_general !== undefined && 
        (updateData.calificacion_general < 0 || updateData.calificacion_general > 5)) {
      res.status(400).json({ error: 'La calificación general debe estar entre 0 y 5' });
      return;
    }

    const updatedEstadistica = await estadisticaRepository.update(id, updateData);
    res.json(updatedEstadistica);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

export const deleteEstadistica = async (req: Request, res: Response): Promise<void> => {
  try {
    const id = parseInt(req.params.id);
    if (isNaN(id)) {
      res.status(400).json({ error: 'ID inválido' });
      return;
    }

    await estadisticaRepository.delete(id);
    res.status(204).send();
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

export const getEstadisticasByUsuario = async (req: Request, res: Response): Promise<void> => {
  try {
    const id = parseInt(req.params.id);
    if (isNaN(id)) {
      res.status(400).json({ error: 'ID inválido' });
      return;
    }

    const estadisticas = await estadisticaRepository.getByUserId(id);
    res.json(estadisticas);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

export const getEstadisticasByNivel = async (req: Request, res: Response): Promise<void> => {
  try {
    const id = parseInt(req.params.id);
    if (isNaN(id)) {
      res.status(400).json({ error: 'ID inválido' });
      return;
    }

    const estadisticas = await estadisticaRepository.getByLevelId(id);
    res.json(estadisticas);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

export const getEstadisticasByUsuarioAndNivel = async (req: Request, res: Response): Promise<void> => {
  try {
    const userId = parseInt(req.params.userId);
    const levelId = parseInt(req.params.levelId);
    if (isNaN(userId) || isNaN(levelId)) {
      res.status(400).json({ error: 'IDs inválidos' });
      return;
    }

    const estadisticas = await estadisticaRepository.getByUserIdAndLevelId(userId, levelId);
    res.json(estadisticas);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};