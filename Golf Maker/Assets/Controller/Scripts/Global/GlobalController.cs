using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GlobalController : MonoBehaviour
{
    private MapEventsHandler mapEventsHandler;

    private void Awake()
    {
        mapEventsHandler = MapEventsHandler.GetInstance();
        mapEventsHandler.SaveMap += SaveMap;
        mapEventsHandler.LoadMap += LoadMap;
    }
    public void OnSaveMap(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            mapEventsHandler.OnSaveMap();
        }
    }

    public void OnLoadMap(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            mapEventsHandler.OnLoadMap();
        }
    }

    private void SaveMap(object sender, EventArgs e)
    {
        // Implement your save map logic here
        Debug.Log("Map saved.");
    }
    
    private void LoadMap(object sender, EventArgs e)
    {
        // Implement your load map logic here
        Debug.Log("Map loaded.");
    }
}
