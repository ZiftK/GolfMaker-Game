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
    public PencilSetType pencilSetType;
    public SelectBlockArgs(string blockName, PencilSetType pencilSetType)
    {
        this.blockName = blockName;
        this.pencilSetType = pencilSetType;
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

public class LoadLevelArgs : EventArgs
{
    public int levelId;

    public LoadLevelArgs(int levelId)
    {
        this.levelId = levelId;
    }
}

public class ResetLevelArgs : EventArgs
{
    public string levelStruct;

    public ResetLevelArgs(string levelStruct)
    {
        this.levelStruct = levelStruct;
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
    public event EventHandler<LoadLevelArgs> LoadLevel;

    public event EventHandler<ResetLevelArgs> ResetLevel;
    public event EventHandler ExitEditLevel;
    public event EventHandler EnterEditLevel;

    public event EventHandler<SelectPencilArgs> SelectPencil;
    public event EventHandler<SelectBlockArgs> SelectBlock;

    public void OnSaveLevel(LevelEntity levelData)
    {
        SaveLevel?.Invoke(this, new SaveLevelArgs(levelData));
    }
    public void OnLoadLevel(int levelId)
    {
        LoadLevel?.Invoke(this, new LoadLevelArgs(levelId));
    }

    public void OnResetLevel(string levelStruct)
    {
        ResetLevel?.Invoke(this, new ResetLevelArgs(levelStruct));
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
