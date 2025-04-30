using UnityEngine;
using System;



public class LevelEventsHandler
{
    public static LevelEventsHandler instance;
    public static LevelEventsHandler GetInstance()
    {
        if (instance == null)
        {
            instance = new LevelEventsHandler();
        }

        return instance;
    }

    public event EventHandler SaveLevel;
    public event EventHandler LoadLevel;

    public void OnSaveLevel()
    {
        SaveLevel?.Invoke(this, new EventArgs());
    }
    public void OnLoadLevel()
    {
        LoadLevel?.Invoke(this, new EventArgs());
    }

}
