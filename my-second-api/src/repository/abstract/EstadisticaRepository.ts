export interface Estadistica {
  id_estadistica: number;
  id_usuario: number;
  id_nivel: number;
  max_muertes: number;
  min_muertes: number;
  max_golpes: number;
  min_golpes: number;
  monedas_recolectadas: number;
  calificacion_general: number;
}

export interface EstadisticaRepository {
  getAll(): Promise<Estadistica[]>;
  create(data: Omit<Estadistica, 'id_estadistica'>): Promise<Estadistica>;
  update(id: number, data: Partial<Omit<Estadistica, 'id_estadistica'>>): Promise<Estadistica>;
  delete(id: number): Promise<void>;
  getByUserId(userId: number): Promise<Estadistica[]>;
  getByLevelId(levelId: number): Promise<Estadistica[]>;
  getByUserIdAndLevelId(userId: number, levelId: number): Promise<Estadistica[]>;
}