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
            Difficulty = "Medium",
            Par = 3,
            Description = "A challenging level with obstacles.",
            AverageRating = 4.5f,
            TimesPlayed = 10,
            TimesCompleted = 5,
            RewardCoins = 100,
            LevelStructure = LevelParser.SerializeLevelIds(Grid2D.Instance.GetLevelIds()),
        };

        // Add logic to save the level using levelRepository
        levelRepository.CreateLevelRecord(level);
    }

    private void OnLoadLevel(object sender, System.EventArgs e)
    {
        // Implement your load logic here
        Debug.Log("Level loaded!");
    }
}
