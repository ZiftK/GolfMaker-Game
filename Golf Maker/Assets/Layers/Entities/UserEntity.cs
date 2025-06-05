using System;

[Serializable]
public class UserEntity
{
    public int id_usuario { get; set; }
    public string nombre_usuario { get; set; }
    public string email { get; set; }
    public string contrasenna { get; set; }
    public DateTime fecha_registro { get; set; }
    public int niveles_creados { get; set; }
    public int niveles_completados { get; set; }
    public float puntuacion_promedio_recibida { get; set; }

    public override string ToString()
    {
        return $"UserEntity {{ id_usuario = {id_usuario}, nombre_usuario = {nombre_usuario}, email = {email}, contrasenna = {contrasenna}, fecha_registro = {fecha_registro}, niveles_creados = {niveles_creados}, niveles_completados = {niveles_completados}, puntuacion_promedio_recibida = {puntuacion_promedio_recibida} }}";
    }
}