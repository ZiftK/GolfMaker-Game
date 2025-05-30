using UnityEngine;
using UnityEngine.UIElements;

public class RatingForm : MonoBehaviour
{
    private VisualElement[] stars;
    private int selectedRating = 0;
    private const int TOTAL_STARS = 5;
    private UIDocument uiDoc;


    void OnEnable()
    {
        uiDoc = GetComponent<UIDocument>();
        var root = uiDoc.rootVisualElement;

        stars = new VisualElement[TOTAL_STARS];

        for (int i = 0; i < TOTAL_STARS; i++)
        {
            int index = i;
            var star = root.Q<VisualElement>($"star{index + 1}");
            stars[index] = star;

            star.RegisterCallback<ClickEvent>(_ => OnClick(index));
        }
    }

    void OnClick(int index)
    {
        selectedRating = index + 1;
        UpdateStarVisuals();
        Debug.Log($"Selected rating: {selectedRating}");
    }

    void UpdateStarVisuals()
    {
        for (int i = 0; i < TOTAL_STARS; i++)
        {
            string path = i < selectedRating
                ? $"stars/star_{selectedRating}"
                : "stars/star_empty";

            stars[i].style.backgroundImage = new StyleBackground(Resources.Load<Texture2D>(path));
        }
    }
}
