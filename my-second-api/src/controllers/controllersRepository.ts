import { EstadisticaRepository } from "../repository/abstract/EstadisticaRepository";
import { NivelRepository } from "../repository/abstract/NivelRepository";
import { UsuarioRepository } from "../repository/abstract/UsuarioRepository";
import { RatingRepository } from "../repository/abstract/RatingRepository";

import { SupabaseEstadisticaRepository } from "../repository/concrete/SupabaseEstadisticaRepository";
import { SupabaseNivelRepository } from "../repository/concrete/SupabaseNivelRepository";
import { SupabaseUsuarioRepository } from "../repository/concrete/SupabaseUsuarioRepository";
import { SupabaseRatingRepository } from "../repository/concrete/SupabaseRatingRepository";

const controllersRepository = {
  estadisticaRepository: new SupabaseEstadisticaRepository() as EstadisticaRepository,
  nivelRepository: new SupabaseNivelRepository() as NivelRepository,
  usuarioRepository: new SupabaseUsuarioRepository() as UsuarioRepository,
  ratingRepository: new SupabaseRatingRepository() as RatingRepository,
};

export default controllersRepository;