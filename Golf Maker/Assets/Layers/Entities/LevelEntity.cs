using System;
using UnityEngine;

[Serializable]
public enum Dificultad
{
    Fácil,
    Medio,
    Difícil
}

[Serializable]
public class LevelEntity
{
    public int? IdNivel;
    public int IdUsuario;
    public string Nombre;
    public DateTime? FechaCreacion;
    public Dificultad Dificultad;
    public string Descripcion;
    public float RatingPromedio;
    public int JugadoVeces;
    public int CompletadoVeces;
    public int CantidadMoneas;
    public string EstructuraNivel;
    public int AnchoNivel;
    public int AltoNivel;
}