import { UsuarioRepository } from '../abstract/UsuarioRepository';
import { supabase } from '../../config/supabaseClient';

export class SupabaseUsuarioRepository implements UsuarioRepository {
  async getAll(): Promise<any[]> {
    const { data, error } = await supabase.from('Usuarios').select('*');
    if (error) throw error;
    return data || [];
  }

  async create(data: any): Promise<any> {
    const { error, data: insertedData } = await supabase.from('Usuarios').insert(data).select().single();
    if (error) throw error;
    return insertedData;
  }

  async update(id: string, data: any): Promise<any> {
    const { error, data: updatedData } = await supabase
      .from('Usuarios')
      .update(data)
      .eq('id_usuario', id)
      .select()
      .single();
    if (error) throw error;
    return updatedData;
  }

  async delete(id: string): Promise<void> {
    const { error } = await supabase.from('Usuarios').delete().eq('id_usuario', id);
    if (error) throw error;
  }

  async getById(id: string): Promise<any> {
    const { data, error } = await supabase.from('Usuarios').select('*').eq('id_usuario', id).single();
    if (error) throw error;
    return data;
  }

  async getByUsuarioId(id: string): Promise<any[]> {
    const { data, error } = await supabase.from('Niveles').select('*').eq('id_usuario', id);
    if (error) throw error;
    return data || [];
  }

  async getJugadosPorUsuarioId(id: string): Promise<any[]> {
    const { data, error } = await supabase.from('EstadisticasJugadorMapa').select('*').eq('id_usuario', id);
    if (error) throw error;
    return data || [];
  }
}