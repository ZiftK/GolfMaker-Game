using UnityEngine;

public class GameLevelHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    // Awake is called when the script instance is being loaded
    void Awake()
    {
        LevelEventsHandler levelEventsHandler = LevelEventsHandler.GetInstance();
        levelEventsHandler.OnLoadLevel();
        Grid2D.Instance.ActivateVisualGrid(false);
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
