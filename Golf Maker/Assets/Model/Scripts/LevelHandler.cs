using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    LevelEventsHandler levelEventsHandler;
    ILevelRepository levelRepository;

    void Awake()
    {
        levelEventsHandler = LevelEventsHandler.GetInstance();
        levelEventsHandler.SaveLevel += OnSaveLevel;
        levelEventsHandler.LoadLevel += OnLoadLevel;
    }

    private void OnSaveLevel(object sender, System.EventArgs e)
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
            LevelStructure = LevelParser.SerializeLevelIds(Grid2D.Instance.GetLevelIds()),
        };

        // Add logic to save the level using levelRepository
        levelRepository.CreateLevelRecord(level);
    }

    private void OnLoadLevel(object sender, System.EventArgs e)
    {
        LevelEntity level = levelRepository.LoadLevelRecord(1);

        //todo: Add logic to load the level using levelRepository
    }
}
