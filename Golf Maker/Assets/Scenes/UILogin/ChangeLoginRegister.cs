using UnityEngine;
using UnityEngine.UIElements;

public class ChangeLoginRegister : MonoBehaviour
{
    private VisualElement root;
    private VisualElement formContainer;

    void OnEnable()
    {
        // Obtén el root del UXML cargado
        var uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;

        formContainer = root.Q<VisualElement>("formContainer");

        // Botones
        var loginButton = root.Q<Button>("loginButton");
        var signInButton = root.Q<Button>("signInButton");

        // Eventos
        loginButton.clicked += OnLoginClicked;
        signInButton.clicked += OnSignInClicked;
    }

    void OnLoginClicked()
    {
        Debug.Log("Login con usuario y contraseña...");
        // Aquí iría tu lógica de login
    }

    void OnSignInClicked()
    {
        formContainer.Clear(); // Borra campos actuales

        // Usuario
        var usernameField = new TextField("Username");
        usernameField.name = "registerUsername";
        usernameField.style.width = 375;
        usernameField.style.height = 50;
        usernameField.style.fontSize = 24;
        usernameField.style.marginBottom = 20;
        formContainer.Add(usernameField);

        // Correo
        var emailField = new TextField("Correo");
        emailField.name = "registerEmail";
        emailField.style.width = 375;
        emailField.style.height = 50;
        emailField.style.fontSize = 24;
        emailField.style.marginBottom = 20;
        formContainer.Add(emailField);

        // Contraseña
        var passwordField = new TextField("Contraseña");
        passwordField.name = "registerPassword";
        passwordField.isPasswordField = true;
        passwordField.style.width = 375;
        passwordField.style.height = 50;
        passwordField.style.fontSize = 24;
        passwordField.style.marginBottom = 40;
        formContainer.Add(passwordField);

        // Botón Registrar
        var registerButton = new Button(() =>
        {
            Debug.Log("Registrando usuario...");
            // Aquí va la lógica de registro
        });
        registerButton.text = "Registrar";
        registerButton.style.width = 375;
        registerButton.style.height = 120;
        registerButton.style.fontSize = 35;
        registerButton.style.marginBottom = 60;
        formContainer.Add(registerButton);
    }
}
