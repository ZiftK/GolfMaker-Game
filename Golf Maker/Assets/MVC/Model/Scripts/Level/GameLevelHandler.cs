using System.Threading.Tasks;
using UnityEngine;

public class GameLevelHandler : MonoBehaviour
{


    #region Repository
    private ILevelRepository levelRepository;
    #endregion Repository

    public Vector3 initialBallPosition = new Vector3(0, 0, 0);

    public static GameLevelHandler Instance;

    #region Properties

    private int hits;
    private int resets;

    private int coins;

    #endregion Properties


    // Awake is called when the script instance is being loaded
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
            return;
        }

        GameLevelEvents.OnLoadLevelEvent += OnLoadLevel;
        GameLevelEvents.OnHitBallEvent += AddHit;
        GameLevelEvents.OnResetBallEvent += AddReset;
        GameLevelEvents.OnTakeCoin += TakeCoin;
        GameLevelEvents.OnResetCoins += ResetCoins;

        levelRepository = ServerLevelRepository.GetInstance();

        GridFacade.Instance.ActivateVisualGrid(false);
        OnLoadLevel(1); // Load the default level with ID 1

    }

    public void OnLoadLevel(int levelId)
    {
        // levelRepository = new PrimalLevelRepository();
        // LevelEntity level = levelRepository.LoadLevelRecord(levelId);

        // if (level == null)
        // {
        //     Debug.LogError($"Level with ID {levelId} not found.");
        //     return;
        // }

        // // set level dimensions
        // Grid2D.Instance.SetLevelHeight((int)level.AltoNivel);
        // Grid2D.Instance.SetLevelWidth((int)level.AnchoNivel);

        // // deserialize the level structure
        // int[,] levelStructure = LevelParser.DeSerializeLevelIds(level.EstructuraNivel);

        // // Load the level structure into the grid
        // Grid2D.Instance.LoadLevelFromParseLevel(levelStructure);

        // // Activate the visual grid
        // Grid2D.Instance.ActivateVisualGrid(false);
        _ = AsyncLoadLevel(levelId);
    }

    async Task AsyncLoadLevel(int levelId)
    {
        var levelData = await levelRepository.GetById(levelId);
        EnvDataHandler.Instance.SetLevelToPlayData(levelData);

        GameLevelEvents.TriggerOnSetLevelStruct(levelData.estructura_nivel);
    }

    #region Controls
    public void AddHit() => hits++;
    public void ResetHits() => hits = 0;
    public void MinusHit()
    {
        if (hits > 0)
        {
            hits--;
        }
    }

    public void AddReset() => resets++;
    public void ResetResets() => resets = 0;
    public void MinusReset()
    {
        if (resets > 0)
        {
            resets--;
        }
    }

    public void TakeCoin() => coins++;
    public void ResetCoins() => coins = 0;
    #endregion Controls
}
