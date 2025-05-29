using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PrimalLoginHandler : MonoBehaviour
{

    [Header("Login form")]
    public Canvas loginCanvas;
    public TMP_InputField usernameInputField;
    public TMP_InputField passwordInputField;

    [Header("Main form")]
    public Canvas mainCanvas;

    [Header("Level list canvas")]
    public Canvas levelList;
    public GameObject content;
    public GameObject buttonPrefab;

    public void OnLoginButtonClickEvent()
    {
        _ = TryLogin();
    }

    public void OnCreateLevelButtonClickEvent()
    {
        SceneManager.LoadScene("LevelCreator");
    }


    public void OnLevelListButtonClickEvent()
    {
        SwitchCambas(2);
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        _ = ShowLevelsList();
    }

    public void OnReturnFromLevelListClickEvent()
    {
        SwitchCambas(1);
    }

    public void SwitchCambas(int id)
    {
        mainCanvas.gameObject.SetActive(false);
        loginCanvas.gameObject.SetActive(false);
        levelList.gameObject.SetActive(false);

        switch (id)
        {
            case 0:

                loginCanvas.gameObject.SetActive(true);
                break;
            case 1:
                mainCanvas.gameObject.SetActive(true);
                break;

            case 2:
                levelList.gameObject.SetActive(true);
                break;
            default:
                break;

        }
    }

    public async Task TryLogin()
    {

        string username = usernameInputField.text;
        string password = passwordInputField.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            Debug.LogError("Username or password cannot be empty.");
            return;
        }

        IUserRepository userRepository = ServerUserRepository.GetInstance();
        UserEntity user = await userRepository.GetByUsername(username);

        if (user == null)
        {
            Debug.LogError("User not found.");
            return;
        }

        if (user.contrasenna != password)
        {
            Debug.LogError("Invalid password.");
            return;
        }

        EnvDataHandler.Instance.SetUserData(user);
        // Reset input fields after submission
        usernameInputField.text = string.Empty;
        passwordInputField.text = string.Empty;
        SwitchCambas(1);

    }

    public async Task ShowLevelsList()
    {
        ILevelRepository levelRepository = ServerLevelRepository.GetInstance();
        List<LevelEntity> levels = await levelRepository.GetAll();

        if (levels.Count == 0)
        {
            Debug.Log("Sin niveles");
            return;
        }

        foreach (LevelEntity level in levels)
        {
            GameObject button = Instantiate(buttonPrefab, content.transform);
            button.GetComponent<LevelButton>().Customize(level);
        }
    }

}
