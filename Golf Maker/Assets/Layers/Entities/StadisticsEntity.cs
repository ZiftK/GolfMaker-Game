using System;
using UnityEngine;

[Serializable]
public class StadisticsEntity
{
    public int IdEstadistica { get; set; }
    public int IdUsuario { get; set; }
    public int IdNivel { get; set; }
    public int MaxMuertes { get; set; }
    public int MinMuertes { get; set; }
    public int MaxGolpes { get; set; }
    public int MinGolpes { get; set; }
    public int MonedasRecolectadas { get; set; }
    public float CalificacionGeneral { get; set; }
}
