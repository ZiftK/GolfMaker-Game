export interface NivelRepository {
  getAll(): Promise<any[]>;
  create(data: any): Promise<any>;
  update(id: string, data: any): Promise<any>;
  delete(id: string): Promise<void>;
  getByUserId(usuarioId: string): Promise<any[]>;
  getById(id: string): Promise<any>;
}