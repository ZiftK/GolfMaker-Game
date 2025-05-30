/**
 * @fileoverview Define las interfaces para el manejo de calificaciones en el sistema.
 * Este módulo contiene la definición de la estructura de datos de una calificación
 * y los métodos abstractos para interactuar con el almacenamiento de calificaciones.
 * 
 * @module repository/abstract/RatingRepository
 */

/**
 * Interfaz que define la estructura de una calificación en el sistema
 * @interface Rating
 * @property {number} id_rating - Identificador único de la calificación
 * @property {number} id_usuario - Identificador del usuario que realizó la calificación
 * @property {number} id_nivel - Identificador del nivel que se está calificando
 * @property {number} calificacion - Valor numérico de la calificación (1-5)
 * @property {string} comentario - Comentario opcional sobre el nivel
 * @property {Date} fecha_rating - Fecha y hora en que se realizó la calificación
 */
export interface Rating {
  id_rating: number;
  id_usuario: number;
  id_nivel: number;
  calificacion: number;
  comentario: string;
  fecha_rating: Date;
}

/**
 * Interfaz que define las operaciones disponibles para el manejo de calificaciones
 * @interface RatingRepository
 */
export interface RatingRepository {
  /**
   * Obtiene todas las calificaciones almacenadas en el sistema
   * @returns {Promise<Rating[]>} Promesa que resuelve a un array de calificaciones
   */
  getAll(): Promise<Rating[]>;

  /**
   * Crea una nueva calificación en el sistema
   * @param {Omit<Rating, 'id_rating' | 'fecha_rating'>} data - Datos de la calificación a crear
   * @returns {Promise<Rating>} Promesa que resuelve a la calificación creada
   */
  create(data: Omit<Rating, 'id_rating' | 'fecha_rating'>): Promise<Rating>;

  /**
   * Actualiza una calificación existente
   * @param {number} id - Identificador de la calificación a actualizar
   * @param {Partial<Omit<Rating, 'id_rating' | 'fecha_rating'>>} data - Datos parciales para actualizar
   * @returns {Promise<Rating>} Promesa que resuelve a la calificación actualizada
   */
  update(id: number, data: Partial<Omit<Rating, 'id_rating' | 'fecha_rating'>>): Promise<Rating>;

  /**
   * Elimina una calificación del sistema
   * @param {number} id - Identificador de la calificación a eliminar
   * @returns {Promise<void>} Promesa que resuelve cuando la calificación ha sido eliminada
   */
  delete(id: number): Promise<void>;

  /**
   * Calcula el promedio de calificaciones para un nivel específico
   * @param {number} levelId - Identificador del nivel
   * @returns {Promise<number>} Promesa que resuelve al promedio de calificaciones
   */
  getAverageRatingByLevel(levelId: number): Promise<number>;
}