/**
 * @fileoverview Define las interfaces para el manejo de usuarios en el sistema.
 * Este módulo contiene la definición de la estructura de datos de un usuario
 * y los métodos abstractos para interactuar con el almacenamiento de usuarios.
 * 
 * @module repository/abstract/UsuarioRepository
 */

/**
 * Interfaz que define la estructura de un usuario en el sistema
 * @interface Usuario
 * @property {number} id_usuario - Identificador único del usuario
 * @property {string} nombre_usuario - Nombre de usuario único en el sistema
 * @property {string} email - Correo electrónico del usuario
 * @property {string} contrasenna - Contraseña encriptada del usuario
 * @property {Date} fecha_registro - Fecha y hora de registro del usuario
 * @property {number} niveles_creados - Cantidad de niveles creados por el usuario
 * @property {number} niveles_completados - Cantidad de niveles completados por el usuario
 * @property {number} puntiacion_promedio_recibida - Promedio de puntuaciones recibidas en sus niveles
 */
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

/**
 * Interfaz que define las operaciones disponibles para el manejo de usuarios
 * @interface UsuarioRepository
 */
export interface UsuarioRepository {
  /**
   * Obtiene todos los usuarios registrados en el sistema
   * @returns {Promise<Usuario[]>} Promesa que resuelve a un array de usuarios
   */
  getAll(): Promise<Usuario[]>;

  /**
   * Crea un nuevo usuario en el sistema
   * @param {Omit<Usuario, 'id_usuario' | 'fecha_registro'>} data - Datos del usuario a crear
   * @returns {Promise<Usuario>} Promesa que resuelve al usuario creado
   */
  create(data: Omit<Usuario, 'id_usuario' | 'fecha_registro'>): Promise<Usuario>;

  /**
   * Actualiza un usuario existente
   * @param {number} id - Identificador del usuario a actualizar
   * @param {Partial<Omit<Usuario, 'id_usuario' | 'fecha_registro'>>} data - Datos parciales para actualizar
   * @returns {Promise<Usuario>} Promesa que resuelve al usuario actualizado
   */
  update(id: number, data: Partial<Omit<Usuario, 'id_usuario' | 'fecha_registro'>>): Promise<Usuario>;

  /**
   * Elimina un usuario del sistema
   * @param {number} id - Identificador del usuario a eliminar
   * @returns {Promise<void>} Promesa que resuelve cuando el usuario ha sido eliminado
   */
  delete(id: number): Promise<void>;

  /**
   * Obtiene un usuario por su ID
   * @param {number} id - Identificador del usuario a buscar
   * @returns {Promise<Usuario>} Promesa que resuelve al usuario encontrado
   */
  getById(id: number): Promise<Usuario>;

  /**
   * Busca un usuario por su nombre de usuario
   * @param {string} nombreUsuario - Nombre de usuario a buscar
   * @returns {Promise<Usuario | null>} Promesa que resuelve al usuario encontrado o null si no existe
   */
  getByUsername(nombreUsuario: string): Promise<Usuario | null>;
}