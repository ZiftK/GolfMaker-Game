import { EstadisticaRepository } from '../abstract/EstadisticaRepository';
import { supabase } from '../../config/supabaseClient';

export class SupabaseEstadisticaRepository implements EstadisticaRepository {
  async getAll(): Promise<any[]> {
    const { data, error } = await supabase.from('EstadisticasJugadorMapa').select('*');
    if (error) throw error;
    return data || [];
  }

  async create(data: any): Promise<any> {
    const { error, data: insertedData } = await supabase.from('EstadisticasJugadorMapa').insert(data).select().single();
    if (error) throw error;
    return insertedData;
  }

  async update(id: string, data: any): Promise<any> {
    const { error, data: updatedData } = await supabase
      .from('EstadisticasJugadorMapa')
      .update(data)
      .eq('id_estadistica', id)
      .select()
      .single();
    if (error) throw error;
    return updatedData;
  }

  async delete(id: string): Promise<void> {
    const { error } = await supabase.from('EstadisticasJugadorMapa').delete().eq('id_estadistica', id);
    if (error) throw error;
  }
}