using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class RepositoryManager : MonoBehaviour
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

        });

        leaveButton = root.Q<Button>("leave-tool");
        leaveButton.RegisterCallback<ClickEvent>(_ =>
        {
            SceneManager.LoadScene("Root");
        });
    }

    void SaveLevel()
    {
        string levelName = root.Q<TextField>().value;

        if (string.IsNullOrWhiteSpace(levelName))
        {
            Debug.LogError("Level name empty");
            return;
        }

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
            alto_nivel = Grid2D.Instance.GetLevelHeight(),
            ancho_nivel = Grid2D.Instance.GetLevelWidth(),
            estructura_nivel = LevelParser.SerializeLevelIds(Grid2D.Instance.GetLevelIds()),
        };

        editorLevelEvents.OnSaveLevel(levelData);
    }

    void LoadLevel()
    {

        editorLevelEvents.OnLoadLevel();
    }
}
