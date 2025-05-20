using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GlobalController : MonoBehaviour
{
    private EditorLevelHandler levelEventsHandler;

    private void Awake()
    {
        levelEventsHandler = EditorLevelHandler.GetInstance();
        
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

    public void OnExitEditLevel(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SceneManager.LoadScene("Game");
            levelEventsHandler.OnLoadLevel();
        }
    }

    
}
