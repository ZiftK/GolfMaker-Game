using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrimalLoginHandler : MonoBehaviour
{

    public TMP_InputField usernameInputField;
    public TMP_InputField passwordInputField;

    public void OnButtonClickEvent()
    {
        _ = TryLogin();
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

        Debug.Log("Login successful.");

        // Reset input fields after submission
        usernameInputField.text = string.Empty;
        passwordInputField.text = string.Empty;
    }
}
