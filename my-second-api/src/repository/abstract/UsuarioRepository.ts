export interface UsuarioRepository {
  getAll(): Promise<any[]>;
  create(data: any): Promise<any>;
  update(id: string, data: any): Promise<any>;
  delete(id: string): Promise<void>;
  getById(id: string): Promise<any>;
}