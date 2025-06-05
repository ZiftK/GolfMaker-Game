/**
 * @fileoverview Define las interfaces para el manejo de estadísticas en el sistema.
 * Este módulo contiene la definición de la estructura de datos de una estadística
 * y los métodos abstractos para interactuar con el almacenamiento de estadísticas.
 * 
 * @module repository/abstract/EstadisticaRepository
 */

/**
 * Interfaz que define la estructura de una estadística en el sistema
 * @interface Estadistica
 * @property {number} id_estadistica - Identificador único de la estadística
 * @property {number} id_usuario - Identificador del usuario al que pertenece la estadística
 * @property {number} id_nivel - Identificador del nivel al que pertenece la estadística
 * @property {number} max_muertes - Número máximo de muertes registradas
 * @property {number} min_muertes - Número mínimo de muertes registradas
 * @property {number} max_golpes - Número máximo de golpes registrados
 * @property {number} min_golpes - Número mínimo de golpes registrados
 * @property {number} monedas_recolectadas - Cantidad de monedas recolectadas
 * @property {number} calificacion_general - Calificación general del nivel (0-5)
 */
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

/**
 * Interfaz que define las operaciones disponibles para el manejo de estadísticas
 * @interface EstadisticaRepository
 */
export interface EstadisticaRepository {
  /**
   * Obtiene todas las estadísticas registradas en el sistema
   * @returns {Promise<Estadistica[]>} Promesa que resuelve a un array de estadísticas
   */
  getAll(): Promise<Estadistica[]>;

  /**
   * Crea una nueva estadística en el sistema
   * @param {Omit<Estadistica, 'id_estadistica'>} data - Datos de la estadística a crear
   * @returns {Promise<Estadistica>} Promesa que resuelve a la estadística creada
   */
  create(data: Omit<Estadistica, 'id_estadistica'>): Promise<Estadistica>;

  /**
   * Actualiza una estadística existente
   * @param {number} id - Identificador de la estadística a actualizar
   * @param {Partial<Omit<Estadistica, 'id_estadistica'>>} data - Datos parciales para actualizar
   * @returns {Promise<Estadistica>} Promesa que resuelve a la estadística actualizada
   */
  update(id: number, data: Partial<Omit<Estadistica, 'id_estadistica'>>): Promise<Estadistica>;

  /**
   * Elimina una estadística del sistema
   * @param {number} id - Identificador de la estadística a eliminar
   * @returns {Promise<void>} Promesa que resuelve cuando la estadística ha sido eliminada
   */
  delete(id: number): Promise<void>;

  /**
   * Obtiene todas las estadísticas de un usuario específico
   * @param {number} userId - Identificador del usuario
   * @returns {Promise<Estadistica[]>} Promesa que resuelve a un array de estadísticas del usuario
   */
  getByUserId(userId: number): Promise<Estadistica[]>;

  /**
   * Obtiene todas las estadísticas de un nivel específico
   * @param {number} levelId - Identificador del nivel
   * @returns {Promise<Estadistica[]>} Promesa que resuelve a un array de estadísticas del nivel
   */
  getByLevelId(levelId: number): Promise<Estadistica[]>;

  /**
   * Obtiene todas las estadísticas de un usuario en un nivel específico
   * @param {number} userId - Identificador del usuario
   * @param {number} levelId - Identificador del nivel
   * @returns {Promise<Estadistica[]>} Promesa que resuelve a un array de estadísticas del usuario en el nivel
   */
  getByUserIdAndLevelId(userId: number, levelId: number): Promise<Estadistica[]>;
}