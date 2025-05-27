using UnityEngine;

[DefaultExecutionOrder(-100)]
public class EditorEventHandler : MonoBehaviour
{

    EditorLevelEvents levelEventsHandler;
    ILevelRepository levelRepository;

    void Awake()
    {
        levelRepository = new PrimalLevelRepository(); 
        levelEventsHandler = EditorLevelEvents.GetInstance();

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
            Description = "A challenging level with obstacles.",
            AverageRating = 0f,
            TimesPlayed = 0,
            TimesCompleted = 0,
            CoinsCount = 0,
            levelHeight = Grid2D.Instance.GetLevelHeight(),
            levelWidth = Grid2D.Instance.GetLevelWidth(),
            LevelStructure = LevelParser.SerializeLevelIds(Grid2D.Instance.GetLevelIds()),
        };

        // Add logic to save the level using levelRepository
        levelRepository.CreateLevelRecord(level);
    }

    private void OnLoadLevel(object sender, System.EventArgs e)
    {
        LevelEntity level = levelRepository.LoadLevelRecord(1);

        if (level == null)
        {
            Debug.LogError("Level not found.");
            return;
        }
        
        if (level.levelHeight != Grid2D.Instance.GetLevelHeight() || level.levelWidth != Grid2D.Instance.GetLevelWidth())
        {
            Debug.LogError(
                $"Level dimensions must be {level.levelWidth}x{level.levelHeight} not {Grid2D.Instance.GetLevelWidth()}x{Grid2D.Instance.GetLevelHeight()}");
            return;
        }

        int [,] levelIds = LevelParser.DeSerializeLevelIds(level.LevelStructure);

        Grid2D.Instance.LoadLevelFromParseLevel(levelIds);
    }
}
