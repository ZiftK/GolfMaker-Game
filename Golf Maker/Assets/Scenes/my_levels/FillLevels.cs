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
        levelsGrid = root.Q<VisualElement>("ScrollView_Father");

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
            var playButton = card.Q<Button>("PlayButton");

            if (levelName != null)
                levelName.text = level.nombre;

            if (levelIdLabel != null)
                levelIdLabel.text = $"ID: {level.id_nivel}";

            if (playButton != null)
            {
                int levelId = level.id_nivel;
                playButton.RegisterCallback<ClickEvent>(_ =>
                {
                    Debug.Log($"Playing level: {level.nombre} (ID: {levelId})");
                    
                    // Guardar los datos del nivel para usar después
                    EnvDataHandler.Instance.SetLevelToPlayData(level);
                    
                    // Cargar la escena Game
                    SceneManager.LoadScene("Game");
                    
                    // Una vez que la escena esté cargada, configurar el nivel
                    SceneManager.sceneLoaded += (scene, mode) =>
                    {
                        if (scene.name == "Game")
                        {
                            // Obtener la instancia de GridFacade
                            GridFacade gridFacade = GridFacade.Instance;
                            if (gridFacade != null)
                            {
                                // Cargar la estructura del nivel en el GridFacade
                                gridFacade.SetStructure(level.estructura_nivel);
                                
                                // Configurar el ID del nivel
                                GameLevelEvents.TriggerLoadLevel(levelId);
                                
                                // Desactivar la UI
                                UIManager.Instance.DeacTiveAll();
                            }
                            else
                            {
                                Debug.LogError("GridFacade instance not found in Game scene");
                            }
                        }
                    };
                });
            }

            levelsGrid.Add(card);
        }
    }
}
