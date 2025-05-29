export type Dificultad = 'Facil' | 'Medio' | 'Dificil';

export interface Nivel {
  id_nivel: number;
  id_usuario: number;
  nombre: string;
  fecha_creacion: Date;
  dificultad: Dificultad;
  descripcion: string | null;
  rating_promedio: number;
  jugado_veces: number;
  completado_veces: number;
  estructura_nivel: string | null;
  cantidad_monedas: number;
}

export interface NivelRepository {
  getAll(): Promise<Nivel[]>;
  create(data: Omit<Nivel, 'id_nivel' | 'fecha_creacion' | 'rating_promedio' | 'jugado_veces' | 'completado_veces'>): Promise<Nivel>;
  update(id: number, data: Partial<Omit<Nivel, 'id_nivel' | 'fecha_creacion'>>): Promise<Nivel>;
  delete(id: number): Promise<void>;
  getByUserId(usuarioId: number): Promise<Nivel[]>;
  getById(id: number): Promise<Nivel | null>;
}