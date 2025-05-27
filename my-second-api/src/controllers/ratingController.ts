import { Request, Response } from 'express';

export const getAllRatings = (req: Request, res: Response): void => {
  res.send('Get all ratings');
};

export const createRating = (req: Request, res: Response): void => {
  res.send('Create a new rating');
};

export const updateRating = (req: Request, res: Response): void => {
  res.send(`Update rating with ID ${req.params.id}`);
};

export const deleteRating = (req: Request, res: Response): void => {
  res.send(`Delete rating with ID ${req.params.id}`);
};