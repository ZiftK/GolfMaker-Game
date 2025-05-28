using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrimalLoginHandler : MonoBehaviour
{

    public TMP_InputField usernameInputField;
    public TMP_InputField passwordInputField;


    public void OnButtonClickEvent()
    {
        
        string username = usernameInputField.text;
        string password = passwordInputField.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            Debug.LogError("Username or password cannot be empty.");
            return;
        }

        

        // Reset input fields after submission
        usernameInputField.text = string.Empty;
        passwordInputField.text = string.Empty;
    }
}
