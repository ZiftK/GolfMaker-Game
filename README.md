# GolfMaker-Game
A game to create and play mini golf levels

---

# API para GolfMaker

Esta es la API backend desarrollada en Node.js, Express y MySQL para el juego GolfMaker.

## Prerrequisitos

Asegúrate de tener instalados los siguientes programas:

* [Node.js](https://nodejs.org/) (incluye npm)
* [MySQL Server](https://dev.mysql.com/downloads/mysql/) (o un equivalente como MariaDB)
* [Git](https://git-scm.com/)

## Instalación y Configuración

Sigue estos pasos para poner en marcha el proyecto localmente:

1.  **Clonar el Repositorio:**
    ```bash
    # Reemplaza <URL_DEL_REPOSITORIO> con la URL real de tu repo en GitHub
    git clone <URL_DEL_REPOSITORIO>
    cd nombre-del-repositorio # O el nombre de la carpeta que se clone
    ```

2.  **Instalar Dependencias:**
    ```bash
    npm install
    ```

3.  **Configurar la Base de Datos:**
    * Asegúrate de que tu servidor MySQL esté corriendo.
    * Conéctate a MySQL como usuario administrador (ej. `root`).
    * Crea la base de datos si no existe:
        ```sql
        CREATE DATABASE IF NOT EXISTS GolfMaker;
        ```
    * Ejecuta el script SQL (`schema.sql`) incluido en este repositorio para crear las tablas. Desde la terminal, en la carpeta del proyecto:
        ```bash
        # Reemplaza tu_usuario_db con tu usuario de MySQL. Te pedirá la contraseña.
        mysql -u tu_usuario_db -p GolfMaker < schema.sql
        ```

4.  **Configurar Variables de Entorno:**
    * Copia el archivo de ejemplo `.env.example` a un nuevo archivo llamado `.env`:
        ```bash
        # En Linux/macOS/Git Bash
        cp .env.example .env
        # En Windows CMD
        # copy .env.example .env
        ```
    * Abre el archivo `.env` con un editor de texto.
    * Rellena los valores correspondientes a tu configuración local de MySQL (DB_USER, DB_PASSWORD, DB_PORT si es diferente) y el puerto de la API (PORT) si lo deseas cambiar. **¡Este archivo `.env` no debe subirse a GitHub!**

## Ejecutar la Aplicación

1.  **Modo Desarrollo (con reinicio automático):**
    ```bash
    npm run dev
    ```
    La API estará disponible en `http://localhost:3000` (o el puerto que hayas configurado en `.env`).

2.  **Modo Producción (si aplica - usa el script start):**
    ```bash
    npm start
    ```

## Endpoints de la API Implementados (Hasta Ahora)

* `GET /usuarios`: Obtiene todos los usuarios.
* `GET /usuarios/:id`: Obtiene un usuario por su ID.
* `POST /usuarios`: Crea un nuevo usuario (requiere `nombre_usuario`, `email`, `contraseña` en el body).
* `PUT /usuarios/:id`: Actualiza un usuario existente (requiere todos los campos actualizables en el body).
* `DELETE /usuarios/:id`: Elimina un usuario.
* *(Añadir más endpoints a medida que los implementes, ej. para /niveles)*

---