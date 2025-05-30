using UnityEngine;
using UnityEngine.UIElements;
using System.Threading.Tasks;

public class LevelDetailView : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;

    private Label levelNameLabel;
    private Label authorLabel;
    private Label levelIdLabel;
    private VisualElement ratingContainer;

    private const int maxStars = 5;
    private string baseStarPath = "Assets/MVC/View/Front-end/assets/star_";

    private async void OnEnable()
    {
        await FillDataDetail();
    }

    private async Task FillDataDetail()
    {
        var root = uiDocument.rootVisualElement;

        levelNameLabel = root.Q<Label>("LevelNameLabel");
        authorLabel = root.Q<Label>("AuthorLabel");
        levelIdLabel = root.Q<Label>("LevelIdLabel");
        ratingContainer = root.Q<VisualElement>("RatingContainer");

        var level = EnvDataHandler.Instance.GetCurrentInEditionLevel();
        if (level == null)
        {
            Debug.LogWarning("No level found in edition.");
            return;
        }


        authorLabel.text = $"Author: {level.id_usuario}";
        levelIdLabel.text = $"ID: {level.id_nivel}";

        try
        {
            float avgRating = await ServerRatingRepository.GetInstance().GetAverageRatingByLevel(level.id_nivel);

            DisplayRatingStars(avgRating);
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error getting rating: {ex.Message}");
        }
    }

    private void DisplayRatingStars(float rating)
    {
        ratingContainer.Clear();

        int fullStars = Mathf.FloorToInt(rating);
        bool hasHalfStar = rating - fullStars >= 0.5f;

        for (int i = 0; i < maxStars; i++)
        {
            var star = new VisualElement();
            star.style.width = 24;
            star.style.height = 24;

            string spritePath;

            if (i < fullStars)
                spritePath = $"{baseStarPath}5.png"; // full star
            else if (i == fullStars && !hasHalfStar)
                spritePath = $"{baseStarPath}4.png"; // empty star (no half star)
            else if (i == fullStars && hasHalfStar)
                spritePath = $"{baseStarPath}3.png"; // half star (you should have this asset)
            else if (i < fullStars + 1)
                spritePath = $"{baseStarPath}2.png"; // quarter star (you should have this asset)
            else
                spritePath = $"{baseStarPath}1.png"; // empty star

            star.style.backgroundImage = new StyleBackground(LoadSprite(spritePath));
            ratingContainer.Add(star);
        }
    }

    private Sprite LoadSprite(string path)
    {
#if UNITY_EDITOR
        return UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite>(path);
#else
                return null;
#endif
    }
}
