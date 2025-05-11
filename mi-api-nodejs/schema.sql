-- Crear la base de datos (si no existe)
CREATE DATABASE IF NOT EXISTS GolfMaker;

-- Usar la base de datos
USE GolfMaker;

-- Tabla de Usuarios
CREATE TABLE IF NOT EXISTS Usuarios (
    id_usuario INT AUTO_INCREMENT PRIMARY KEY,
    nombre_usuario VARCHAR(50) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    contraseña VARCHAR(255) NOT NULL, -- Recuerda que aquí guardas el HASH
    fecha_registro TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    niveles_creados INT DEFAULT 0,
    niveles_completados INT DEFAULT 0,
    puntuacion_total INT DEFAULT 0,
    monedas INT DEFAULT 0
);

-- Tabla de Niveles
CREATE TABLE IF NOT EXISTS Niveles (
    id_nivel INT AUTO_INCREMENT PRIMARY KEY,
    id_usuario INT NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    fecha_creacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    dificultad ENUM('fácil', 'medio', 'difícil') NOT NULL,
    par INT NOT NULL,
    descripcion TEXT,
    rating_promedio FLOAT DEFAULT 0,
    jugado_veces INT DEFAULT 0,
    completado_veces INT DEFAULT 0,
    monedas_recompensa INT DEFAULT 0,
    estructura_nivel TEXT,
    FOREIGN KEY (id_usuario) REFERENCES Usuarios(id_usuario) ON DELETE CASCADE
);


-- Tabla de Ratings
CREATE TABLE IF NOT EXISTS Rating (
    id_rating INT AUTO_INCREMENT PRIMARY KEY,
    id_usuario INT NOT NULL,
    id_nivel INT NOT NULL,
    calificacion INT CHECK (calificacion BETWEEN 1 AND 5),
    comentario TEXT,
    fecha_rating TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    monedas_bonus INT DEFAULT 0,
    FOREIGN KEY (id_usuario) REFERENCES Usuarios(id_usuario) ON DELETE CASCADE,
    FOREIGN KEY (id_nivel) REFERENCES Niveles(id_nivel) ON DELETE CASCADE
);

-- Tabla de PowerUps
CREATE TABLE IF NOT EXISTS PowerUps (
    id_powerup INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    descripcion TEXT,
    duracion INT DEFAULT 0, -- Tiempo en segundos
    cantidad_uso INT DEFAULT 1,
    efecto ENUM('rebote_extra', 'vuelo', 'freno', 'aceleracion') NOT NULL,
    desbloqueo TEXT
);

-- Tabla intermedia Usuario_PowerUp
CREATE TABLE IF NOT EXISTS Usuario_PowerUp (
    id_usuario INT NOT NULL,
    id_powerup INT NOT NULL,
    cantidad_disponible INT DEFAULT 1,
    PRIMARY KEY (id_usuario, id_powerup),
    FOREIGN KEY (id_usuario) REFERENCES Usuarios(id_usuario) ON DELETE CASCADE,
    FOREIGN KEY (id_powerup) REFERENCES PowerUps(id_powerup) ON DELETE CASCADE
);

-- Tabla de Tienda (No utilizada por ahora en API)
CREATE TABLE IF NOT EXISTS Tienda (
    id_item INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    tipo ENUM('monedas', 'power-up') NOT NULL,
    precio_monedas INT DEFAULT 0,
    precio_real DECIMAL(10,2) DEFAULT 0,
    descripcion TEXT
);

-- Tabla de Transacciones (No utilizada por ahora en API)
CREATE TABLE IF NOT EXISTS Transacciones (
    id_transaccion INT AUTO_INCREMENT PRIMARY KEY,
    id_usuario INT NOT NULL,
    id_item INT NOT NULL,
    monto_monedas INT DEFAULT 0,
    monto_real DECIMAL(10,2) DEFAULT 0,
    fecha TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (id_usuario) REFERENCES Usuarios(id_usuario) ON DELETE CASCADE,
    FOREIGN KEY (id_item) REFERENCES Tienda(id_item) ON DELETE CASCADE
);

-- Puedes añadir aquí INSERTs iniciales si quieres datos de ejemplo
-- INSERT INTO PowerUps (nombre, efecto) VALUES ('Super Rebote', 'rebote_extra');
-- etc.