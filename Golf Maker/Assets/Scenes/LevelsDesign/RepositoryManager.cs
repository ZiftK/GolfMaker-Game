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
            IdNivel = -1,
            IdUsuario = EnvDataHandler.Instance.GetCurrentUserId(), // Example user ID
            Nombre = levelName,
            FechaCreacion = System.DateTime.Now,
            Dificultad = Dificultad.Medio,
            Descripcion = "A challenging level with obstacles.",
            RatingPromedio = 0f,
            JugadoVeces = 0,
            CompletadoVeces = 0,
            CantidadMoneas = 0,
            AltoNivel = Grid2D.Instance.GetLevelHeight(),
            AnchoNivel = Grid2D.Instance.GetLevelWidth(),
            EstructuraNivel = LevelParser.SerializeLevelIds(Grid2D.Instance.GetLevelIds()),
        };

        editorLevelEvents.OnSaveLevel(levelData);
    }

    void LoadLevel()
    {

        editorLevelEvents.OnLoadLevel();
    }
}
