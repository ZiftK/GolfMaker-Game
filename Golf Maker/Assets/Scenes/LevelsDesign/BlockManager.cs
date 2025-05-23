using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEditor;

public class BlockManager : MonoBehaviour
{
    public VisualTreeAsset blockbuttonTemplete;
    public UIDocument uIDocument;

    private VisualElement blocksGrid;

    private EditorLevelHandler editorLevelHandler;

    public class BlockData
    {
        public string icon;
        public string name;
    }

    void Awake()
    {
        if (uIDocument == null)
        {
            uIDocument = GetComponent<UIDocument>();
        }


        var root = uIDocument.rootVisualElement;
        blocksGrid = root.Q<VisualElement>("blocks-grid");
        Debug.Log(blocksGrid);




        Sprite[] blocks = Resources.LoadAll<Sprite>("Blocks");

        RenderBlocks(blocks);

        editorLevelHandler = EditorLevelHandler.GetInstance();
    }

    public void RenderBlocks(Sprite[] blocks)
    {
        blocksGrid.Clear();


        foreach (var block in blocks)
        {
            var button = blockbuttonTemplete.CloneTree();
            var icon = button.Q<VisualElement>("icon");

            icon.style.backgroundImage = new StyleBackground(block);
            icon.style.backgroundSize = new StyleBackgroundSize(new BackgroundSize(BackgroundSizeType.Cover));
            icon.AddToClassList(block.name);
            button.RegisterCallback<ClickEvent>(_ =>
            {

                
                editorLevelHandler.OnSelectBlock(new SelectBlockArgs(block.name));
            });

            blocksGrid.Add(button);
        }
    }
}