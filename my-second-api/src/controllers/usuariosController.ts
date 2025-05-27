import { Request, Response } from 'express';

export const getAllUsuarios = (req: Request, res: Response): void => {
  res.send('Get all usuarios');
};

export const createUsuario = (req: Request, res: Response): void => {
  res.send('Create a new usuario');
};

export const updateUsuario = (req: Request, res: Response): void => {
  res.send(`Update usuario with ID ${req.params.id}`);
};

export const deleteUsuario = (req: Request, res: Response): void => {
  res.send(`Delete usuario with ID ${req.params.id}`);
};