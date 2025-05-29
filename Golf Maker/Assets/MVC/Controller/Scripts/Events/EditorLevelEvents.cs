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

public class SaveLevelArgs : EventArgs
{
    public LevelEntity levelData { get; }
    public SaveLevelArgs(LevelEntity levelData)
    {
        this.levelData = levelData;
    }
}

public class EditorLevelEvents
{


    private static EditorLevelEvents Instance;
    public static EditorLevelEvents GetInstance()
    {
        if (Instance == null)
        {
            Instance = new EditorLevelEvents();
        }
        return Instance;
    }

    public event EventHandler<SaveLevelArgs> SaveLevel;
    public event EventHandler LoadLevel;
    public event EventHandler ExitEditLevel;
    public event EventHandler EnterEditLevel;

    public event EventHandler<SelectPencilArgs> SelectPencil;
    public event EventHandler<SelectBlockArgs> SelectBlock;

    public void OnSaveLevel(LevelEntity levelData)
    {
        SaveLevel?.Invoke(this, new SaveLevelArgs(levelData));
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

    public void OnSelectPencil(SelectPencilArgs e)
    {
        SelectPencil?.Invoke(this, e);
    }
    public void OnSelectBlock(SelectBlockArgs e)
    {
        SelectBlock?.Invoke(this, e);
    }

}
