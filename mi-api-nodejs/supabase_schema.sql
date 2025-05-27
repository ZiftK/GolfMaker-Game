-- Crear la base de datos (si no existe)
-- Nota: En Supabase no es necesario crear la base de datos manualmente.

-- Tabla de Usuarios
CREATE TABLE IF NOT EXISTS Usuarios (
    id_usuario SERIAL PRIMARY KEY,
    nombre_usuario VARCHAR(50) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    contrasenna VARCHAR(255) NOT NULL, -- Recuerda que aquí guardas el HASH
    fecha_registro TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    niveles_creados INT DEFAULT 0,
    niveles_completados INT DEFAULT 0,
    puntiacion_promedio_recibida FLOAT DEFAULT 0
);

-- Crear tipo ENUM para dificultad
DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'dificultad_enum') THEN
        CREATE TYPE dificultad_enum AS ENUM ('fácil', 'medio', 'difícil');
    END IF;
END $$;

-- Tabla de Niveles
CREATE TABLE IF NOT EXISTS Niveles (
    id_nivel SERIAL PRIMARY KEY,
    id_usuario INT NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    fecha_creacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    dificultad dificultad_enum NOT NULL,
    descripcion TEXT,
    rating_promedio FLOAT DEFAULT 0,
    jugado_veces INT DEFAULT 0,
    completado_veces INT DEFAULT 0,
    estructura_nivel TEXT,
    cantidad_moneas INT DEFAULT 0,
    FOREIGN KEY (id_usuario) REFERENCES Usuarios(id_usuario) ON DELETE CASCADE
);

-- Tabla de Ratings
CREATE TABLE IF NOT EXISTS Rating (
    id_rating SERIAL PRIMARY KEY,
    id_usuario INT NOT NULL,
    id_nivel INT NOT NULL,
    calificacion INT CHECK (calificacion BETWEEN 1 AND 5),
    comentario TEXT,
    fecha_rating TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (id_usuario) REFERENCES Usuarios(id_usuario) ON DELETE CASCADE,
    FOREIGN KEY (id_nivel) REFERENCES Niveles(id_nivel) ON DELETE CASCADE
);

-- Tabla de EstadisticasJugadorMapa
CREATE TABLE IF NOT EXISTS EstadisticasJugadorMapa (
    id_estadistica SERIAL PRIMARY KEY,
    id_usuario INT NOT NULL,
    id_nivel INT NOT NULL,
    max_muertes INT DEFAULT 0,
    min_muertes INT DEFAULT 0,
    max_golpes INT DEFAULT 0,
    min_golpes INT DEFAULT 0,
    monedas_recolectadas INT DEFAULT 0,
    calificacion_general FLOAT CHECK (calificacion_general BETWEEN 0 AND 5) DEFAULT 0,
    FOREIGN KEY (id_usuario) REFERENCES Usuarios(id_usuario) ON DELETE CASCADE,
    FOREIGN KEY (id_nivel) REFERENCES Niveles(id_nivel) ON DELETE CASCADE
);

-- Puedes añadir aquí INSERTs iniciales si quieres datos de ejemplo
-- INSERT INTO Usuarios (nombre_usuario, email, contrasenna) VALUES ('usuario1', 'email@example.com', 'hash');