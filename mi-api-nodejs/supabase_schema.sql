-- Crear la base de datos (si no existe)
CREATE DATABASE IF NOT EXISTS GolfMaker;

-- Usar la base de datos
USE GolfMaker;

-- Tabla de Usuarios
CREATE TABLE IF NOT EXISTS usuarios (
    id_usuario SERIAL PRIMARY KEY,
    nombre_usuario VARCHAR(50) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    contraseña VARCHAR(255) NOT NULL,
    fecha_registro TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    niveles_creados INTEGER DEFAULT 0,
    niveles_completados INTEGER DEFAULT 0,
    puntuacion_total INTEGER DEFAULT 0,
    monedas INTEGER DEFAULT 0
);

-- Tabla de Niveles
CREATE TABLE IF NOT EXISTS niveles (
    id_nivel SERIAL PRIMARY KEY,
    id_usuario INTEGER NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    fecha_creacion TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    dificultad VARCHAR(10) CHECK (dificultad IN ('fácil', 'medio', 'difícil')) NOT NULL,
    par INTEGER NOT NULL,
    descripcion TEXT,
    rating_promedio FLOAT DEFAULT 0,
    jugado_veces INTEGER DEFAULT 0,
    completado_veces INTEGER DEFAULT 0,
    monedas_recompensa INTEGER DEFAULT 0,
    estructura_nivel TEXT,
    CONSTRAINT fk_usuario
        FOREIGN KEY (id_usuario)
        REFERENCES usuarios(id_usuario)
        ON DELETE CASCADE
);

-- Tabla de Ratings
CREATE TABLE IF NOT EXISTS rating (
    id_rating SERIAL PRIMARY KEY,
    id_usuario INTEGER NOT NULL,
    id_nivel INTEGER NOT NULL,
    calificacion INTEGER CHECK (calificacion BETWEEN 1 AND 5),
    comentario TEXT,
    fecha_rating TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    monedas_bonus INTEGER DEFAULT 0,
    CONSTRAINT fk_usuario_rating
        FOREIGN KEY (id_usuario)
        REFERENCES usuarios(id_usuario)
        ON DELETE CASCADE,
    CONSTRAINT fk_nivel_rating
        FOREIGN KEY (id_nivel)
        REFERENCES niveles(id_nivel)
        ON DELETE CASCADE
);

-- Tabla de PowerUps
CREATE TABLE IF NOT EXISTS powerups (
    id_powerup SERIAL PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    descripcion TEXT,
    duracion INTEGER DEFAULT 0,
    cantidad_uso INTEGER DEFAULT 1,
    efecto VARCHAR(20) CHECK (efecto IN ('rebote_extra', 'vuelo', 'freno', 'aceleracion')) NOT NULL,
    desbloqueo TEXT
);

-- Tabla intermedia Usuario_PowerUp
CREATE TABLE IF NOT EXISTS usuario_powerup (
    id_usuario INTEGER NOT NULL,
    id_powerup INTEGER NOT NULL,
    cantidad_disponible INTEGER DEFAULT 1,
    PRIMARY KEY (id_usuario, id_powerup),
    CONSTRAINT fk_usuario_powerup
        FOREIGN KEY (id_usuario)
        REFERENCES usuarios(id_usuario)
        ON DELETE CASCADE,
    CONSTRAINT fk_powerup
        FOREIGN KEY (id_powerup)
        REFERENCES powerups(id_powerup)
        ON DELETE CASCADE
);

-- Tabla de Tienda
CREATE TABLE IF NOT EXISTS tienda (
    id_item SERIAL PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    tipo VARCHAR(10) CHECK (tipo IN ('monedas', 'power-up')) NOT NULL,
    precio_monedas INTEGER DEFAULT 0,
    precio_real DECIMAL(10,2) DEFAULT 0,
    descripcion TEXT
);

-- Tabla de Transacciones
CREATE TABLE IF NOT EXISTS transacciones (
    id_transaccion SERIAL PRIMARY KEY,
    id_usuario INTEGER NOT NULL,
    id_item INTEGER NOT NULL,
    monto_monedas INTEGER DEFAULT 0,
    monto_real DECIMAL(10,2) DEFAULT 0,
    fecha TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_usuario_transaccion
        FOREIGN KEY (id_usuario)
        REFERENCES usuarios(id_usuario)
        ON DELETE CASCADE,
    CONSTRAINT fk_item
        FOREIGN KEY (id_item)
        REFERENCES tienda(id_item)
        ON DELETE CASCADE
);

-- Puedes añadir aquí INSERTs iniciales si quieres datos de ejemplo
-- INSERT INTO PowerUps (nombre, efecto) VALUES ('Super Rebote', 'rebote_extra');
-- etc.