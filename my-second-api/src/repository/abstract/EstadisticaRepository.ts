export interface EstadisticaRepository {
  getAll(): Promise<any[]>;
  create(data: any): Promise<any>;
  update(id: string, data: any): Promise<any>;
  delete(id: string): Promise<void>;
  getByUserId(userId: string): Promise<any[]>;
  getByLevelId(levelId: string): Promise<any[]>;
  getByUserIdAndLevelId(userId: string, levelId: string): Promise<any[]>;
}