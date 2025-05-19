using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class BlockManager : MonoBehaviour
{
    public VisualTreeAsset blockbuttonTemplete;
    public UIDocument uIDocument;

    private VisualElement blocksGrid;

    public class BlockData
    {
        public string icon;
        public string name;
    }

    void Start()
    {
        var root = uIDocument.rootVisualElement;
        blocksGrid = root.Q<VisualElement>("blocks-grid");

        List<BlockData> blocks = new List<BlockData>
        {
            new BlockData { icon = "icon1", name = "Block 1" },
            new BlockData { icon = "icon2", name = "Block 2" },
            new BlockData { icon = "icon3", name = "Block 3" }
        };

        RenderBlocks(blocks);
    }

    public void RenderBlocks(List<BlockData> blocks)
    {
        blocksGrid.Clear();

        foreach (var block in blocks)
        {
            var button = blockbuttonTemplete.CloneTree();
            var icon = button.Q<Image>("icon");

            icon.AddToClassList(block.icon);
            button.RegisterCallback<ClickEvent>(_ =>
            {
                Debug.Log("Block clicked: " + block.name);
            });

            blocksGrid.Add(button);
        }
    }
}