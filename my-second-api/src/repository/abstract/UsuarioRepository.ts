export interface Usuario {
  id_usuario: number;
  nombre_usuario: string;
  email: string;
  contrasenna: string;
  fecha_registro: Date;
  niveles_creados: number;
  niveles_completados: number;
  puntiacion_promedio_recibida: number;
}

export interface UsuarioRepository {
  getAll(): Promise<Usuario[]>;
  create(data: Omit<Usuario, 'id_usuario' | 'fecha_registro'>): Promise<Usuario>;
  update(id: number, data: Partial<Omit<Usuario, 'id_usuario' | 'fecha_registro'>>): Promise<Usuario>;
  delete(id: number): Promise<void>;
  getById(id: number): Promise<Usuario>;
}