import { SupabaseUsuarioRepository } from '../repository/concrete/SupabaseUsuarioRepository';
import { SupabaseNivelRepository } from '../repository/concrete/SupabaseNivelRepository';
import { SupabaseRatingRepository } from '../repository/concrete/SupabaseRatingRepository';
import { SupabaseEstadisticaRepository } from '../repository/concrete/SupabaseEstadisticaRepository';
import { supabase } from '../config/supabaseClient';
import dotenv from 'dotenv';
import path from 'path';

// Cargar variables de entorno desde el archivo .env en la raíz del proyecto
dotenv.config({ path: path.resolve(__dirname, '../../.env') });

// Verificar que las variables de entorno estén configuradas
if (!process.env.SUPABASE_URL || !process.env.SUPABASE_KEY) {
  throw new Error('Las variables de entorno SUPABASE_URL y SUPABASE_KEY son requeridas');
}

console.log('Configuración de Supabase:');
console.log('URL:', process.env.SUPABASE_URL);
console.log('Key:', process.env.SUPABASE_KEY ? 'Configurada' : 'No configurada');

describe('Repository Tests', () => {
  let usuarioRepository: SupabaseUsuarioRepository;
  let nivelRepository: SupabaseNivelRepository;
  let ratingRepository: SupabaseRatingRepository;
  let estadisticaRepository: SupabaseEstadisticaRepository;

  beforeEach(() => {
    usuarioRepository = new SupabaseUsuarioRepository();
    nivelRepository = new SupabaseNivelRepository();
    ratingRepository = new SupabaseRatingRepository();
    estadisticaRepository = new SupabaseEstadisticaRepository();
  });

  describe('UsuarioRepository', () => {
    it('should get all usuarios', async () => {
      const result = await usuarioRepository.getAll();
      console.log('\n=== Usuarios obtenidos ===');
      console.log(JSON.stringify(result, null, 2));
      expect(Array.isArray(result)).toBe(true);
    });

    it('should create a new usuario', async () => {
      const userData = {
        nombre: 'Test User ' + Date.now(),
        email: `test${Date.now()}@example.com`
      };
      const result = await usuarioRepository.create(userData);
      console.log('\n=== Usuario creado ===');
      console.log(JSON.stringify(result, null, 2));
      expect(result).toHaveProperty('id_usuario');
      expect(result.nombre).toBe(userData.nombre);
      expect(result.email).toBe(userData.email);
    });

    it('should get usuario by id', async () => {
      // Primero creamos un usuario para obtener su ID
      const userData = {
        nombre: 'Test User ' + Date.now(),
        email: `test${Date.now()}@example.com`
      };
      const createdUser = await usuarioRepository.create(userData);
      
      const result = await usuarioRepository.getById(createdUser.id_usuario);
      console.log('\n=== Usuario por ID ===');
      console.log(JSON.stringify(result, null, 2));
      expect(result).toHaveProperty('id_usuario', createdUser.id_usuario);
    });
  });

  describe('NivelRepository', () => {
    it('should get all niveles', async () => {
      const result = await nivelRepository.getAll();
      console.log('\n=== Niveles obtenidos ===');
      console.log(JSON.stringify(result, null, 2));
      expect(Array.isArray(result)).toBe(true);
    });

    it('should create a new nivel', async () => {
      const nivelData = {
        nombre: 'Test Level ' + Date.now(),
        descripcion: 'Test Description ' + Date.now()
      };
      const result = await nivelRepository.create(nivelData);
      console.log('\n=== Nivel creado ===');
      console.log(JSON.stringify(result, null, 2));
      expect(result).toHaveProperty('id_nivel');
      expect(result.nombre).toBe(nivelData.nombre);
      expect(result.descripcion).toBe(nivelData.descripcion);
    });

    it('should get nivel by id', async () => {
      // Primero creamos un nivel para obtener su ID
      const nivelData = {
        nombre: 'Test Level ' + Date.now(),
        descripcion: 'Test Description ' + Date.now()
      };
      const createdNivel = await nivelRepository.create(nivelData);
      
      const result = await nivelRepository.getById(createdNivel.id_nivel);
      console.log('\n=== Nivel por ID ===');
      console.log(JSON.stringify(result, null, 2));
      expect(result).toHaveProperty('id_nivel', createdNivel.id_nivel);
    });
  });

  describe('RatingRepository', () => {
    it('should get all ratings', async () => {
      const result = await ratingRepository.getAll();
      console.log('\n=== Ratings obtenidos ===');
      console.log(JSON.stringify(result, null, 2));
      expect(Array.isArray(result)).toBe(true);
    });

    it('should create a new rating', async () => {
      // Primero creamos un nivel para poder calificarlo
      const nivelData = {
        nombre: 'Test Level ' + Date.now(),
        descripcion: 'Test Description ' + Date.now()
      };
      const createdNivel = await nivelRepository.create(nivelData);

      const ratingData = {
        rating: 5,
        comentario: 'Great level! ' + Date.now(),
        id_nivel: createdNivel.id_nivel
      };
      const result = await ratingRepository.create(ratingData);
      console.log('\n=== Rating creado ===');
      console.log(JSON.stringify(result, null, 2));
      expect(result).toHaveProperty('id_rating');
      expect(result.rating).toBe(ratingData.rating);
    });

    it('should get average rating by level', async () => {
      // Primero creamos un nivel y le agregamos algunas calificaciones
      const nivelData = {
        nombre: 'Test Level ' + Date.now(),
        descripcion: 'Test Description ' + Date.now()
      };
      const createdNivel = await nivelRepository.create(nivelData);

      const ratings = [
        { rating: 5, comentario: 'Great!', id_nivel: createdNivel.id_nivel },
        { rating: 4, comentario: 'Good!', id_nivel: createdNivel.id_nivel },
        { rating: 3, comentario: 'OK', id_nivel: createdNivel.id_nivel }
      ];

      for (const rating of ratings) {
        await ratingRepository.create(rating);
      }

      const result = await ratingRepository.getAverageRatingByLevel(createdNivel.id_nivel);
      console.log('\n=== Promedio de rating por nivel ===');
      console.log(JSON.stringify(result, null, 2));
      expect(typeof result).toBe('number');
      expect(result).toBeGreaterThanOrEqual(0);
      expect(result).toBeLessThanOrEqual(5);
    });
  });

  describe('EstadisticaRepository', () => {
    it('should get all estadisticas', async () => {
      const result = await estadisticaRepository.getAll();
      console.log('\n=== Estadísticas obtenidas ===');
      console.log(JSON.stringify(result, null, 2));
      expect(Array.isArray(result)).toBe(true);
    });

    it('should create a new estadistica', async () => {
      // Primero creamos un nivel para poder registrar estadísticas
      const nivelData = {
        nombre: 'Test Level ' + Date.now(),
        descripcion: 'Test Description ' + Date.now()
      };
      const createdNivel = await nivelRepository.create(nivelData);

      const statsData = {
        tiempo: 120,
        intentos: 5,
        id_nivel: createdNivel.id_nivel
      };
      const result = await estadisticaRepository.create(statsData);
      console.log('\n=== Estadística creada ===');
      console.log(JSON.stringify(result, null, 2));
      expect(result).toHaveProperty('id_estadistica');
      expect(result.tiempo).toBe(statsData.tiempo);
      expect(result.intentos).toBe(statsData.intentos);
    });

    it('should get estadisticas by user id', async () => {
      // Primero creamos un usuario y algunas estadísticas
      const userData = {
        nombre: 'Test User ' + Date.now(),
        email: `test${Date.now()}@example.com`
      };
      const createdUser = await usuarioRepository.create(userData);

      const nivelData = {
        nombre: 'Test Level ' + Date.now(),
        descripcion: 'Test Description ' + Date.now()
      };
      const createdNivel = await nivelRepository.create(nivelData);

      const statsData = {
        tiempo: 120,
        intentos: 5,
        id_nivel: createdNivel.id_nivel,
        id_usuario: createdUser.id_usuario
      };
      await estadisticaRepository.create(statsData);

      const result = await estadisticaRepository.getByUserId(createdUser.id_usuario);
      console.log('\n=== Estadísticas por usuario ===');
      console.log(JSON.stringify(result, null, 2));
      expect(Array.isArray(result)).toBe(true);
    });

    it('should get estadisticas by level id', async () => {
      // Primero creamos un nivel y algunas estadísticas
      const nivelData = {
        nombre: 'Test Level ' + Date.now(),
        descripcion: 'Test Description ' + Date.now()
      };
      const createdNivel = await nivelRepository.create(nivelData);

      const statsData = {
        tiempo: 120,
        intentos: 5,
        id_nivel: createdNivel.id_nivel
      };
      await estadisticaRepository.create(statsData);

      const result = await estadisticaRepository.getByLevelId(createdNivel.id_nivel);
      console.log('\n=== Estadísticas por nivel ===');
      console.log(JSON.stringify(result, null, 2));
      expect(Array.isArray(result)).toBe(true);
    });
  });
}); 