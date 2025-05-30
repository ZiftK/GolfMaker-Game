using System;
using NUnit;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelEditorButtonsController : MonoBehaviour
{
    public UIDocument uIDocument;

    private VisualElement saveButton;
    private VisualElement resetButton;

    private VisualElement leaveButton;

    private EditorLevelEvents editorLevelEvents;

    private VisualElement root;

    public void Awake()
    {
        editorLevelEvents = EditorLevelEvents.GetInstance();

        root = uIDocument.rootVisualElement;

        saveButton = root.Q<Button>("save-tool");
        saveButton.RegisterCallback<ClickEvent>(_ =>
        {
            SaveLevel();
        });

        resetButton = root.Q<Button>("reset-tool");
        resetButton.RegisterCallback<ClickEvent>(_ =>
        {
            ResetLevel();
        });

        leaveButton = root.Q<Button>("leave-tool");
        leaveButton.RegisterCallback<ClickEvent>(_ =>
        {
            SceneManager.LoadScene("Root");
        });
    }

    void SaveLevel()
    {
        try
        {
            string saveLevelStruct = GridFacade.Instance.GetStructure();
            var levelInEdition = EnvDataHandler.Instance.GetCurrentInEditionLevel();

            LevelEntity levelData = levelInEdition;

            levelData.estructura_nivel = saveLevelStruct;
            levelData.id_usuario = EnvDataHandler.Instance.GetCurrentUserId();


            editorLevelEvents.OnSaveLevel(levelData);
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex.Message);
        }
    }

    void ResetLevel()
    {
        string levelStruct = EnvDataHandler.Instance.GetCurrentInEditionLevel().estructura_nivel;
        if (string.IsNullOrEmpty(levelStruct))
        {
            return;
        }

        editorLevelEvents.OnResetLevel(levelStruct);

    }

    void LoadLevel()
    {
        editorLevelEvents.OnLoadLevel(EnvDataHandler.Instance.GetCurrentLevelIdToLoad());
    }

    void SaveMiddleData()
    {

    }
}
