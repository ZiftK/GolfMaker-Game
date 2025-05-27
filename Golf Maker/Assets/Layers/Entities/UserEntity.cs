
using System;

public class UserEntity {
    public int? UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public DateTime? RegistrationDate { get; set; }
    public int TotalLevelsCreated { get; set; }
    public int TotalLevelsCompleted { get; set; }
    public float AverageRatingReceived { get; set; }
}