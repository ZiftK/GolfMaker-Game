using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GlobalController : MonoBehaviour
{
    private EditorLevelEvents editorLevelEvents;

    private void Awake()
    {
        editorLevelEvents = EditorLevelEvents.GetInstance();
        
    }
    public void OnSaveMap(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            editorLevelEvents.OnSaveLevel();
        }
    }

    public void OnLoadMap(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            editorLevelEvents.OnLoadLevel();
        }
    }

    public void OnExitEditLevel(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SceneManager.LoadScene("Game");
            editorLevelEvents.OnLoadLevel();
        }
    }

    
}
