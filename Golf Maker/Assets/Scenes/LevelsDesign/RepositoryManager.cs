using UnityEngine;
using UnityEngine.UIElements;

public class RepositoryManager : MonoBehaviour
{
    public UIDocument uIDocument;

    private VisualElement saveButton;
    private VisualElement resetButton;

    private EditorLevelEvents editorLevelEvents;


    public void Awake()
    {
        editorLevelEvents = EditorLevelEvents.GetInstance();

        var root = uIDocument.rootVisualElement;
        
        saveButton = root.Q<Button>("save-tool");
        saveButton.RegisterCallback<ClickEvent>(_ =>
        {
            editorLevelEvents.OnSaveLevel();
        });

        resetButton = root.Q<Button>("reset-tool");
        resetButton.RegisterCallback<ClickEvent>(_ =>
        {
            editorLevelEvents.OnLoadLevel();
        });
    }
}
