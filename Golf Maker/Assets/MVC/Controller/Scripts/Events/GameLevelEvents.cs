using UnityEngine;

public class GameLevelEvents
{
    public delegate void LoadLevel(int levelId);
    public static event LoadLevel OnLoadLevelEvent;

    // Store the pending initial ball position
    public static Vector3? PendingBallInitialPosition = null;

    public static void TriggerLoadLevel(int levelId)
    {
        OnLoadLevelEvent?.Invoke(levelId);
    }

    public delegate void SetVector3(Vector3 position);

    public static event SetVector3 SetBallInitialPositionEvent;
    public static void TriggerSetBallInitialPosition(Vector3 position)
    {
        Debug.Log($"GameLevelEvents: Triggering SetBallInitialPositionEvent with position {position}");
        PendingBallInitialPosition = position;
        SetBallInitialPositionEvent?.Invoke(position);
    }

    public delegate void NoArgsDelegate();

    public static event NoArgsDelegate OnHitBallEvent;
    public static void TriggerHitBall()
    {
        OnHitBallEvent?.Invoke();
    }

    public static event NoArgsDelegate OnTakeCoin;
    public static void TriggerTakeCoin()
    {
        OnTakeCoin?.Invoke();
    }

    public static event NoArgsDelegate OnResetCoins;
    public static void TriggerResetCoins()
    {
        OnResetCoins?.Invoke();
    }

    public static event NoArgsDelegate OnResetBallEvent;
    public static void TriggerResetBall()
    {
        OnResetBallEvent?.Invoke();
    }

    public delegate void StringDelegate(string value);
    public static event StringDelegate OnSetLevelStruct;

    public static void TriggerOnSetLevelStruct(string levelStruct)
    {
        OnSetLevelStruct?.Invoke(levelStruct);
    }
}
