/**
 * @fileoverview Controlador de estadísticas que maneja todas las operaciones relacionadas con las estadísticas del juego.
 * Este módulo proporciona funciones para crear, leer, actualizar y eliminar estadísticas de jugadores,
 * así como para obtener información relacionada con el rendimiento en niveles específicos.
 * 
 * @module controllers/estadisticasController
 * @requires express
 * @requires ./controllersRepository
 */

import { Request, Response } from 'express';
import controllersRepository from './controllersRepository';
import { Estadistica } from '../repository/abstract/EstadisticaRepository';

const { estadisticaRepository } = controllersRepository;

/**
 * Obtiene todas las estadísticas registradas en el sistema
 * @async
 * @param {Request} req - Objeto de solicitud Express
 * @param {Response} res - Objeto de respuesta Express
 * @returns {Promise<void>} Responde con un array de estadísticas o un error
 */
export const getAllEstadisticas = async (req: Request, res: Response): Promise<void> => {
  try {
    const estadisticas = await estadisticaRepository.getAll();
    res.json(estadisticas);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

/**
 * Crea un nuevo registro de estadísticas en el sistema
 * @async
 * @param {Request} req - Objeto de solicitud Express con los datos de estadística en el body
 * @param {Response} res - Objeto de respuesta Express
 * @returns {Promise<void>} Responde con la estadística creada o un error
 * @throws {Error} Si la calificación general está fuera del rango válido (0-5)
 */
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

/**
 * Actualiza un registro de estadísticas existente
 * @async
 * @param {Request} req - Objeto de solicitud Express con el ID de la estadística en params y datos actualizados en body
 * @param {Response} res - Objeto de respuesta Express
 * @returns {Promise<void>} Responde con la estadística actualizada o un error
 * @throws {Error} Si la calificación general está fuera del rango válido (0-5)
 */
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

/**
 * Elimina un registro de estadísticas del sistema
 * @async
 * @param {Request} req - Objeto de solicitud Express con el ID de la estadística en params
 * @param {Response} res - Objeto de respuesta Express
 * @returns {Promise<void>} Responde con código 204 si se elimina correctamente o un error
 */
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

/**
 * Obtiene todas las estadísticas de un usuario específico
 * @async
 * @param {Request} req - Objeto de solicitud Express con el ID del usuario en params
 * @param {Response} res - Objeto de respuesta Express
 * @returns {Promise<void>} Responde con un array de estadísticas del usuario o un error
 */
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

/**
 * Obtiene todas las estadísticas de un nivel específico
 * @async
 * @param {Request} req - Objeto de solicitud Express con el ID del nivel en params
 * @param {Response} res - Objeto de respuesta Express
 * @returns {Promise<void>} Responde con un array de estadísticas del nivel o un error
 */
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

/**
 * Obtiene las estadísticas específicas de un usuario en un nivel particular
 * @async
 * @param {Request} req - Objeto de solicitud Express con el ID del usuario y del nivel en params
 * @param {Response} res - Objeto de respuesta Express
 * @returns {Promise<void>} Responde con las estadísticas encontradas o un error
 */
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