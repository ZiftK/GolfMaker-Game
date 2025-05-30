/**
 * @fileoverview Controlador de usuarios que maneja todas las operaciones relacionadas con usuarios.
 * Este módulo proporciona funciones para crear, leer, actualizar y eliminar usuarios,
 * así como para obtener información relacionada con sus niveles y estadísticas.
 * 
 * @module controllers/usuariosController
 * @requires express
 * @requires ./controllersRepository
 */

import { Request, Response } from 'express';
import controllersRepository from './controllersRepository';
import { Usuario } from '../repository/abstract/UsuarioRepository';

const { usuarioRepository, nivelRepository, estadisticaRepository } = controllersRepository;

/**
 * Obtiene todos los usuarios registrados en el sistema
 * @async
 * @param {Request} req - Objeto de solicitud Express
 * @param {Response} res - Objeto de respuesta Express
 * @returns {Promise<void>} Responde con un array de usuarios o un error
 */
export const getAllUsuarios = async (req: Request, res: Response): Promise<void> => {
  try {
    const usuarios = await usuarioRepository.getAll();
    res.json(usuarios);
  } catch (error: any) {
    res.status(500).json({ error: error.message });
  }
};

/**
 * Crea un nuevo usuario en el sistema
 * @async
 * @param {Request} req - Objeto de solicitud Express con los datos del usuario en el body
 * @param {Response} res - Objeto de respuesta Express
 * @returns {Promise<void>} Responde con el usuario creado o un error
 */
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

/**
 * Actualiza la información de un usuario existente
 * @async
 * @param {Request} req - Objeto de solicitud Express con el ID del usuario en params y datos actualizados en body
 * @param {Response} res - Objeto de respuesta Express
 * @returns {Promise<void>} Responde con el usuario actualizado o un error
 */
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

/**
 * Elimina un usuario del sistema
 * @async
 * @param {Request} req - Objeto de solicitud Express con el ID del usuario en params
 * @param {Response} res - Objeto de respuesta Express
 * @returns {Promise<void>} Responde con código 204 si se elimina correctamente o un error
 */
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

/**
 * Obtiene un usuario por su ID
 * @async
 * @param {Request} req - Objeto de solicitud Express con el ID del usuario en params
 * @param {Response} res - Objeto de respuesta Express
 * @returns {Promise<void>} Responde con el usuario encontrado o un error
 */
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

/**
 * Obtiene todos los niveles creados por un usuario específico
 * @async
 * @param {Request} req - Objeto de solicitud Express con el ID del usuario en params
 * @param {Response} res - Objeto de respuesta Express
 * @returns {Promise<void>} Responde con un array de niveles o un error
 */
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

/**
 * Obtiene todos los niveles jugados por un usuario específico
 * @async
 * @param {Request} req - Objeto de solicitud Express con el ID del usuario en params
 * @param {Response} res - Objeto de respuesta Express
 * @returns {Promise<void>} Responde con un array de niveles jugados o un error
 */
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

/**
 * Obtiene un usuario por su nombre de usuario
 * @async
 * @param {Request} req - Objeto de solicitud Express con el nombre de usuario en params
 * @param {Response} res - Objeto de respuesta Express
 * @returns {Promise<void>} Responde con el usuario encontrado o un error
 */
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