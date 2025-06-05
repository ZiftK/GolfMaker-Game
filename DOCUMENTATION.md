# Documentación Técnica - GolfMaker API

## Índice
1. [Descripción General](#descripción-general)
2. [Arquitectura](#arquitectura)
3. [Tecnologías y Frameworks](#tecnologías-y-frameworks)
4. [Patrones de Diseño](#patrones-de-diseño)
5. [Estructura del Proyecto](#estructura-del-proyecto)
6. [Endpoints](#endpoints)
7. [Controladores](#controladores)
8. [Repositorios](#repositorios)
9. [Configuración](#configuración)
10. [Seguridad](#seguridad)

## Descripción General
GolfMaker API es un backend desarrollado en TypeScript que proporciona servicios para el juego GolfMaker. La API gestiona usuarios, niveles, calificaciones y estadísticas del juego.

## Arquitectura
La aplicación sigue una arquitectura en capas:
- **Capa de Presentación**: Routes (API Endpoints)
- **Capa de Negocio**: Controllers
- **Capa de Datos**: Repositories
- **Capa de Configuración**: Config

## Tecnologías y Frameworks
- **Node.js**: Entorno de ejecución
- **TypeScript**: Lenguaje de programación principal
- **Express.js**: Framework web
- **Supabase**: Base de datos y autenticación
- **Jest**: Framework de testing
- **dotenv**: Gestión de variables de entorno

## Patrones de Diseño
1. **Repository Pattern**
   - Abstracción de la capa de datos
   - Separación entre lógica de negocio y acceso a datos

2. **Dependency Injection**
   - Inyección de dependencias en controladores
   - Acoplamiento reducido entre componentes

3. **Factory Pattern**
   - Creación de instancias de repositorios
   - Manejo centralizado de dependencias

4. **MVC (Model-View-Controller)**
   - Separación clara de responsabilidades
   - Controllers como intermediarios

## Estructura del Proyecto
```
my-second-api/
├── src/
│   ├── config/
│   │   └── supabaseClient.ts
│   ├── controllers/
│   │   ├── usuariosController.ts
│   │   ├── nivelesController.ts
│   │   ├── ratingController.ts
│   │   └── estadisticasController.ts
│   ├── repository/
│   │   ├── abstract/
│   │   └── concrete/
│   ├── routes/
│   │   ├── usuarios.ts
│   │   ├── niveles.ts
│   │   ├── rating.ts
│   │   └── estadisticas.ts
│   └── server.ts
```

## Endpoints

### Usuarios (/usuarios)
- `GET /usuarios`: Obtiene lista de usuarios
- `GET /usuarios/:id`: Obtiene usuario específico
- `POST /usuarios`: Crea nuevo usuario
- `PUT /usuarios/:id`: Actualiza usuario existente
- `DELETE /usuarios/:id`: Elimina usuario

### Niveles (/niveles)
- `GET /niveles`: Lista todos los niveles
- `GET /niveles/:id`: Obtiene nivel específico
- `POST /niveles`: Crea nuevo nivel
- `PUT /niveles/:id`: Actualiza nivel existente
- `DELETE /niveles/:id`: Elimina nivel

### Rating (/rating)
- `GET /rating/:nivelId`: Obtiene calificaciones de un nivel
- `POST /rating`: Añade nueva calificación
- `PUT /rating/:id`: Actualiza calificación
- `DELETE /rating/:id`: Elimina calificación

### Estadísticas (/estadisticas)
- `GET /estadisticas/usuario/:id`: Estadísticas de usuario
- `GET /estadisticas/nivel/:id`: Estadísticas de nivel
- `POST /estadisticas`: Registra nueva estadística
- `GET /estadisticas/global`: Estadísticas globales

## Controladores

### UsuariosController
- Gestión de autenticación
- CRUD de usuarios
- Validación de datos de usuario
- Manejo de perfiles

### NivelesController
- Creación y edición de niveles
- Validación de estructura de niveles
- Gestión de metadata de niveles
- Control de acceso basado en roles

### RatingController
- Sistema de calificación
- Validación de calificaciones
- Cálculo de promedios
- Prevención de duplicados

### EstadisticasController
- Tracking de métricas
- Agregación de datos
- Generación de reportes
- Análisis de rendimiento

## Repositorios

### Estructura de Repositorios
```
repository/
├── abstract/
│   ├── IUsuarioRepository
│   ├── INivelRepository
│   ├── IRatingRepository
│   └── IEstadisticasRepository
└── concrete/
    ├── SupabaseUsuarioRepository
    ├── SupabaseNivelRepository
    ├── SupabaseRatingRepository
    └── SupabaseEstadisticasRepository
```

### Funcionalidades por Repositorio

#### UsuarioRepository
- Persistencia de datos de usuario
- Consultas de perfil
- Gestión de credenciales
- Histórico de actividades

#### NivelRepository
- Almacenamiento de niveles
- Búsqueda y filtrado
- Versionado de niveles
- Metadata y etiquetas

#### RatingRepository
- Almacenamiento de calificaciones
- Cálculo de promedios
- Histórico de ratings
- Agregación de feedback

#### EstadisticasRepository
- Almacenamiento de métricas
- Agregación de datos
- Consultas temporales
- Generación de reportes

## Configuración

### Variables de Entorno
```
PORT=3000
SUPABASE_URL=your-supabase-url
SUPABASE_KEY=your-supabase-key
NODE_ENV=development/production
```

### Supabase
La configuración de Supabase se realiza en `config/supabaseClient.ts`, gestionando:
- Conexión a la base de datos
- Autenticación
- Políticas de seguridad
- Manejo de errores

## Seguridad

### Medidas Implementadas
1. **Autenticación**
   - JWT tokens
   - Sesiones seguras
   - Renovación de tokens

2. **Autorización**
   - Control de acceso basado en roles
   - Políticas de acceso granular
   - Validación de permisos

3. **Protección de Datos**
   - Encriptación de datos sensibles
   - Sanitización de inputs
   - Validación de datos

4. **Seguridad en API**
   - Rate limiting
   - CORS configurado
   - Validación de headers

### Mejores Prácticas
- Logging seguro
- Manejo de errores consistente
- Actualizaciones regulares
- Monitoreo de seguridad 