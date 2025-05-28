export interface RatingRepository {
  getAll(): Promise<any[]>;
  create(data: any): Promise<any>;
  update(id: string, data: any): Promise<any>;
  delete(id: string): Promise<void>;
  getAverageRatingByLevel(levelId: string): Promise<number>;
}