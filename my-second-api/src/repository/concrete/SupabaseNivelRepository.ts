import { NivelRepository } from '../abstract/NivelRepository';
import { supabase } from '../../config/supabaseClient';

export class SupabaseNivelRepository implements NivelRepository {
  async getAll(): Promise<any[]> {
    const { data, error } = await supabase.from('Niveles').select('*');
    if (error) throw error;
    return data || [];
  }

  async create(data: any): Promise<any> {
    const { error, data: insertedData } = await supabase.from('Niveles').insert(data).select().single();
    if (error) throw error;
    return insertedData;
  }

  async update(id: string, data: any): Promise<any> {
    const { error, data: updatedData } = await supabase
      .from('Niveles')
      .update(data)
      .eq('id_nivel', id)
      .select()
      .single();
    if (error) throw error;
    return updatedData;
  }

  async delete(id: string): Promise<void> {
    const { error } = await supabase.from('Niveles').delete().eq('id_nivel', id);
    if (error) throw error;
  }

  async getById(id: string): Promise<any> {
    const { data, error } = await supabase.from('Niveles').select('*').eq('id_nivel', id).single();
    if (error) throw error;
    return data;
  }

  async getByUserId(usuarioId: string): Promise<any[]> {
    const { data, error } = await supabase.from('Niveles').select('*').eq('id_usuario', usuarioId);
    if (error) throw error;
    return data || [];
  }
}