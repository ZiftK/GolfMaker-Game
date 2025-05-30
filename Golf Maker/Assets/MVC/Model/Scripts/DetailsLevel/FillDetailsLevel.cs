using UnityEngine.UIElements;
using System.Threading.Tasks;
using UnityEngine;

public class LevelDetailView : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;

    private Label levelNameLabel;
    private Label authorLabel;
    private Label levelIdLabel;
    private Label averageRatingLabel;

    private async void Start()
    {
        await FillDataDetail();
    }

    private async Task FillDataDetail()
    {
        var root = uiDocument.rootVisualElement;

        // Obtener referencias UI
        levelNameLabel = root.Q<Label>("LevelNameLabel");
        authorLabel = root.Q<Label>("AuthorLabel");
        levelIdLabel = root.Q<Label>("LevelIdLabel");
        averageRatingLabel = root.Q<Label>("AverageRatingLabel");

        // Obtener nivel actual
        var level = EnvDataHandler.Instance.GetCurrentInEditionLevel();
        if (level == null)
        {
            Debug.LogWarning("No hay nivel en edición.");
            return;
        }

        // Llenar datos base
        levelNameLabel.text = $"Level Name: {level.dificultad}";
        authorLabel.text = $"Author: {level.id_usuario}";
        levelIdLabel.text = $"ID: {level.id_nivel}";

        // Obtener rating promedio
        try
        {
            float avgRating = await ServerRatingRepository.GetInstance().GetAverageRatingByLevel(level.id_nivel);
            averageRatingLabel.text = $"Average Rating: {avgRating:F1} ★";
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error obteniendo rating promedio: {ex.Message}");
            averageRatingLabel.text = "Average Rating: N/A";
        }
    }
}
