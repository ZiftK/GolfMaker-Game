using System;
using UnityEngine;

[Serializable]
public enum Dificultad
{
    Facil,
    Medio,
    Dificil
}

[Serializable]
public class LevelEntity
{
    public int id_nivel;
    public int id_usuario;
    public string nombre;
    public string fecha_creacion;
    public string dificultad;
    public string descripcion;
    public float rating_promedio;
    public int jugado_veces;
    public int completado_veces;
    public int cantidad_monedas;
    public string estructura_nivel;

    [NonSerialized]
    public int ancho_nivel;
    [NonSerialized]
    public int alto_nivel;
}

