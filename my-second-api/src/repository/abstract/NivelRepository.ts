/**
 * @fileoverview Define las interfaces para el manejo de niveles en el sistema.
 * Este módulo contiene la definición de la estructura de datos de un nivel,
 * sus tipos asociados y los métodos abstractos para interactuar con el almacenamiento de niveles.
 * 
 * @module repository/abstract/NivelRepository
 */

/**
 * Tipo que define los posibles valores de dificultad para un nivel
 * @typedef {('Facil' | 'Medio' | 'Dificil')} Dificultad
 */
export type Dificultad = 'Facil' | 'Medio' | 'Dificil';

/**
 * Interfaz que define la estructura de un nivel en el sistema
 * @interface Nivel
 * @property {number} id_nivel - Identificador único del nivel
 * @property {number} id_usuario - Identificador del usuario creador del nivel
 * @property {string} nombre - Nombre del nivel
 * @property {Date} fecha_creacion - Fecha y hora de creación del nivel
 * @property {Dificultad} dificultad - Nivel de dificultad del nivel
 * @property {string | null} descripcion - Descripción opcional del nivel
 * @property {number} rating_promedio - Promedio de calificaciones recibidas
 * @property {number} jugado_veces - Cantidad de veces que el nivel ha sido jugado
 * @property {number} completado_veces - Cantidad de veces que el nivel ha sido completado
 * @property {string | null} estructura_nivel - Estructura del nivel en formato JSON
 * @property {number} cantidad_monedas - Cantidad de monedas disponibles en el nivel
 */
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

/**
 * Interfaz que define las operaciones disponibles para el manejo de niveles
 * @interface NivelRepository
 */
export interface NivelRepository {
  /**
   * Obtiene todos los niveles registrados en el sistema
   * @returns {Promise<Nivel[]>} Promesa que resuelve a un array de niveles
   */
  getAll(): Promise<Nivel[]>;

  /**
   * Crea un nuevo nivel en el sistema
   * @param {Omit<Nivel, 'id_nivel' | 'fecha_creacion' | 'rating_promedio' | 'jugado_veces' | 'completado_veces'>} data - Datos del nivel a crear
   * @returns {Promise<Nivel>} Promesa que resuelve al nivel creado
   */
  create(data: Omit<Nivel, 'id_nivel' | 'fecha_creacion' | 'rating_promedio' | 'jugado_veces' | 'completado_veces'>): Promise<Nivel>;

  /**
   * Actualiza un nivel existente
   * @param {number} id - Identificador del nivel a actualizar
   * @param {Partial<Omit<Nivel, 'id_nivel' | 'fecha_creacion'>>} data - Datos parciales para actualizar
   * @returns {Promise<Nivel>} Promesa que resuelve al nivel actualizado
   */
  update(id: number, data: Partial<Omit<Nivel, 'id_nivel' | 'fecha_creacion'>>): Promise<Nivel>;

  /**
   * Elimina un nivel del sistema
   * @param {number} id - Identificador del nivel a eliminar
   * @returns {Promise<void>} Promesa que resuelve cuando el nivel ha sido eliminado
   */
  delete(id: number): Promise<void>;

  /**
   * Obtiene todos los niveles creados por un usuario específico
   * @param {number} usuarioId - Identificador del usuario
   * @returns {Promise<Nivel[]>} Promesa que resuelve a un array de niveles del usuario
   */
  getByUserId(usuarioId: number): Promise<Nivel[]>;

  /**
   * Obtiene un nivel por su ID
   * @param {number} id - Identificador del nivel a buscar
   * @returns {Promise<Nivel | null>} Promesa que resuelve al nivel encontrado o null si no existe
   */
  getById(id: number): Promise<Nivel | null>;
}