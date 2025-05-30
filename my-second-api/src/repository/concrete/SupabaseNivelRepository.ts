/**
 * @fileoverview Implementación concreta del repositorio de niveles utilizando Supabase.
 * Este módulo proporciona la implementación específica para almacenar y gestionar
 * niveles del juego en una base de datos Supabase.
 * 
 * @module repository/concrete/SupabaseNivelRepository
 * @requires ../abstract/NivelRepository
 * @requires ../../config/supabaseClient
 */

import { NivelRepository, Nivel } from '../abstract/NivelRepository';
import { supabase } from '../../config/supabaseClient';

/**
 * Nombre de la tabla en la base de datos Supabase
 * @constant {string} TABLE_NAME - Nombre de la tabla de niveles
 */
const TABLE_NAME = 'niveles';

/**
 * Implementación del repositorio de niveles usando Supabase
 * @class SupabaseNivelRepository
 * @implements {NivelRepository}
 */
export class SupabaseNivelRepository implements NivelRepository {
  /**
   * Obtiene todos los niveles de la base de datos
   * @async
   * @returns {Promise<Nivel[]>} Lista de todos los niveles
   * @throws {Error} Si hay un error en la consulta a Supabase
   */
  async getAll(): Promise<Nivel[]> {
    const { data, error } = await supabase.from(TABLE_NAME).select('*');
    if (error) throw error;
    return data || [];
  }

  /**
   * Crea un nuevo nivel en la base de datos
   * @async
   * @param {Omit<Nivel, 'id_nivel' | 'fecha_creacion' | 'rating_promedio' | 'jugado_veces' | 'completado_veces'>} data - Datos del nivel a crear
   * @returns {Promise<Nivel>} Nivel creado con todos sus datos
   * @throws {Error} Si hay un error en la inserción
   */
  async create(data: Omit<Nivel, 'id_nivel' | 'fecha_creacion' | 'rating_promedio' | 'jugado_veces' | 'completado_veces'>): Promise<Nivel> {
    const { error, data: insertedData } = await supabase.from(TABLE_NAME).insert(data).select().single();
    if (error) throw error;
    return insertedData;
  }

  /**
   * Actualiza un nivel existente
   * @async
   * @param {number} id - ID del nivel a actualizar
   * @param {Partial<Omit<Nivel, 'id_nivel' | 'fecha_creacion'>>} data - Datos parciales a actualizar
   * @returns {Promise<Nivel>} Nivel actualizado con todos sus datos
   * @throws {Error} Si hay un error en la actualización o si el nivel no existe
   */
  async update(id: number, data: Partial<Omit<Nivel, 'id_nivel' | 'fecha_creacion'>>): Promise<Nivel> {
    const { error, data: updatedData } = await supabase
      .from(TABLE_NAME)
      .update(data)
      .eq('id_nivel', id)
      .select()
      .single();
    if (error) throw error;
    return updatedData;
  }

  /**
   * Elimina un nivel de la base de datos
   * @async
   * @param {number} id - ID del nivel a eliminar
   * @returns {Promise<void>}
   * @throws {Error} Si hay un error en la eliminación o si el nivel no existe
   */
  async delete(id: number): Promise<void> {
    const { error } = await supabase.from(TABLE_NAME).delete().eq('id_nivel', id);
    if (error) throw error;
  }

  /**
   * Obtiene un nivel por su ID
   * @async
   * @param {number} id - ID del nivel a buscar
   * @returns {Promise<Nivel | null>} Nivel encontrado o null si no existe
   * @throws {Error} Si hay un error en la consulta (excepto cuando no se encuentra)
   */
  async getById(id: number): Promise<Nivel | null> {
    const { data, error } = await supabase.from(TABLE_NAME).select('*').eq('id_nivel', id).single();
    if (error) {
      if (error.code === 'PGRST116') return null; // Record not found
      throw error;
    }
    return data;
  }

  /**
   * Obtiene todos los niveles creados por un usuario específico
   * @async
   * @param {number} usuarioId - ID del usuario
   * @returns {Promise<Nivel[]>} Lista de niveles creados por el usuario
   * @throws {Error} Si hay un error en la consulta
   */
  async getByUserId(usuarioId: number): Promise<Nivel[]> {
    const { data, error } = await supabase.from(TABLE_NAME).select('*').eq('id_usuario', usuarioId);
    if (error) throw error;
    return data || [];
  }
}