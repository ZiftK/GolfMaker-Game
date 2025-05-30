using UnityEngine;
using UnityEngine.UIElements;

public class PlayingModeUIController : MonoBehaviour
{
    private UIDocument uiDocument;
    private VisualElement root;
    private VisualElement pauseOverlay;
    private VisualElement winOverlay;
    private Button pauseButton;
    private Button resumeButton;
    private Button exitButton;
    private Button continueButton;

    private void Awake()
    {
        // Get the UIDocument component
        uiDocument = GetComponent<UIDocument>();
        if (uiDocument == null)
        {
            Debug.LogError("UIDocument component not found!");
            return;
        }

        // Get the root element
        root = uiDocument.rootVisualElement;
        
        // Get UI elements
        pauseOverlay = root.Q("PauseOverlay");
        winOverlay = root.Q("WinOverlay");
        pauseButton = root.Q<Button>("PauseButton");
        resumeButton = root.Q<Button>("ResumeButton");
        exitButton = root.Q<Button>("ExitButton");
        continueButton = root.Q<Button>("ContinueButton");

        // Add click events
        pauseButton?.RegisterCallback<ClickEvent>(OnPauseClick);
        resumeButton?.RegisterCallback<ClickEvent>(OnResumeClick);
        exitButton?.RegisterCallback<ClickEvent>(OnExitClick);
        continueButton?.RegisterCallback<ClickEvent>(OnContinueClick);
    }

    private void OnPauseClick(ClickEvent evt)
    {
        Time.timeScale = 0;
        pauseOverlay.style.display = DisplayStyle.Flex;
    }

    private void OnResumeClick(ClickEvent evt)
    {
        Time.timeScale = 1;
        pauseOverlay.style.display = DisplayStyle.None;
    }

    private void OnExitClick(ClickEvent evt)
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    private void OnContinueClick(ClickEvent evt)
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void ShowWinOverlay(int coins)
    {
        winOverlay.style.display = DisplayStyle.Flex;
        Label coinsLabel = root.Q<Label>("CoinsLabel");
        if (coinsLabel != null)
        {
            coinsLabel.text = $"Monedas obtenidas: {coins}";
        }
    }
} 