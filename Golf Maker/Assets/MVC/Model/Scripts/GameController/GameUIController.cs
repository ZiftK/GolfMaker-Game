using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameUIController : MonoBehaviour
{
    private VisualElement root;
    private VisualElement winOverlay;
    private Label coinsLabel;
    private Button continueButton;

    void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        winOverlay = root.Q<VisualElement>("WinOverlay");
        coinsLabel = root.Q<Label>("CoinsLabel");
        continueButton = root.Q<Button>("ContinueButton");

        continueButton.clicked += OnContinue;
    }

    public void ShowWinScreen(int coinsEarned)
    {
        coinsLabel.text = $"Monedas obtenidas: {coinsEarned}";
        winOverlay.style.display = DisplayStyle.Flex;
    }

    private void OnContinue()
    {
        // Por ejemplo, volver al menú principal
        UIManager.Instance.ShowMainMenu();
    }
}
