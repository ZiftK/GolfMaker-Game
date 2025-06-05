/**
 * @fileoverview Implementación concreta del repositorio de calificaciones utilizando Supabase.
 * Este módulo proporciona la implementación específica para almacenar y gestionar
 * calificaciones de niveles en una base de datos Supabase.
 * 
 * @module repository/concrete/SupabaseRatingRepository
 * @requires ../abstract/RatingRepository
 * @requires ../../config/supabaseClient
 */

import { RatingRepository, Rating } from '../abstract/RatingRepository';
import { supabase } from '../../config/supabaseClient';

/**
 * Nombre de la tabla en la base de datos Supabase
 * @constant {string} TABLE_NAME - Nombre de la tabla de calificaciones
 */
const TABLE_NAME = 'rating';

/**
 * Implementación del repositorio de calificaciones usando Supabase
 * @class SupabaseRatingRepository
 * @implements {RatingRepository}
 */
export class SupabaseRatingRepository implements RatingRepository {
  
  /**
   * Calcula el promedio de calificaciones para un nivel específico
   * @async
   * @param {number} levelId - ID del nivel
   * @returns {Promise<number>} Promedio de calificaciones (0 si no hay calificaciones)
   * @throws {Error} Si hay un error en la consulta a Supabase
   */
  async getAverageRatingByLevel(levelId: number): Promise<number> {
    const { data, error } = await supabase
      .from(TABLE_NAME)
      .select('calificacion')
      .eq('id_nivel', levelId);
    if (error) throw error;
    if (!data || data.length === 0) return 0;
    const ratings = data.map((rating) => rating.calificacion);
    const average = ratings.reduce((sum, rating) => sum + rating, 0) / ratings.length;
    return average;
  }

  /**
   * Obtiene todas las calificaciones de la base de datos
   * @async
   * @returns {Promise<Rating[]>} Lista de todas las calificaciones
   * @throws {Error} Si hay un error en la consulta a Supabase
   */
  async getAll(): Promise<Rating[]> {
    const { data, error } = await supabase.from(TABLE_NAME).select('*');
    if (error) throw error;
    return data || [];
  }

  /**
   * Crea una nueva calificación en la base de datos
   * @async
   * @param {Omit<Rating, 'id_rating' | 'fecha_rating'>} data - Datos de la calificación a crear
   * @returns {Promise<Rating>} Calificación creada con todos sus datos
   * @throws {Error} Si hay un error en la inserción
   */
  async create(data: Omit<Rating, 'id_rating' | 'fecha_rating'>): Promise<Rating> {
    const { error, data: insertedData } = await supabase.from(TABLE_NAME).insert(data).select().single();
    if (error) throw error;
    return insertedData;
  }

  /**
   * Actualiza una calificación existente
   * @async
   * @param {number} id - ID de la calificación a actualizar
   * @param {Partial<Omit<Rating, 'id_rating' | 'fecha_rating'>>} data - Datos parciales a actualizar
   * @returns {Promise<Rating>} Calificación actualizada con todos sus datos
   * @throws {Error} Si hay un error en la actualización o si la calificación no existe
   */
  async update(id: number, data: Partial<Omit<Rating, 'id_rating' | 'fecha_rating'>>): Promise<Rating> {
    const { error, data: updatedData } = await supabase
      .from(TABLE_NAME)
      .update(data)
      .eq('id_rating', id)
      .select()
      .single();
    if (error) throw error;
    return updatedData;
  }

  /**
   * Elimina una calificación de la base de datos
   * @async
   * @param {number} id - ID de la calificación a eliminar
   * @returns {Promise<void>}
   * @throws {Error} Si hay un error en la eliminación o si la calificación no existe
   */
  async delete(id: number): Promise<void> {
    const { error } = await supabase.from(TABLE_NAME).delete().eq('id_rating', id);
    if (error) throw error;
  }
}