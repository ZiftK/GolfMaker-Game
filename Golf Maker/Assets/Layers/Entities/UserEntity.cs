using System;

public class UserEntity
{
    public int IdUsuario { get; set; }
    public string NombreUsuario { get; set; }
    public string Email { get; set; }
    public string Contrasenna { get; set; }
    public DateTime FechaRegistro { get; set; }
    public int NivelesCreados { get; set; }
    public int NivelesCompletados { get; set; }
    public float PuntuacionPromedioRecibida { get; set; }
}