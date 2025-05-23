using UnityEngine;
using UnityEngine.UIElements;

public class RepositoryManager : MonoBehaviour
{
    public UIDocument uIDocument;

    private VisualElement saveButton;
    private VisualElement resetButton;

    private EditorLevelHandler editorLevelHandler;


    public void Awake()
    {
        editorLevelHandler = EditorLevelHandler.GetInstance();

        var root = uIDocument.rootVisualElement;
        
        saveButton = root.Q<Button>("save-tool");
        saveButton.RegisterCallback<ClickEvent>(_ =>
        {
            editorLevelHandler.OnSaveLevel();
        });

        resetButton = root.Q<Button>("reset-tool");
        resetButton.RegisterCallback<ClickEvent>(_ =>
        {
            editorLevelHandler.OnLoadLevel();
        });
    }
}
