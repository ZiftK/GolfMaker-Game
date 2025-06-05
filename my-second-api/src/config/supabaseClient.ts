/**
 * @fileoverview Configuración del cliente Supabase para la aplicación.
 * Este módulo maneja la inicialización y configuración del cliente de Supabase,
 * incluyendo la carga de variables de entorno y validaciones de seguridad.
 * 
 * @module config/supabaseClient
 * @requires @supabase/supabase-js
 * @requires dotenv
 * @requires path
 */

import { createClient } from '@supabase/supabase-js';
import dotenv from 'dotenv';
import path from 'path';

/**
 * Configura las variables de entorno desde el archivo .env
 * El archivo .env debe estar ubicado un nivel arriba de la carpeta config
 * @throws {Error} Si el archivo .env no se puede cargar
 */
dotenv.config({ path: path.resolve(__dirname, '../.env') });

/**
 * URL de la instancia de Supabase
 * @constant {string | undefined}
 * @description Debe ser proporcionada en el archivo .env como SUPABASE_URL
 */
const supabaseUrl = process.env.SUPABASE_URL;

/**
 * Clave de API de Supabase
 * @constant {string | undefined}
 * @description Debe ser proporcionada en el archivo .env como SUPABASE_KEY
 */
const supabaseKey = process.env.SUPABASE_KEY;

/**
 * Validación de las variables de entorno requeridas
 * @throws {Error} Si SUPABASE_URL o SUPABASE_KEY no están definidas
 */
if (!supabaseUrl || !supabaseKey) {
  throw new Error('Las variables de entorno SUPABASE_URL y SUPABASE_KEY son requeridas');
}

/**
 * Cliente de Supabase inicializado
 * @constant {SupabaseClient}
 * @exports supabase
 * @description Instancia del cliente de Supabase configurada con las credenciales proporcionadas
 * @example
 * // Importar el cliente en otros módulos
 * import { supabase } from '../config/supabaseClient';
 * 
 * // Usar el cliente
 * const { data, error } = await supabase
 *   .from('tabla')
 *   .select('*');
 */
export const supabase = createClient(supabaseUrl, supabaseKey);