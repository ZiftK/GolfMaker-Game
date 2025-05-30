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

    void Awake()
    {
        var root = uIDocument.rootVisualElement;

        nameTextBox = root.Q<TextField>("name");
        descriptionTextBox = root.Q<TextField>("description");
        difficultEnum = root.Q<EnumField>("dificult");
        createButton = root.Q<Button>("create-button");

        createButton.RegisterCallback<ClickEvent>(_ =>
            {
                CreateLevel();
            }
        );

    }

    public void CreateLevel()
    {
        var level = new LevelEntity
        {
            id_nivel = -1,
            nombre = nameTextBox.text,
            descripcion = descriptionTextBox.text,
            dificultad = difficultEnum.value.ToString()
        };
        EnvDataHandler.Instance.SetLevelInEditionData(level);
        SceneManager.LoadScene("LevelCreator");
    }
}
