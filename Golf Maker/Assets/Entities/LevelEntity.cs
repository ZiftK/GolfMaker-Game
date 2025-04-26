
using System;

public class LevelEntity {
    public int LevelId { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public DateTime CreationDate { get; set; }
    public string Difficulty { get; set; }
    public int Par { get; set; }
    public string Description { get; set; }
    public float AverageRating { get; set; }
    public int TimesPlayed { get; set; }
    public int TimesCompleted { get; set; }
    public int RewardCoins { get; set; }
    public string LevelStructure { get; set; }
}