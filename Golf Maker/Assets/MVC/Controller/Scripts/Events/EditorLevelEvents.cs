using UnityEngine;
using System;


public class SelectPencilArgs : EventArgs
{
    public string pincelName { get; }
    public SelectPencilArgs(string pincelName)
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

public class EditorLevelEvents
{
    

    public event EventHandler SaveLevel;
    public event EventHandler LoadLevel;
    public event EventHandler ExitEditLevel;
    public event EventHandler EnterEditLevel;

    public static event EventHandler<SelectPencilArgs> SelectPencil;
    public static event EventHandler<SelectBlockArgs> SelectBlock;

    public static void OnSaveLevel()
    {
        SaveLevel?.Invoke(this, new EventArgs());
    }
    public static void OnLoadLevel()
    {
        LoadLevel?.Invoke(this, new EventArgs());
    }

    public static void OnExitEditLevel()
    {
        ExitEditLevel?.Invoke(this, new EventArgs());
    }

    public static void OnEnterEditLevel()
    {
        EnterEditLevel?.Invoke(this, new EventArgs());
    }

    public static void OnSelectPencil(SelectPencilArgs e)
    {
        SelectPencil?.Invoke(this, e);
    }
    public static void OnSelectBlock(SelectBlockArgs e)
    {
        SelectBlock?.Invoke(this, e);
    }

}
