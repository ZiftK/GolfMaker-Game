/**
 * @fileoverview Repositorio central de controladores de la aplicación.
 * Este módulo proporciona una instancia única de todos los repositorios
 * necesarios para la aplicación, implementando el patrón Singleton.
 * 
 * @module controllers/controllersRepository
 * @requires repository/abstract/*
 * @requires repository/concrete/*
 */

import { EstadisticaRepository } from "../repository/abstract/EstadisticaRepository";
import { NivelRepository } from "../repository/abstract/NivelRepository";
import { UsuarioRepository } from "../repository/abstract/UsuarioRepository";
import { RatingRepository } from "../repository/abstract/RatingRepository";

import { SupabaseEstadisticaRepository } from "../repository/concrete/SupabaseEstadisticaRepository";
import { SupabaseNivelRepository } from "../repository/concrete/SupabaseNivelRepository";
import { SupabaseUsuarioRepository } from "../repository/concrete/SupabaseUsuarioRepository";
import { SupabaseRatingRepository } from "../repository/concrete/SupabaseRatingRepository";

/**
 * Objeto singleton que contiene todas las instancias de repositorios
 * @constant {Object}
 * @property {EstadisticaRepository} estadisticaRepository - Repositorio para manejo de estadísticas
 * @property {NivelRepository} nivelRepository - Repositorio para manejo de niveles
 * @property {UsuarioRepository} usuarioRepository - Repositorio para manejo de usuarios
 * @property {RatingRepository} ratingRepository - Repositorio para manejo de calificaciones
 */
const controllersRepository = {
  estadisticaRepository: new SupabaseEstadisticaRepository() as EstadisticaRepository,
  nivelRepository: new SupabaseNivelRepository() as NivelRepository,
  usuarioRepository: new SupabaseUsuarioRepository() as UsuarioRepository,
  ratingRepository: new SupabaseRatingRepository() as RatingRepository,
};

export default controllersRepository;