/**
 * @fileoverview Implementación concreta del repositorio de usuarios utilizando Supabase.
 * Este módulo proporciona la implementación específica para almacenar y gestionar
 * usuarios en una base de datos Supabase.
 * 
 * @module repository/concrete/SupabaseUsuarioRepository
 * @requires ../abstract/UsuarioRepository
 * @requires ../../config/supabaseClient
 */

import { UsuarioRepository, Usuario } from '../abstract/UsuarioRepository';
import { supabase } from '../../config/supabaseClient';

/**
 * Nombres de las tablas en la base de datos Supabase
 * @constant {string} USUARIOS_TABLE - Nombre de la tabla de usuarios
 * @constant {string} NIVELES_TABLE - Nombre de la tabla de niveles
 * @constant {string} ESTADISTICAS_TABLE - Nombre de la tabla de estadísticas
 */
const USUARIOS_TABLE = 'usuarios';
const NIVELES_TABLE = 'niveles';
const ESTADISTICAS_TABLE = 'estadisticasjugadormapa';

/**
 * Implementación del repositorio de usuarios usando Supabase
 * @class SupabaseUsuarioRepository
 * @implements {UsuarioRepository}
 */
export class SupabaseUsuarioRepository implements UsuarioRepository {
  /**
   * Obtiene todos los usuarios de la base de datos
   * @async
   * @returns {Promise<Usuario[]>} Lista de todos los usuarios
   * @throws {Error} Si hay un error en la consulta a Supabase
   */
  async getAll(): Promise<Usuario[]> {
    const { data, error } = await supabase.from(USUARIOS_TABLE).select('*');
    if (error) throw error;
    return data || [];
  }

  /**
   * Crea un nuevo usuario en la base de datos
   * @async
   * @param {Omit<Usuario, 'id_usuario' | 'fecha_registro'>} data - Datos del usuario a crear
   * @returns {Promise<Usuario>} Usuario creado con todos sus datos
   * @throws {Error} Si hay un error en la inserción o si el nombre de usuario/email ya existe
   */
  async create(data: Omit<Usuario, 'id_usuario' | 'fecha_registro'>): Promise<Usuario> {
    const { error, data: insertedData } = await supabase
      .from(USUARIOS_TABLE)
      .insert({
        nombre_usuario: data.nombre_usuario,
        email: data.email,
        contrasenna: data.contrasenna,
        niveles_creados: data.niveles_creados || 0,
        niveles_completados: data.niveles_completados || 0,
        puntiacion_promedio_recibida: data.puntiacion_promedio_recibida || 0
      })
      .select()
      .single();
    if (error) throw error;
    return insertedData;
  }

  /**
   * Actualiza los datos de un usuario existente
   * @async
   * @param {number} id - ID del usuario a actualizar
   * @param {Partial<Omit<Usuario, 'id_usuario' | 'fecha_registro'>>} data - Datos parciales a actualizar
   * @returns {Promise<Usuario>} Usuario actualizado con todos sus datos
   * @throws {Error} Si hay un error en la actualización o si el usuario no existe
   */
  async update(id: number, data: Partial<Omit<Usuario, 'id_usuario' | 'fecha_registro'>>): Promise<Usuario> {
    const { error, data: updatedData } = await supabase
      .from(USUARIOS_TABLE)
      .update(data)
      .eq('id_usuario', id)
      .select()
      .single();
    if (error) throw error;
    return updatedData;
  }

  /**
   * Elimina un usuario de la base de datos
   * @async
   * @param {number} id - ID del usuario a eliminar
   * @returns {Promise<void>}
   * @throws {Error} Si hay un error en la eliminación o si el usuario no existe
   */
  async delete(id: number): Promise<void> {
    const { error } = await supabase.from(USUARIOS_TABLE).delete().eq('id_usuario', id);
    if (error) throw error;
  }

  /**
   * Obtiene un usuario por su ID
   * @async
   * @param {number} id - ID del usuario a buscar
   * @returns {Promise<Usuario>} Datos del usuario encontrado
   * @throws {Error} Si hay un error en la consulta o si el usuario no existe
   */
  async getById(id: number): Promise<Usuario> {
    const { data, error } = await supabase
      .from(USUARIOS_TABLE)
      .select('*')
      .eq('id_usuario', id)
      .single();
    if (error) throw error;
    return data;
  }

  /**
   * Obtiene todos los niveles creados por un usuario
   * @async
   * @param {number} id - ID del usuario
   * @returns {Promise<any[]>} Lista de niveles creados por el usuario
   * @throws {Error} Si hay un error en la consulta
   */
  async getByUsuarioId(id: number): Promise<any[]> {
    const { data, error } = await supabase.from(NIVELES_TABLE).select('*').eq('id_usuario', id);
    if (error) throw error;
    return data || [];
  }

  /**
   * Obtiene todas las estadísticas de niveles jugados por un usuario
   * @async
   * @param {number} id - ID del usuario
   * @returns {Promise<any[]>} Lista de estadísticas de niveles jugados
   * @throws {Error} Si hay un error en la consulta
   */
  async getJugadosPorUsuarioId(id: number): Promise<any[]> {
    const { data, error } = await supabase.from(ESTADISTICAS_TABLE).select('*').eq('id_usuario', id);
    if (error) throw error;
    return data || [];
  }

  /**
   * Busca un usuario por su nombre de usuario
   * @async
   * @param {string} nombreUsuario - Nombre de usuario a buscar
   * @returns {Promise<Usuario | null>} Usuario encontrado o null si no existe
   * @throws {Error} Si hay un error en la consulta (excepto cuando no se encuentra)
   */
  async getByUsername(nombreUsuario: string): Promise<Usuario | null> {
    const { data, error } = await supabase
      .from(USUARIOS_TABLE)
      .select('*')
      .eq('nombre_usuario', nombreUsuario)
      .single();
    if (error) {
      if (error.code === 'PGRST116') return null; // No rows returned
      throw error;
    }
    return data;
  }
}