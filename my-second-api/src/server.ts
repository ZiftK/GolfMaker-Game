/**
 * @fileoverview Punto de entrada principal de la aplicación.
 * Este módulo configura y arranca el servidor Express, estableciendo
 * los middleware necesarios y montando las rutas de la API.
 * 
 * @module server
 * @requires express
 * @requires dotenv
 * @requires ./routes
 */

import express from 'express';
import dotenv from 'dotenv';
import routes from './routes/';
import swaggerUi from 'swagger-ui-express';
import swaggerJsdoc from 'swagger-jsdoc';

/**
 * Carga las variables de entorno desde el archivo .env
 */
dotenv.config();

/**
 * Instancia principal de la aplicación Express
 * @constant {express.Application} app
 */
const app = express();

/**
 * Puerto en el que se ejecutará el servidor
 * Se obtiene de las variables de entorno o se usa 3000 como valor por defecto
 * @constant {number} port
 */
const port = process.env.PORT || 3000;

/**
 * Configuración de middleware
 * @description Configura el middleware para parsear JSON en las peticiones
 */
app.use(express.json());

/**
 * Swagger: configuración de opciones y ruta de documentación
 * @description Expone la documentación de la API en /docs
 */
const swaggerOptions = {
  definition: {
    openapi: '3.0.0',
    info: {
      title: 'Golf Maker API',
      version: '1.0.0',
      description: 'Documentación de la API de Golf Maker',
    },
  },
  apis: ['./routes/**/*.ts'], // Asegúrate de que coincida con tu estructura real
};

const swaggerDocs = swaggerJsdoc(swaggerOptions);
app.use('/docs', swaggerUi.serve, swaggerUi.setup(swaggerDocs));

/**
 * Configuración de rutas
 * @description Monta todas las rutas de la API bajo la ruta raíz '/'
 */
app.use('/', routes);

/**
 * Inicia el servidor HTTP
 * @description Pone en marcha el servidor en el puerto especificado
 * @listens {number} port - Puerto en el que escucha el servidor
 */
app.listen(port, () => {
  console.log(`✅ Server is running on http://localhost:${port}`);
  console.log(`📚 Swagger docs available at http://localhost:${port}/docs`);
});
