
using System;
public enum DifficultyLevel
{
    Easy,
    Medium,
    Hard
}

public class LevelEntity {
    public int? LevelId { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public DateTime? CreationDate { get; set; }
    public string Difficulty { get; set; }
    public string Description { get; set; }
    public float AverageRating { get; set; }
    public int TimesPlayed { get; set; }
    public int TimesCompleted { get; set; }

    public int CoinsCount { get; set; }

    public int? levelWidth { get; set; }
    public int? levelHeight { get; set; }
    public string LevelStructure { get; set; }
}