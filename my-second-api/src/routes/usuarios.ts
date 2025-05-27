import { Router } from 'express';
import { getAllUsuarios, createUsuario, updateUsuario, deleteUsuario } from '../controllers/usuariosController';

const router = Router();

router.get('/', getAllUsuarios);
router.post('/', createUsuario);
router.put('/:id', updateUsuario);
router.delete('/:id', deleteUsuario);

export default router;