using System;
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
            string levelName = root.Q<TextField>().value;

            if (string.IsNullOrWhiteSpace(levelName))
            {
                Debug.LogError("Level name empty");
                return;
            }

            string saveLevelStruct = GridFacade.Instance.GetStructure();

             LevelEntity levelData = new LevelEntity
            {
                id_nivel = EnvDataHandler.Instance.GetCurrentLevelInEditionId(),
                id_usuario = EnvDataHandler.Instance.GetCurrentUserId(), // Example user ID
                nombre = levelName,
                fecha_creacion = "",
                dificultad = Dificultad.Medio.ToString(),
                descripcion = "A challenging level with obstacles.",
                rating_promedio = 0f,
                jugado_veces = 0,
                completado_veces = 0,
                cantidad_monedas = 0,
                alto_nivel = GridFacade.Instance.GetLevelHeight(),
                ancho_nivel = GridFacade.Instance.GetLevelWidth(),
                estructura_nivel = saveLevelStruct,
            };

            editorLevelEvents.OnSaveLevel(levelData);
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex.Message);
        }
       
    }

    void ResetLevel()
    {
        string levelStruct = EnvDataHandler.Instance.GetCurrentLevelInEditionStructure();
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
}
