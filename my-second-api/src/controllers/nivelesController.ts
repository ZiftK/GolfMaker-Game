import { Request, Response } from 'express';

export const getAllNiveles = (req: Request, res: Response): void => {
  res.send('Get all niveles');
};

export const createNivel = (req: Request, res: Response): void => {
  res.send('Create a new nivel');
};

export const updateNivel = (req: Request, res: Response): void => {
  res.send(`Update nivel with ID ${req.params.id}`);
};

export const deleteNivel = (req: Request, res: Response): void => {
  res.send(`Delete nivel with ID ${req.params.id}`);
};