using UnityEngine;

public class GameLevelController : MonoBehaviour
{
    public Vector3 initialBallPosition = new Vector3(0, 0, 0);

    void Start()
    {
        // Check if we have a level to play in EnvDataHandler
        var levelToPlay = EnvDataHandler.Instance.GetCurrentLevelToPlay();
        
        if (levelToPlay != null)
        {
            // If we have a level to play, load it
            GameLevelEvents.TriggerLoadLevel(levelToPlay.id_nivel);
            GameLevelEvents.TriggerOnSetLevelStruct(levelToPlay.estructura_nivel);
        }
        else
        {
            // If no level is set, load the default level (ID 1)
            GameLevelEvents.TriggerLoadLevel(1);
        }
    }
} 