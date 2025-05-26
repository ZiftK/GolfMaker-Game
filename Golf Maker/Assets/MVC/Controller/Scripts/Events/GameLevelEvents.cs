using UnityEngine;

public class GameLevelEvents
{
    public delegate void LoadLevel(int levelId);
    public static event LoadLevel OnLoadLevelEvent;

    public static void TriggerLoadLevel(int levelId)
    {
        OnLoadLevelEvent?.Invoke(levelId);
    }

    public delegate void SetVector3(Vector3 position);

    public static event SetVector3 SetBallInitialPositionEvent;
    public static void TriggerSetBallInitialPosition(Vector3 position)
    {
        SetBallInitialPositionEvent?.Invoke(position);
    }
}
