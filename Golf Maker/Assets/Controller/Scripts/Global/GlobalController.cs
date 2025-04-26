using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GlobalController : MonoBehaviour
{
    private LevelEventsHandler levelEventsHandler;

    private void Awake()
    {
        levelEventsHandler = LevelEventsHandler.GetInstance();
        
    }
    public void OnSaveMap(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            levelEventsHandler.OnSaveLevel();
        }
    }

    public void OnLoadMap(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            levelEventsHandler.OnLoadLevel();
        }
    }

    
}
