using UnityEngine;
using System;

public class RatingEntity
{
    public int IdRating { get; set; }
    public int IdUsuario { get; set; }
    public int IdNivel { get; set; }
    public float Calificacion { get; set; }
    public string Comentario { get; set; }
    public DateTime FechaRating { get; set; }
}
