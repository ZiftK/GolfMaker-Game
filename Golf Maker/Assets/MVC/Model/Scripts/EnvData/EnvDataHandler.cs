using UnityEngine;

[DefaultExecutionOrder(-300)]
public class EnvDataHandler : MonoBehaviour
{
    private UserEntity userData;

    private LevelEntity levelInEdition;

    private int levelToPlay;

    public static EnvDataHandler Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this instance across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
        DontDestroyOnLoad(this.gameObject); // Ensure this object persists across scene loads
    }

    public void SetUserData(UserEntity user)
    {
        userData = user;
    }

    public void SetLevelInEditionData(LevelEntity level)
    {
        levelInEdition = level;
    }

    public void SetLevelInEditionUserData(ref LevelEntity level)
    {
        level.id_usuario = this.userData.id_usuario;
    }

    public void ClearLevelInEditionData()
    {
        levelInEdition = null;
    }

    public bool HasData() => !(userData is null);

    public int GetCurrentUserId() => userData.id_usuario;

    public int GetCurrentLevelInEditionId()
    {
        if (levelInEdition is null) return -1;
        return levelInEdition.id_nivel;
    }

    public int GetCurrentLevelIdToLoad()
    {
        return levelToPlay;
    }

    public string GetCurrentLevelInEditionStructure()
    {
        if (levelInEdition is null) return "";
        return levelInEdition.estructura_nivel;
    }
}
