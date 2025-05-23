using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.Linq;

public class FillLevels : MonoBehaviour
{
    public VisualTreeAsset blockLevelCardTemplate;
    public UIDocument uIDocument;

    private VisualElement levelsGrid;
    public class LevelData
    {
        public string name;
    }


    void Start()
    {
        if (uIDocument == null)
        {
            uIDocument = GetComponent<UIDocument>();
            if (uIDocument == null)
            {
                Debug.LogError("UIDocument not found on the GameObject.");
                return;
            }
        }

        var root = uIDocument.rootVisualElement;
        levelsGrid = root.Q<VisualElement>("card-container");


        List<LevelData> levels = new List<LevelData>
        {
            new() { name = "Level 1" },
            new() { name = "Level 2" },
            new() { name = "Level 3" },
            new() { name = "Level 4" },
            new() { name = "Level 5" }
        };

        RenderLevels(levels);
    }

    public void RenderLevels(List<LevelData> levels)
    {
        levelsGrid.Clear();

        foreach (var level in levels)
        {
            var card = blockLevelCardTemplate.CloneTree();
            var levelName = card.Q<Label>("LevelName");
            Debug.Log(card);
            levelName.text = level.name;

            card.RegisterCallback<ClickEvent>(_ =>
            {
                Debug.Log("Level clicked: " + level.name);
            });

            levelsGrid.Add(card);
        }
    }
}
