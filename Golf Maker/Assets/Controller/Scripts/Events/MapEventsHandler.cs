using UnityEngine;
using System;



public class MapEventsHandler
{
    public static MapEventsHandler instance;
    public static MapEventsHandler GetInstance()
    {
        if (instance == null)
        {
            instance = new MapEventsHandler();
        }

        return instance;
    }

    public event EventHandler SaveMap;
    public event EventHandler LoadMap;

    public void OnSaveMap()
    {
        SaveMap?.Invoke(this, new EventArgs());
    }
    public void OnLoadMap()
    {
        LoadMap?.Invoke(this, new EventArgs());
    }

}
