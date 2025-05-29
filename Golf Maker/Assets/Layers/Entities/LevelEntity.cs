using System;

public enum Dificultad
{
    Fácil,
    Medio,
    Difícil
}

public class LevelEntity
{
    public int? IdNivel { get; set; }
    public int IdUsuario { get; set; }
    public string Nombre { get; set; }
    public DateTime? FechaCreacion { get; set; }
    public Dificultad Dificultad { get; set; }
    public string Descripcion { get; set; }
    public float RatingPromedio { get; set; }
    public int JugadoVeces { get; set; }
    public int CompletadoVeces { get; set; }
    public int CantidadMoneas { get; set; }
    public string EstructuraNivel { get; set; }
    public int AnchoNivel { get; set; }
    public int AltoNivel { get; set; }
}