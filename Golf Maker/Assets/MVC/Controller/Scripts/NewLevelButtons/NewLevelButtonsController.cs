using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class NewLevelButtonsController : MonoBehaviour
{
    public UIDocument uIDocument;

    private TextField nameTextBox;

    private TextField descriptionTextBox;

    private EnumField difficultEnum;

    private Button createButton;

    private Button backButton;

    void OnEnable()
    {
        var root = uIDocument.rootVisualElement;

        nameTextBox = root.Q<TextField>("name");
        descriptionTextBox = root.Q<TextField>("description");
        difficultEnum = root.Q<EnumField>("dificult");
        createButton = root.Q<Button>("create-button");
        backButton = root.Q<Button>("backButton");

        createButton.RegisterCallback<ClickEvent>(_ =>
            {
                CreateLevel();
            }
        );

        backButton.RegisterCallback<ClickEvent>(_ =>
            {
                SwitchToPlayDesign();
            }
        );
    }

    public void CreateLevel()
    {

        string levelName = nameTextBox.text;
        string levelDescription = descriptionTextBox.text;

        if (string.IsNullOrEmpty(levelName))
        {
            Debug.LogError("El nombre no puede estar vacio");
            return;
        }

        if (string.IsNullOrWhiteSpace(levelName))
        {
            Debug.LogError("El nombre no puede estar vacio");
            return;
        }

        var level = new LevelEntity
        {
            id_nivel = -1,
            nombre = levelName,
            descripcion = levelDescription,
            dificultad = difficultEnum.value.ToString()
        };
        EnvDataHandler.Instance.SetLevelInEditionData(level);
        SceneManager.LoadScene("LevelCreator");
    }

    public void SwitchToPlayDesign()
    {
        Debug.Log("Se presiono");
        // clear
        nameTextBox.value = "";
        descriptionTextBox.value = "";
        UIManager.Instance.ShowPlayDesignMenu();
    }
}
