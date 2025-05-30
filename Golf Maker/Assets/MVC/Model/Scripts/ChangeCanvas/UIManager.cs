using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Canvas for Login")]
    public UIDocument loginCanvas;

    [Header("Main menu")]
    public Canvas mainCanvas;

    [Header("Canvas Play or Design")]
    public Canvas playOrDesignCanvas;

    [Header("My Levels Canvas")]
    public UIDocument myLevelsCanvas;

    [Header("Levels Design Canvas")]
    public UIDocument levelDesignCanvas;

    [Header("Level List Canvas")]
    public UIDocument levelList;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void SwitchCanvas(int id)
    {

        loginCanvas.gameObject.SetActive(false);
        mainCanvas.gameObject.SetActive(false);
        playOrDesignCanvas.gameObject.SetActive(false);
        levelList.gameObject.SetActive(false);
        myLevelsCanvas.rootVisualElement.style.display = DisplayStyle.None;
        levelDesignCanvas.rootVisualElement.style.display = DisplayStyle.None;

        switch (id)
        {
            case 0:
                loginCanvas.gameObject.SetActive(true);
                break;
            case 1:
                mainCanvas.gameObject.SetActive(true);
                break;
            case 2:
                playOrDesignCanvas.gameObject.SetActive(true);
                break;
            case 3:
                levelList.gameObject.SetActive(true);
                break;
            case 4:
                myLevelsCanvas.rootVisualElement.style.display = DisplayStyle.Flex;
                break;
            case 5:
                levelDesignCanvas.rootVisualElement.style.display = DisplayStyle.Flex;
                break;
        }
    }

    
    public void ShowLogin() => SwitchCanvas(0);
    public void ShowMainMenu() => SwitchCanvas(1);
    public void ShowPlayDesignMenu() => SwitchCanvas(2);
    public void ShowLevelList() => SwitchCanvas(3);
    public void ShowMyLevels() => SwitchCanvas(4);
    public void ShowLevelDesigner() => SwitchCanvas(5);
}
