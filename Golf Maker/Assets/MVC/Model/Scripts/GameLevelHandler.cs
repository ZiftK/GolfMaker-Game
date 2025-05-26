using UnityEngine;

public class GameLevelHandler : MonoBehaviour
{

    #region Repository
    private ILevelRepository levelRepository;
    #endregion Repository

    public Vector3 initialBallPosition = new Vector3(0, 0, 0);


    // Awake is called when the script instance is being loaded
    void Awake()
    {
        GameLevelEvents.OnLoadLevelEvent += OnLoadLevel;
        Grid2D.Instance.ActivateVisualGrid(false);
        OnLoadLevel(1); // Load the default level with ID 1
     
    }

    public void OnLoadLevel(int levelId)
    {
        levelRepository = new PrimalLevelRepository();
        LevelEntity level = levelRepository.LoadLevelRecord(levelId);

        if (level == null)
        {
            Debug.LogError($"Level with ID {levelId} not found.");
            return;
        }

        // set level dimensions
        Grid2D.Instance.SetLevelHeight(level.levelHeight);
        Grid2D.Instance.SetLevelWidth(level.levelWidth);

        // deserialize the level structure
        int[,] levelStructure = LevelParser.DeSerializeLevelIds(level.LevelStructure);

        // Load the level structure into the grid
        Grid2D.Instance.LoadLevelFromParseLevel(levelStructure);

        // Activate the visual grid
        Grid2D.Instance.ActivateVisualGrid(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
