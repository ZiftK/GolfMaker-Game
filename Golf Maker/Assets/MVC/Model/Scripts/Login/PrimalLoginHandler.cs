using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PrimalLoginHandler : MonoBehaviour
{
    private VisualElement root;
    private VisualElement formContainer;

    [Header("Main menu")]
    public Canvas mainCanvas;

    [Header("Canvas Play or Design")]
    public Canvas playOrDesignCanvas;

    [Header("My Levesl Canvas")]
    public UIDocument myLevelsCanvas;

    [Header("Leves design Canvas")]
    public UIDocument levelDesignCanvas;

    [Header("Level list canvas")]
    public UIDocument levelList;
    public GameObject content;
    public GameObject buttonPrefab;

    void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        formContainer = root.Q<VisualElement>("formContainer");
        formContainer.AddToClassList("formContainer");

        LoadLoginForm();

        if (EnvDataHandler.Instance.HasData())
        {
            SwitchCanvas(1);
        }
    }

    private void LoadLoginForm()
    {
        formContainer.Clear();

        var title = new Label("Inicia Sesión");
        title.AddToClassList("formTitle");
        formContainer.Add(title);

        var usernameField = new TextField("Email") { name = "usernameInputField" };
        usernameField.AddToClassList("inputField");
        formContainer.Add(usernameField);

        var passwordField = new TextField("Contraseña") { name = "passwordInputField", isPasswordField = true };
        passwordField.AddToClassList("inputField");
        formContainer.Add(passwordField);

        var buttonRow = new VisualElement();
        buttonRow.style.flexDirection = FlexDirection.RowReverse;
        buttonRow.style.justifyContent = Justify.SpaceBetween;
        buttonRow.style.alignItems = Align.FlexEnd;
        buttonRow.style.alignSelf = Align.Center;
        buttonRow.style.width = 828;
        buttonRow.style.flexGrow = 1;

        var loginBtn = new Button(OnLoginButtonClickEvent) { name = "loginButton", text = "Iniciar" };
        loginBtn.AddToClassList("btnLogin");

        var signInBtn = new Button(LoadRegisterForm) { name = "signInButton", text = "Registrate" };
        signInBtn.AddToClassList("btnLogin");

        buttonRow.Add(signInBtn);
        buttonRow.Add(loginBtn);

        formContainer.Add(buttonRow);
    }

    private void LoadRegisterForm()
    {
        formContainer.Clear();

        var title = new Label("Registrarse");
        title.AddToClassList("formTitle");
        formContainer.Add(title);

        var usernameField = new TextField("Username") { name = "registerUsername" };
        usernameField.AddToClassList("inputField");
        formContainer.Add(usernameField);

        var emailField = new TextField("Correo") { name = "registerEmail" };
        emailField.AddToClassList("inputField");
        formContainer.Add(emailField);

        var passwordField = new TextField("Contraseña") { name = "registerPassword", isPasswordField = true };
        passwordField.AddToClassList("inputField");
        formContainer.Add(passwordField);

        var registerBtn = new Button(async () =>
        {
            UserEntity user = new UserEntity
            {
                id_usuario = -1,
                nombre_usuario = usernameField.value,
                email = emailField.value,
                contrasenna = passwordField.value,
                fecha_registro = DateTime.Now,
                niveles_creados = 0,
                niveles_completados = 0,
                puntuacion_promedio_recibida = 0
            };

            await RegisterNewUser(user);
        })



        { text = "Registrar" };
        registerBtn.AddToClassList("btnLogin");

        var backBtn = new Button(LoadLoginForm) { text = "Volver" };
        backBtn.AddToClassList("btnLogin");

        var buttonRow = new VisualElement();
        buttonRow.style.flexDirection = FlexDirection.RowReverse;
        buttonRow.style.justifyContent = Justify.SpaceBetween;
        buttonRow.style.alignItems = Align.FlexEnd;
        buttonRow.style.alignSelf = Align.Center;
        buttonRow.style.width = 828;
        buttonRow.style.flexGrow = 1;

        buttonRow.Add(backBtn);
        buttonRow.Add(registerBtn);
    }

    public async Task RegisterNewUser(UserEntity user)
    {
        IUserRepository userRepository = ServerUserRepository.GetInstance();

        var existing = await userRepository.GetByUsername(user.nombre_usuario);
        if (existing != null)
        {
            Debug.LogError("Usuario ya existe");
            return;
        }

        await userRepository.Create(user);
        EnvDataHandler.Instance.SetUserData(user);
        Debug.Log("Usuario registrado y logueado");
        SwitchCanvas(1);
    }

    public async void OnLoginButtonClickEvent()
    {
        var usernameField = root.Q<TextField>("usernameInputField");
        var passwordField = root.Q<TextField>("passwordInputField");

        string username = usernameField?.value;
        string password = passwordField?.value;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            Debug.LogError("Username or password cannot be empty.");
            return;
        }

        IUserRepository userRepository = ServerUserRepository.GetInstance();
        UserEntity user = await userRepository.GetByUsername(username);

        if (user == null || user.contrasenna != password)
        {
            Debug.LogError("Usuario no encontrado o contraseña inválida.");
            return;
        }

        EnvDataHandler.Instance.SetUserData(user);
        SwitchCanvas(1);
    }

    public void OnCreateLevelButtonClickEvent()
    {
        SceneManager.LoadScene("LevelCreator");
    }

    public void OnLevelListButtonClickEvent()
    {
        SwitchCanvas(2);
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        _ = ShowLevelsList();
    }

    public void OnReturnFromLevelListClickEvent()
    {
        SwitchCanvas(1);
    }

    public void SwitchCanvas(int id)
    {
        mainCanvas.gameObject.SetActive(false);
        levelList.gameObject.SetActive(false);
        playOrDesignCanvas.gameObject.SetActive(false);
        myLevelsCanvas.rootVisualElement.style.display = DisplayStyle.None;
        levelDesignCanvas.rootVisualElement.style.display = DisplayStyle.None;


        switch (id)
        {
            case 1:
                mainCanvas.gameObject.SetActive(true);
                break;
            case 2:
                playOrDesignCanvas.gameObject.SetActive(true);
                break;
            case 3:
                levelList.gameObject.SetActive(true);
                break;

        }
    }

    public async Task ShowLevelsList()
    {
        try
        {
            ILevelRepository levelRepository = ServerLevelRepository.GetInstance();
            List<LevelEntity> levels = await levelRepository.GetAll();

            if (levels == null || levels.Count == 0)
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
        catch (Exception ex)
        {
            Debug.LogError($"Error al obtener la lista de niveles: {ex.Message}");
        }
    }
}
