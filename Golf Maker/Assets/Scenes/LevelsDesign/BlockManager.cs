using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEditor;
using System;

public class BlockManager : MonoBehaviour
{
    public VisualTreeAsset blockbuttonTemplete;
    public UIDocument uIDocument;

    private VisualElement blocksGrid;

    private EditorLevelEvents editorLevelEvents;

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




        Sprite[] blocks = Resources.LoadAll<Sprite>("Blocks");

        RenderBlocks(blocks);

        editorLevelEvents = EditorLevelEvents.GetInstance();
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

                string[] names = block.name.Split("@");
                if (names.Length < 2)
                {
                    Debug.LogError("The name element must be splitted for a @");
                }

                PencilSetType type = Enum.Parse<PencilSetType>(names[1]);
                editorLevelEvents.OnSelectBlock(new SelectBlockArgs(names[0],type) );
            });

            blocksGrid.Add(button);
        }
    }
}