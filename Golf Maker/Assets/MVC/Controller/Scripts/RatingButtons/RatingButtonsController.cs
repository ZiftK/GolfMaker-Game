using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System;
using System.Threading.Tasks;

public class RatingForm : MonoBehaviour
{
    private VisualElement root;
    private TextField commentField;
    private Button submitButton;
    private Button cancelButton;

    private const int maxStars = 5;
    private VisualElement[] stars = new VisualElement[maxStars];
    private int selectedRating = 0;

    private string baseStarPath = "Assets/MVC/View/Front-end/assets/star_";
    private LevelEntity currentLevel;

    void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        commentField = root.Q<TextField>("commentField");
        submitButton = root.Q<Button>("submitButton");
        cancelButton = root.Q<Button>("cancelButton");

        for (int i = 0; i < maxStars; i++)
        {
            int index = i;
            stars[i] = root.Q<VisualElement>($"star{index + 1}");

            stars[i].RegisterCallback<PointerEnterEvent>(_ => HighlightStars(index + 1));
            stars[i].RegisterCallback<PointerLeaveEvent>(_ => HighlightStars(selectedRating));
            stars[i].RegisterCallback<ClickEvent>(_ =>
            {
                selectedRating = index + 1;
                HighlightStars(selectedRating);
            });
        }

        cancelButton.clicked += OnCancel;
        submitButton.clicked += async () => await OnSubmit();

        currentLevel = EnvDataHandler.Instance.GetCurrentInEditionLevel();

        if (currentLevel == null)
        {
            Debug.LogError("No level found in edition.");
            submitButton.SetEnabled(false);
        }
    }

    private void HighlightStars(int upTo)
    {
        for (int i = 0; i < maxStars; i++)
        {
            string spritePath = i < upTo ? "5" : "1";
            stars[i].style.backgroundImage = new StyleBackground(LoadSprite($"{baseStarPath}{spritePath}.png"));
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

    private void OnCancel()
    {
        EnvDataHandler.Instance.ClearLevelInEditionData();
        UIManager.Instance.ShowLevelList();
    }

    private async Task OnSubmit()
    {
        if (selectedRating == 0)
        {
            Debug.LogWarning("You must select a rating.");
            return;
        }

        try
        {
            RatingEntity rating = new RatingEntity
            {
                IdUsuario = EnvDataHandler.Instance.GetCurrentUserId(),
                IdNivel = currentLevel.id_nivel,
                Calificacion = selectedRating,
                Comentario = commentField.text
            };

            await ServerRatingRepository.GetInstance().Create(rating);
            Debug.Log("Rating successfully submitted.");

            UIManager.Instance.ShowLevelList();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to submit rating: {ex.Message}");
        }
    }
}
