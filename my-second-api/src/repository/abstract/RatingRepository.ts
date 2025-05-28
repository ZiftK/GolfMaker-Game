export interface Rating {
  id_rating: number;
  id_usuario: number;
  id_nivel: number;
  calificacion: number;
  comentario: string;
  fecha_rating: Date;
}

export interface RatingRepository {
  getAll(): Promise<Rating[]>;
  create(data: Omit<Rating, 'id_rating' | 'fecha_rating'>): Promise<Rating>;
  update(id: number, data: Partial<Omit<Rating, 'id_rating' | 'fecha_rating'>>): Promise<Rating>;
  delete(id: number): Promise<void>;
  getAverageRatingByLevel(levelId: number): Promise<number>;
}