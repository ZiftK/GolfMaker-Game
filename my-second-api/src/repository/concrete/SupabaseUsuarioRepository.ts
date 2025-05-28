import { UsuarioRepository, Usuario } from '../abstract/UsuarioRepository';
import { supabase } from '../../config/supabaseClient';

const USUARIOS_TABLE = 'usuarios';
const NIVELES_TABLE = 'niveles';
const ESTADISTICAS_TABLE = 'estadisticasjugadormapa';

export class SupabaseUsuarioRepository implements UsuarioRepository {
  async getAll(): Promise<Usuario[]> {
    const { data, error } = await supabase.from(USUARIOS_TABLE).select('*');
    if (error) throw error;
    return data || [];
  }

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

  async delete(id: number): Promise<void> {
    const { error } = await supabase.from(USUARIOS_TABLE).delete().eq('id_usuario', id);
    if (error) throw error;
  }

  async getById(id: number): Promise<Usuario> {
    const { data, error } = await supabase
      .from(USUARIOS_TABLE)
      .select('*')
      .eq('id_usuario', id)
      .single();
    if (error) throw error;
    return data;
  }

  async getByUsuarioId(id: number): Promise<any[]> {
    const { data, error } = await supabase.from(NIVELES_TABLE).select('*').eq('id_usuario', id);
    if (error) throw error;
    return data || [];
  }

  async getJugadosPorUsuarioId(id: number): Promise<any[]> {
    const { data, error } = await supabase.from(ESTADISTICAS_TABLE).select('*').eq('id_usuario', id);
    if (error) throw error;
    return data || [];
  }
}