import { EstadisticaRepository, Estadistica } from '../abstract/EstadisticaRepository';
import { supabase } from '../../config/supabaseClient';

const TABLE_NAME = 'estadisticasjugadormapa';

export class SupabaseEstadisticaRepository implements EstadisticaRepository {
  async getAll(): Promise<Estadistica[]> {
    const { data, error } = await supabase.from(TABLE_NAME).select('*');
    if (error) throw error;
    return data || [];
  }

  async create(data: Omit<Estadistica, 'id_estadistica'>): Promise<Estadistica> {
    const { error, data: insertedData } = await supabase.from(TABLE_NAME).insert(data).select().single();
    if (error) throw error;
    return insertedData;
  }

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

  async delete(id: number): Promise<void> {
    const { error } = await supabase.from(TABLE_NAME).delete().eq('id_estadistica', id);
    if (error) throw error;
  }

  async getByUserId(userId: number): Promise<Estadistica[]> {
    const { data, error } = await supabase.from(TABLE_NAME).select('*').eq('id_usuario', userId);
    if (error) throw error;
    return data || [];
  }

  async getByLevelId(levelId: number): Promise<Estadistica[]> {
    const { data, error } = await supabase.from(TABLE_NAME).select('*').eq('id_nivel', levelId);
    if (error) throw error;
    return data || [];
  }

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