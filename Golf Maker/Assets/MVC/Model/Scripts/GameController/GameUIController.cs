using UnityEngine;
using UnityEngine.UIElements;

public class GameUIController : MonoBehaviour
{
    private VisualElement root;
    private VisualElement pauseMenu;
    private Button pauseButton;
    private Button resumeButton;
    private Button exitButton;
    private VisualElement pauseOverlay;

    void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        pauseButton = root.Q<Button>("PauseButton");
        pauseOverlay = root.Q<VisualElement>("PauseOverlay");
        resumeButton = root.Q<Button>("ResumeButton");
        exitButton = root.Q<Button>("ExitButton");

        pauseButton.clicked += PauseGame;
        resumeButton.clicked += ResumeGame;
        exitButton.clicked += ExitGame;
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        pauseOverlay.style.display = DisplayStyle.Flex;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
        pauseOverlay.style.display = DisplayStyle.None;
    }

    void ExitGame()
    {
        Time.timeScale = 1;
        UIManager.Instance.ShowMainMenu();
    }
}
