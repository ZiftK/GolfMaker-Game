// config/db.js
require('dotenv').config(); // Importante: Carga las variables de entorno desde .env
const mysql = require('mysql2/promise'); // Usa la versión con promesas de mysql2

// Crear el pool de conexiones usando las variables de entorno
const pool = mysql.createPool({
  host: process.env.DB_HOST,       // Lee DB_HOST de .env
  user: process.env.DB_USER,       // Lee DB_USER de .env
  password: process.env.DB_PASSWORD, // Lee DB_PASSWORD de .env
  database: process.env.DB_DATABASE, // Lee DB_DATABASE de .env
  port: process.env.DB_PORT || 3306, // Lee DB_PORT o usa 3306 por defecto
  waitForConnections: true,
  connectionLimit: 10, // Número máximo de conexiones en el pool
  queueLimit: 0
});

// Opcional: Puedes añadir un pequeño test para verificar la conexión al iniciar
// Esto ayuda a saber de inmediato si la configuración es correcta
async function testConnection() {
  let connection;
  try {
    connection = await pool.getConnection();
    console.log(`Conexión a la BD "${process.env.DB_DATABASE}" establecida correctamente.`);
  } catch (err) {
    console.error(`Error al conectar con la BD "${process.env.DB_DATABASE}":`, err.message);
    // Podrías querer detener la app si la BD es crítica: process.exit(1);
  } finally {
    if (connection) connection.release(); // Siempre libera la conexión
  }
}

testConnection(); // Ejecuta la prueba de conexión

// Exportar el pool para que otros archivos puedan usarlo para hacer consultas
module.exports = pool;