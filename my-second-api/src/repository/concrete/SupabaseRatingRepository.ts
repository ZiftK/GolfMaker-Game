import { RatingRepository } from '../abstract/RatingRepository';
import { supabase } from '../../config/supabaseClient';

export class SupabaseRatingRepository implements RatingRepository {
  async getAll(): Promise<any[]> {
    const { data, error } = await supabase.from('Rating').select('*');
    if (error) throw error;
    return data || [];
  }

  async create(data: any): Promise<any> {
    const { error, data: insertedData } = await supabase.from('Rating').insert(data).select().single();
    if (error) throw error;
    return insertedData;
  }

  async update(id: string, data: any): Promise<any> {
    const { error, data: updatedData } = await supabase
      .from('Rating')
      .update(data)
      .eq('id_rating', id)
      .select()
      .single();
    if (error) throw error;
    return updatedData;
  }

  async delete(id: string): Promise<void> {
    const { error } = await supabase.from('Rating').delete().eq('id_rating', id);
    if (error) throw error;
  }
}