using UnityEngine;
using UnityEngine.UIElements;

public class PencilTypeManager : MonoBehaviour
{
    public UIDocument uIDocument;

    private VisualElement blocksGrid;

    private EditorLevelHandler editorLevelHandler;

    public static bool isPointerOverUI = false;

    public class PencilData
    {
        public string icon;
        public string name;
    }

    void Awake()
    {

        editorLevelHandler = EditorLevelHandler.GetInstance();
        if (uIDocument == null)
        {
            uIDocument = GetComponent<UIDocument>();
        }


        var root = uIDocument.rootVisualElement;

        blocksGrid = root.Q<VisualElement>("tools-grid");

        var penToolButton = root.Q<Button>("pen-tool");
        penToolButton.RegisterCallback<ClickEvent>(_ =>
        {
            editorLevelHandler.OnSelectPencil(new SelectPencilArgs("pen"));
        });


        var fillToolButton = root.Q<Button>("fill-tool");
        fillToolButton.RegisterCallback<ClickEvent>(_ =>
        {
            editorLevelHandler.OnSelectPencil(new SelectPencilArgs("fill"));
        });

        var brushToolButton = root.Q<Button>("brush-tool");
        brushToolButton.RegisterCallback<ClickEvent>(_ =>
        {
            editorLevelHandler.OnSelectPencil(new SelectPencilArgs("brush"));
        });

        var rulerToolButton = root.Q<Button>("ruler-tool");
        rulerToolButton.RegisterCallback<ClickEvent>(_ =>
        {
            editorLevelHandler.OnSelectPencil(new SelectPencilArgs("ruler"));
        });

        VisualElement rootPanel = root.Q<VisualElement>("tools-panel");
        VisualElement blocksPanel = root.Q<VisualElement>("blocks-panel");

        rootPanel.RegisterCallback<PointerEnterEvent>(evt => isPointerOverUI = true);
        rootPanel.RegisterCallback<PointerLeaveEvent>(evt => isPointerOverUI = false);
        blocksPanel.RegisterCallback<PointerEnterEvent>(evt => isPointerOverUI = true);
        blocksPanel.RegisterCallback<PointerLeaveEvent>(evt => isPointerOverUI = false);

    }
}
