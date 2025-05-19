using UnityEngine;

[DefaultExecutionOrder(-100)]
public class LevelHandler : MonoBehaviour
{
    LevelEventsHandler levelEventsHandler;
    ILevelRepository levelRepository;

    void Awake()
    {
        levelEventsHandler = LevelEventsHandler.GetInstance();
        levelRepository = new PrimalLevelRepository(); 
        levelEventsHandler.SaveLevel += OnSaveLevel;
        levelEventsHandler.LoadLevel += OnLoadLevel;
    }

    private void OnSaveLevel(object sender, System.EventArgs e)
    {
        Debug.Log("Saving level...");
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
            LevelStructure = LevelParser.SerializeLevelIds(Grid2D.Instance.GetLevelIds()),
        };

        // Add logic to save the level using levelRepository
        levelRepository.CreateLevelRecord(level);
    }

    private void OnLoadLevel(object sender, System.EventArgs e)
    {
        LevelEntity level = levelRepository.LoadLevelRecord(1);

        int [,] levelIds = LevelParser.DeSerializeLevelIds(level.LevelStructure);
        Grid2D.Instance.LoadLevelFromParseLevel(levelIds);
    }
}
