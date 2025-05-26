using UnityEngine;

public class GameLevelEvents
{
    public delegate void LoadLevel(int levelId);
    public static event LoadLevel OnLoadLevelEvent;
    
    public static void TriggerLoadLevel(int levelId)
    {
        OnLoadLevelEvent?.Invoke(levelId);
    }
}
