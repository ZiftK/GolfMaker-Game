using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

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
            await RenderLevels(userLevels);
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to load user levels: {ex.Message}");
        }
    }

    public async Task RenderLevels(List<LevelEntity> levels)
    {
        levelsGrid.Clear();

        foreach (var level in levels)
        {
            var card = blockLevelCardTemplate.CloneTree();
            var levelName = card.Q<Label>("LevelName");
            var levelIdLabel = card.Q<Label>("LevelIdLabel");
            var creatorLabel = card.Q<Label>("CreatorLabel");
            var playButton = card.Q<Button>("PlayButton");

            if (levelName != null)
                levelName.text = level.nombre;

            if (levelIdLabel != null)
                levelIdLabel.text = $"ID: {level.id_nivel}";

            if (creatorLabel != null)
                creatorLabel.text = $"Creator: {level.id_usuario}";

            if (playButton != null)
            {
                int levelId = level.id_nivel;
                playButton.RegisterCallback<ClickEvent>(_ =>
                {
                    Debug.Log("Play level: " + level.nombre + " id " + levelId);
                    EnvDataHandler.Instance.SetLevelToPlayData(level);
                    GameLevelEvents.TriggerLoadLevel(levelId);
                    GameLevelEvents.TriggerOnSetLevelStruct(level.estructura_nivel);
                    SceneManager.LoadScene("Game");
                    UIManager.Instance.DeacTiveAll();
                });
            }

            card.RegisterCallback<ClickEvent>(_ =>
            {
                Debug.Log("Level clicked: " + level.nombre + " id " + level.id_nivel);
            });

            levelsGrid.Add(card);
        }
    }
}
