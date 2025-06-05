/**
 * @fileoverview Controlador de niveles que maneja todas las operaciones relacionadas con los niveles del juego.
 * Este módulo proporciona funciones para crear, leer, actualizar y eliminar niveles,
 * así como para obtener información relacionada con los comentarios y calificaciones de los niveles.
 * 
 * @module controllers/nivelesController
 * @requires express
 * @requires ./controllersRepository
 */

import { Request, Response } from 'express';
import controllersRepository from './controllersRepository';
import { Nivel, Dificultad } from '../repository/abstract/NivelRepository';

const { nivelRepository, ratingRepository } = controllersRepository;

/**
 * Obtiene todos los niveles registrados en el sistema
 * @async
 * @param {Request} req - Objeto de solicitud Express
 * @param {Response} res - Objeto de respuesta Express
 * @returns {Promise<void>} Responde con un array de niveles o un error
 */
export const getAllNiveles = async (req: Request, res: Response): Promise<void> => {
  try {
    const niveles = await nivelRepository.getAll();
    res.json(niveles);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

/**
 * Crea un nuevo nivel en el sistema
 * @async
 * @param {Request} req - Objeto de solicitud Express con los datos del nivel en el body
 * @param {Response} res - Objeto de respuesta Express
 * @returns {Promise<void>} Responde con el nivel creado o un error
 * @throws {Error} Si la dificultad proporcionada no es válida
 */
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

/**
 * Actualiza la información de un nivel existente
 * @async
 * @param {Request} req - Objeto de solicitud Express con el ID del nivel en params y datos actualizados en body
 * @param {Response} res - Objeto de respuesta Express
 * @returns {Promise<void>} Responde con el nivel actualizado o un error
 * @throws {Error} Si la dificultad proporcionada no es válida
 */
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

/**
 * Elimina un nivel del sistema
 * @async
 * @param {Request} req - Objeto de solicitud Express con el ID del nivel en params
 * @param {Response} res - Objeto de respuesta Express
 * @returns {Promise<void>} Responde con código 204 si se elimina correctamente o un error
 */
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

/**
 * Obtiene un nivel por su ID
 * @async
 * @param {Request} req - Objeto de solicitud Express con el ID del nivel en params
 * @param {Response} res - Objeto de respuesta Express
 * @returns {Promise<void>} Responde con el nivel encontrado o un error
 */
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

/**
 * Obtiene todos los niveles creados por un usuario específico
 * @async
 * @param {Request} req - Objeto de solicitud Express con el ID del usuario en params
 * @param {Response} res - Objeto de respuesta Express
 * @returns {Promise<void>} Responde con un array de niveles o un error
 */
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

/**
 * Obtiene los comentarios y calificaciones de un nivel específico
 * @async
 * @param {Request} req - Objeto de solicitud Express con el ID del nivel en params
 * @param {Response} res - Objeto de respuesta Express
 * @returns {Promise<void>} Responde con los comentarios y calificación promedio del nivel o un error
 */
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