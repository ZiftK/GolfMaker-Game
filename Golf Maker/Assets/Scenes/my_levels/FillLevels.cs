using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;
using System.Threading.Tasks;

public class FillLevels : MonoBehaviour
{
    public VisualTreeAsset blockLevelCardTemplate;
    public UIDocument uIDocument;

    private VisualElement levelsGrid;

    async void Start()
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

        await LoadUserLevelsAsync();
    }

    private async Task LoadUserLevelsAsync()
    {
        try
        {
            int userId = EnvDataHandler.Instance.GetCurrentUserId();
            List<LevelEntity> userLevels = await ServerLevelRepository.GetInstance().GetByUserId(userId);
            RenderLevels(userLevels);
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to load user levels: {ex.Message}");
        }
    }

    public void RenderLevels(List<LevelEntity> levels)
    {
        levelsGrid.Clear();

        foreach (var level in levels)
        {
            var card = blockLevelCardTemplate.CloneTree();
            var levelName = card.Q<Label>("LevelName");

            if (levelName != null)
                levelName.text = level.nombre;

            card.RegisterCallback<ClickEvent>(_ =>
            {
                Debug.Log("Level clicked: " + level.nombre);

            });

            levelsGrid.Add(card);
        }
    }
}
