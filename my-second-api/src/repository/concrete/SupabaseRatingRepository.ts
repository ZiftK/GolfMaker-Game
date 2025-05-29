import { RatingRepository, Rating } from '../abstract/RatingRepository';
import { supabase } from '../../config/supabaseClient';

const TABLE_NAME = 'rating';

export class SupabaseRatingRepository implements RatingRepository {
  
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

  async getAll(): Promise<Rating[]> {
    const { data, error } = await supabase.from(TABLE_NAME).select('*');
    if (error) throw error;
    return data || [];
  }

  async create(data: Omit<Rating, 'id_rating' | 'fecha_rating'>): Promise<Rating> {
    const { error, data: insertedData } = await supabase.from(TABLE_NAME).insert(data).select().single();
    if (error) throw error;
    return insertedData;
  }

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

  async delete(id: number): Promise<void> {
    const { error } = await supabase.from(TABLE_NAME).delete().eq('id_rating', id);
    if (error) throw error;
  }
}