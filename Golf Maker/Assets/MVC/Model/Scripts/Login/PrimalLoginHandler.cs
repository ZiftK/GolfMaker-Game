using UnityEngine;
using UnityEngine.UIElements;

public class ChangeLoginRegister : MonoBehaviour
{
    private VisualElement root;
    private VisualElement formContainer;

    void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;

        formContainer = root.Q<VisualElement>("formContainer");
        formContainer.AddToClassList("formContainer");

        var loginButton = root.Q<Button>("loginButton");
        var signInButton = root.Q<Button>("signInButton");

        loginButton.clicked += ReloadLoginForm;
        signInButton.clicked += ShowRegisterForm;
    }

    private void ReloadLoginForm()
    {
        formContainer.Clear();

        // Label
        var titleLabel = new Label("Inicia Sesión");
        titleLabel.style.fontSize = 71;
        titleLabel.style.unityTextAlign = TextAnchor.UpperCenter;
        titleLabel.style.color = Color.white;
        formContainer.Add(titleLabel);

        // Email input
        var usernameField = new TextField("Email") { name = "usernameInputField" };
        usernameField.AddToClassList("inputField");
        formContainer.Add(usernameField);

        // Password input
        var passwordField = new TextField("Contraseña") { name = "passwordInputField", isPasswordField = true };
        passwordField.AddToClassList("inputField");
        formContainer.Add(passwordField);

        // Buttons row
        var buttonRow = new VisualElement();
        buttonRow.style.flexDirection = FlexDirection.RowReverse;
        buttonRow.style.justifyContent = Justify.SpaceBetween;
        buttonRow.style.alignItems = Align.FlexEnd;
        buttonRow.style.alignSelf = Align.Center;
        buttonRow.style.width = 828;
        buttonRow.style.flexGrow = 1;

        var signInButton = new Button(ShowRegisterForm) { name = "signInButton", text = "Registrate" };
        signInButton.AddToClassList("btnLogin");

        var loginButton = new Button(() =>
        {
            // Este botón puede disparar un evento si es necesario
            Debug.Log("Iniciar sesión desde reload");
        })
        { name = "loginButton", text = "Iniciar" };
        loginButton.AddToClassList("btnLogin");

        buttonRow.Add(signInButton);
        buttonRow.Add(loginButton);

        formContainer.Add(buttonRow);
    }

    private void ShowRegisterForm()
    {
        formContainer.Clear();

        var title = new Label("Registrarse");
        title.style.fontSize = 60;
        title.style.unityTextAlign = TextAnchor.MiddleCenter;
        title.style.color = Color.white;
        title.style.marginBottom = 40;
        formContainer.Add(title);

        var usernameField = new TextField("Username");
        usernameField.AddToClassList("inputField");
        formContainer.Add(usernameField);

        var emailField = new TextField("Correo");
        emailField.AddToClassList("inputField");
        formContainer.Add(emailField);

        var passwordField = new TextField("Contraseña") { isPasswordField = true };
        passwordField.AddToClassList("inputField");
        formContainer.Add(passwordField);

        var registerButton = new Button(() =>
        {
            Debug.Log("Simulación de registro...");
        })
        {
            text = "Registrar"
        };
        registerButton.AddToClassList("btnLogin");
        formContainer.Add(registerButton);

        var returnButton = new Button(ReloadLoginForm)
        {
            text = "Volver"
        };
        returnButton.AddToClassList("btnLogin");
        formContainer.Add(returnButton);
    }
}
