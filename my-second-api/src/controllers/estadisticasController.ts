import { Request, Response } from 'express';

export const getAllEstadisticas = (req: Request, res: Response): void => {
  res.send('Get all estadisticas');
};

export const createEstadistica = (req: Request, res: Response): void => {
  res.send('Create a new estadistica');
};

export const updateEstadistica = (req: Request, res: Response): void => {
  res.send(`Update estadistica with ID ${req.params.id}`);
};

export const deleteEstadistica = (req: Request, res: Response): void => {
  res.send(`Delete estadistica with ID ${req.params.id}`);
};