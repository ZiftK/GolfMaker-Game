
public class PrimalLevelRepository : ILevelRepository
{
    private const string LevelDataFilePath = "Assets/Resources/LevelData.json";
    private const string LevelDataBackupFilePath = "Assets/Resources/LevelDataBackup.json";

    public int CreateLevelRecord(LevelEntity level)
    {
        // todo: Implement the logic to create a level record using file system
        return 1;
    }

    public LevelEntity LoadLevelRecord(int id)
    {
        LevelEntity level = new LevelEntity
        {
            LevelId = 1,
            UserId = 123, // Example user ID
            Name = "Level 1",
            CreationDate = System.DateTime.Now,
            Difficulty = "Not set",
            Par = 0,
            Description = "A challenging level with obstacles.",
            AverageRating = 0f,
            TimesPlayed = 0,
            TimesCompleted = 0,
            RewardCoins = 0,
            //todo: Implement the logic to load a level record using file system
            LevelStructure = "***Level structure data here",
        };
        
        

        return level;
    }
}
