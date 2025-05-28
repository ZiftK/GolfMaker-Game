import { NivelRepository, Nivel } from '../abstract/NivelRepository';
import { supabase } from '../../config/supabaseClient';

const TABLE_NAME = 'niveles';

export class SupabaseNivelRepository implements NivelRepository {
  async getAll(): Promise<Nivel[]> {
    const { data, error } = await supabase.from(TABLE_NAME).select('*');
    if (error) throw error;
    return data || [];
  }

  async create(data: Omit<Nivel, 'id_nivel' | 'fecha_creacion' | 'rating_promedio' | 'jugado_veces' | 'completado_veces'>): Promise<Nivel> {
    const { error, data: insertedData } = await supabase.from(TABLE_NAME).insert(data).select().single();
    if (error) throw error;
    return insertedData;
  }

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

  async delete(id: number): Promise<void> {
    const { error } = await supabase.from(TABLE_NAME).delete().eq('id_nivel', id);
    if (error) throw error;
  }

  async getById(id: number): Promise<Nivel | null> {
    const { data, error } = await supabase.from(TABLE_NAME).select('*').eq('id_nivel', id).single();
    if (error) {
      if (error.code === 'PGRST116') return null; // Record not found
      throw error;
    }
    return data;
  }

  async getByUserId(usuarioId: number): Promise<Nivel[]> {
    const { data, error } = await supabase.from(TABLE_NAME).select('*').eq('id_usuario', usuarioId);
    if (error) throw error;
    return data || [];
  }
}