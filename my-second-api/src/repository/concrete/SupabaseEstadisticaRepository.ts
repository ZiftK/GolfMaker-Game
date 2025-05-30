/**
 * @fileoverview Implementación concreta del repositorio de estadísticas utilizando Supabase.
 * Este módulo proporciona la implementación específica para almacenar y gestionar
 * estadísticas de jugadores y niveles en una base de datos Supabase.
 * 
 * @module repository/concrete/SupabaseEstadisticaRepository
 * @requires ../abstract/EstadisticaRepository
 * @requires ../../config/supabaseClient
 */

import { EstadisticaRepository, Estadistica } from '../abstract/EstadisticaRepository';
import { supabase } from '../../config/supabaseClient';

/**
 * Nombre de la tabla en la base de datos Supabase
 * @constant {string} TABLE_NAME - Nombre de la tabla de estadísticas
 */
const TABLE_NAME = 'estadisticasjugadormapa';

/**
 * Implementación del repositorio de estadísticas usando Supabase
 * @class SupabaseEstadisticaRepository
 * @implements {EstadisticaRepository}
 */
export class SupabaseEstadisticaRepository implements EstadisticaRepository {
  /**
   * Obtiene todas las estadísticas de la base de datos
   * @async
   * @returns {Promise<Estadistica[]>} Lista de todas las estadísticas
   * @throws {Error} Si hay un error en la consulta a Supabase
   */
  async getAll(): Promise<Estadistica[]> {
    const { data, error } = await supabase.from(TABLE_NAME).select('*');
    if (error) throw error;
    return data || [];
  }

  /**
   * Crea un nuevo registro de estadísticas en la base de datos
   * @async
   * @param {Omit<Estadistica, 'id_estadistica'>} data - Datos de la estadística a crear
   * @returns {Promise<Estadistica>} Estadística creada con todos sus datos
   * @throws {Error} Si hay un error en la inserción
   */
  async create(data: Omit<Estadistica, 'id_estadistica'>): Promise<Estadistica> {
    const { error, data: insertedData } = await supabase.from(TABLE_NAME).insert(data).select().single();
    if (error) throw error;
    return insertedData;
  }

  /**
   * Actualiza un registro de estadísticas existente
   * @async
   * @param {number} id - ID de la estadística a actualizar
   * @param {Partial<Omit<Estadistica, 'id_estadistica'>>} data - Datos parciales a actualizar
   * @returns {Promise<Estadistica>} Estadística actualizada con todos sus datos
   * @throws {Error} Si hay un error en la actualización o si la estadística no existe
   */
  async update(id: number, data: Partial<Omit<Estadistica, 'id_estadistica'>>): Promise<Estadistica> {
    const { error, data: updatedData } = await supabase
      .from(TABLE_NAME)
      .update(data)
      .eq('id_estadistica', id)
      .select()
      .single();
    if (error) throw error;
    return updatedData;
  }

  /**
   * Elimina un registro de estadísticas de la base de datos
   * @async
   * @param {number} id - ID de la estadística a eliminar
   * @returns {Promise<void>}
   * @throws {Error} Si hay un error en la eliminación o si la estadística no existe
   */
  async delete(id: number): Promise<void> {
    const { error } = await supabase.from(TABLE_NAME).delete().eq('id_estadistica', id);
    if (error) throw error;
  }

  /**
   * Obtiene todas las estadísticas de un usuario específico
   * @async
   * @param {number} userId - ID del usuario
   * @returns {Promise<Estadistica[]>} Lista de estadísticas del usuario
   * @throws {Error} Si hay un error en la consulta
   */
  async getByUserId(userId: number): Promise<Estadistica[]> {
    const { data, error } = await supabase.from(TABLE_NAME).select('*').eq('id_usuario', userId);
    if (error) throw error;
    return data || [];
  }

  /**
   * Obtiene todas las estadísticas de un nivel específico
   * @async
   * @param {number} levelId - ID del nivel
   * @returns {Promise<Estadistica[]>} Lista de estadísticas del nivel
   * @throws {Error} Si hay un error en la consulta
   */
  async getByLevelId(levelId: number): Promise<Estadistica[]> {
    const { data, error } = await supabase.from(TABLE_NAME).select('*').eq('id_nivel', levelId);
    if (error) throw error;
    return data || [];
  }

  /**
   * Obtiene todas las estadísticas de un usuario en un nivel específico
   * @async
   * @param {number} userId - ID del usuario
   * @param {number} levelId - ID del nivel
   * @returns {Promise<Estadistica[]>} Lista de estadísticas del usuario en el nivel
   * @throws {Error} Si hay un error en la consulta
   */
  async getByUserIdAndLevelId(userId: number, levelId: number): Promise<Estadistica[]> {
    const { data, error } = await supabase
      .from(TABLE_NAME)
      .select('*')
      .eq('id_usuario', userId)
      .eq('id_nivel', levelId);
    if (error) throw error;
    return data || [];
  }
}