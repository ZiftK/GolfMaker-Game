// app.js
require('dotenv').config();
const express = require('express');

// Importar Rutas
const usuariosRoutes = require('./routes/usuariosRoutes');
const nivelesRoutes = require('./routes/nivelesRoutes');
const bloquesRoutes = require('./routes/bloquesRoutes');
const ratingsRoutes = require('./routes/ratingsRoutes');
const powerUpsRoutes = require('./routes/powerUpsRoutes'); // <-- ULTIMA LINEA DE CODIGO AGREGADA   

const app = express();
const port = process.env.PORT || 3000;

// --- MIDDLEWARE ---
app.use(express.json());

// --- RUTAS ---
app.get('/', (req, res) => {
    res.send('API GolfMaker v2 - ¡Rutas y Controladores!');
});

// Montar las rutas (fíjate en los prefijos)
app.use('/usuarios', usuariosRoutes);
app.use('/niveles', nivelesRoutes);
app.use('/bloques', bloquesRoutes);
app.use('/ratings', ratingsRoutes);
app.use('/powerups', powerUpsRoutes); // <-- ULTIMA LINEA DE CODIGO AGREGADA

// --- INICIAR SERVIDOR ---
app.listen(port, () => {
    console.log(`Servidor iniciado y escuchando en http://localhost:${port}`);
});