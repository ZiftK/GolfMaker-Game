/**
 * @fileoverview Pruebas de integración para las rutas de la API.
 * Este módulo contiene pruebas end-to-end para todas las rutas HTTP de la API,
 * verificando el comportamiento correcto de los endpoints y sus respuestas.
 * 
 * @module tests/routes.test
 * @requires supertest
 * @requires express
 * @requires ../routes/usuarios
 * @requires ../routes/niveles
 * @requires ../routes/rating
 * @requires ../routes/estadisticas
 */

import request from 'supertest';
import express from 'express';
import usuariosRoutes from '../routes/usuarios';
import nivelesRoutes from '../routes/niveles';
import ratingRoutes from '../routes/rating';
import estadisticasRoutes from '../routes/estadisticas';

/**
 * Mocks de repositorios comentados para referencia futura
 * Estos mocks pueden ser descomentados y modificados según las necesidades de prueba
 */
// jest.mock('../repository/concrete/SupabaseUsuarioRepository', () => ({
//   SupabaseUsuarioRepository: jest.fn().mockImplementation(() => ({
//     getAll: jest.fn().mockResolvedValue([]),
//     create: jest.fn().mockResolvedValue({ id: '1', nombre: 'Test User', email: 'test@example.com' }),
//     getById: jest.fn().mockResolvedValue({ id: '1', nombre: 'Test User', email: 'test@example.com' }),
//     update: jest.fn().mockResolvedValue({ id: '1', nombre: 'Updated User', email: 'test@example.com' }),
//     delete: jest.fn().mockResolvedValue(undefined)
//   }))
// }));

// jest.mock('../repository/concrete/SupabaseNivelRepository', () => ({
//   SupabaseNivelRepository: jest.fn().mockImplementation(() => ({
//     getAll: jest.fn().mockResolvedValue([]),
//     create: jest.fn().mockResolvedValue({ id: '1', nombre: 'Test Level', descripcion: 'Test Description' }),
//     getById: jest.fn().mockResolvedValue({ id: '1', nombre: 'Test Level', descripcion: 'Test Description' }),
//     getByUserId: jest.fn().mockResolvedValue([]),
//     update: jest.fn().mockResolvedValue({ id: '1', nombre: 'Updated Level', descripcion: 'Updated Description' }),
//     delete: jest.fn().mockResolvedValue(undefined)
//   }))
// }));

// jest.mock('../repository/concrete/SupabaseRatingRepository', () => ({
//   SupabaseRatingRepository: jest.fn().mockImplementation(() => ({
//     getAll: jest.fn().mockResolvedValue([]),
//     create: jest.fn().mockResolvedValue({ id: '1', rating: 5, comentario: 'Great level!' }),
//     getAverageRatingByLevel: jest.fn().mockResolvedValue(4.5)
//   }))
// }));

// jest.mock('../repository/concrete/SupabaseEstadisticaRepository', () => ({
//   SupabaseEstadisticaRepository: jest.fn().mockImplementation(() => ({
//     getAll: jest.fn().mockResolvedValue([]),
//     create: jest.fn().mockResolvedValue({ id: '1', tiempo: 120, intentos: 5 }),
//     getByUserId: jest.fn().mockResolvedValue([]),
//     getByLevelId: jest.fn().mockResolvedValue([]),
//     getByUserIdAndLevelId: jest.fn().mockResolvedValue({ id: '1', tiempo: 120, intentos: 5 }),
//     update: jest.fn().mockResolvedValue({ id: '1', tiempo: 150, intentos: 6 }),
//     delete: jest.fn().mockResolvedValue(undefined)
//   }))
// }));

/**
 * Configuración de la aplicación Express para pruebas
 * @constant {express.Application} app
 */
const app = express();
app.use(express.json());

/**
 * Montaje de rutas para pruebas
 */
app.use('/usuarios', usuariosRoutes);
app.use('/niveles', nivelesRoutes);
app.use('/rating', ratingRoutes);
app.use('/estadisticas', estadisticasRoutes);

/**
 * Suite principal de pruebas de la API
 * @description Prueba todos los endpoints principales de la API
 */
