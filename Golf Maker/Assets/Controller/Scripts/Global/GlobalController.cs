using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GlobalController : MonoBehaviour
{
    private MapEventsHandler mapEventsHandler;

    private void Awake()
    {
        mapEventsHandler = MapEventsHandler.GetInstance();
        
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

    
}
