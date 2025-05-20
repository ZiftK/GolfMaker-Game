using UnityEngine;
using System;



public class EditorLevelHandler
{
    public static EditorLevelHandler instance;
    public static EditorLevelHandler GetInstance()
    {
        if (instance == null)
        {
            instance = new EditorLevelHandler();
        }

        return instance;
    }

    public event EventHandler SaveLevel;
    public event EventHandler LoadLevel;
    public event EventHandler ExitEditLevel;
    public event EventHandler EnterEditLevel;

    public void OnSaveLevel()
    {
        SaveLevel?.Invoke(this, new EventArgs());
    }
    public void OnLoadLevel()
    {
        LoadLevel?.Invoke(this, new EventArgs());
    }

    public void OnExitEditLevel()
    {
        ExitEditLevel?.Invoke(this, new EventArgs());
    }
    
    public void OnEnterEditLevel()
    {
        EnterEditLevel?.Invoke(this, new EventArgs());
    }

}