describe('API Routes Tests', () => {
  /**
   * Pruebas de rutas de Usuarios
   * @description Verifica todas las operaciones CRUD y endpoints específicos de usuarios
   */
  describe('Usuarios Routes', () => {
    it('GET /usuarios should return all users', async () => {
      const response = await request(app).get('/usuarios');
      expect(response.status).toBe(200);
    });

    it('POST /usuarios should create a new user', async () => {
      const userData = {
        nombre: 'Test User',
        email: 'test@example.com'
      };
      const response = await request(app).post('/usuarios').send(userData);
      expect(response.status).toBe(201);
    });

    it('GET /usuarios/:id should return a specific user', async () => {
      const response = await request(app).get('/usuarios/1');
      expect(response.status).toBe(200);
    });

    it('GET /usuarios/:id/niveles-creados should return user created levels', async () => {
      const response = await request(app).get('/usuarios/1/niveles-creados');
      expect(response.status).toBe(200);
    });

    it('GET /usuarios/:id/niveles-jugados should return user played levels', async () => {
      const response = await request(app).get('/usuarios/1/niveles-jugados');
      expect(response.status).toBe(200);
    });
  });

  /**
   * Pruebas de rutas de Niveles
   * @description Verifica todas las operaciones CRUD y endpoints específicos de niveles
   */
  describe('Niveles Routes', () => {
    it('GET /niveles should return all levels', async () => {
      const response = await request(app).get('/niveles');
      expect(response.status).toBe(200);
    });

    it('POST /niveles should create a new level', async () => {
      const levelData = {
        nombre: 'Test Level',
        descripcion: 'Test Description'
      };
      const response = await request(app).post('/niveles').send(levelData);
      expect(response.status).toBe(201);
    });

    it('GET /niveles/:id should return a specific level', async () => {
      const response = await request(app).get('/niveles/1');
      expect(response.status).toBe(200);
    });

    it('GET /niveles/user/:userId should return levels by user', async () => {
      const response = await request(app).get('/niveles/user/1');
      expect(response.status).toBe(200);
    });

    it('GET /niveles/:id/comentarios should return level comments', async () => {
      const response = await request(app).get('/niveles/1/comentarios');
      expect(response.status).toBe(200);
    });
  });

  /**
   * Pruebas de rutas de Calificaciones
   * @description Verifica operaciones de calificación y cálculo de promedios
   */
  describe('Rating Routes', () => {
    it('GET /rating should return all ratings', async () => {
      const response = await request(app).get('/rating');
      expect(response.status).toBe(200);
    });

    it('POST /rating should create a new rating', async () => {
      const ratingData = {
        rating: 5,
        comentario: 'Great level!'
      };
      const response = await request(app).post('/rating').send(ratingData);
      expect(response.status).toBe(201);
    });

    it('GET /rating/level/:levelId/average should return average rating', async () => {
      const response = await request(app).get('/rating/level/1/average');
      expect(response.status).toBe(200);
    });
  });

  /**
   * Pruebas de rutas de Estadísticas
   * @description Verifica operaciones CRUD y consultas específicas de estadísticas
   */
  describe('Estadisticas Routes', () => {
    it('GET /estadisticas should return all statistics', async () => {
      const response = await request(app).get('/estadisticas');
      expect(response.status).toBe(200);
    });

    it('POST /estadisticas should create new statistics', async () => {
      const statsData = {
        tiempo: 120,
        intentos: 5
      };
      const response = await request(app).post('/estadisticas').send(statsData);
      expect(response.status).toBe(201);
    });

    it('GET /estadisticas/user/:userId should return user statistics', async () => {
      const response = await request(app).get('/estadisticas/user/1');
      expect(response.status).toBe(200);
    });

    it('GET /estadisticas/level/:levelId should return level statistics', async () => {
      const response = await request(app).get('/estadisticas/level/1');
      expect(response.status).toBe(200);
    });

    it('GET /estadisticas/user/:userId/level/:levelId should return specific statistics', async () => {
      const response = await request(app).get('/estadisticas/user/1/level/1');
      expect(response.status).toBe(200);
    });
  });
}); 