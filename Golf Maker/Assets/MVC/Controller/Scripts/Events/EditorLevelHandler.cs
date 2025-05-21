using UnityEngine;
using System;


public class SelectPincelArgs : EventArgs
{
    public string pincelName { get; }
    public SelectPincelArgs(string pincelName)
    {
        this.pincelName = pincelName;
    }
}

public class SelectBlockArgs : EventArgs
{
    public string blockName { get; }
    public SelectBlockArgs(string blockName)
    {
        this.blockName = blockName;
    }
}

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

    public event EventHandler<SelectPincelArgs> SelectPincel;
    public event EventHandler<SelectBlockArgs> SelectBlock;

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

    public void OnSelectPincel(SelectPincelArgs e)
    {
        SelectPincel?.Invoke(this, e);
    }
    public void OnSelectBlock(SelectBlockArgs e)
    {
        SelectBlock?.Invoke(this, e);
    }

}
