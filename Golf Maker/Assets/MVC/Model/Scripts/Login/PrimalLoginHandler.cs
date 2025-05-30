using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Security.Cryptography;
using System.Text;

public class PrimalLoginHandler : MonoBehaviour
{
    [Header("UI References")]
    public TMP_InputField usernameInputField;
    public TMP_InputField passwordInputField;
    public TMP_InputField confirmPasswordInputField; // Para registro

    [Header("UI Buttons")]
    public Button loginButton;
    public Button switchModeButton;

    [Header("UI Labels")]
    public TMP_Text titleLabel;
    public TMP_Text loginButtonText;
    public TMP_Text switchButtonText;
    public TMP_Text errorMessageText; // Para mostrar errores

    [Header("Settings")]
    public bool startInLoginMode = true;
    public string successSceneName = "LevelCreator";

    private bool isLoginMode;

    private void Start()
    {
        isLoginMode = startInLoginMode;
        SetupUI();
        UpdateUIForCurrentMode();
    }

    private void SetupUI()
    {
        // Configurar eventos de botones
        if (loginButton != null)
            loginButton.onClick.AddListener(() => _ = OnMainButtonClick());

        if (switchModeButton != null)
            switchModeButton.onClick.AddListener(SwitchMode);

        // Ocultar campo de confirmación inicialmente
        if (confirmPasswordInputField != null)
            confirmPasswordInputField.gameObject.SetActive(false);

        // Ocultar mensaje de error inicialmente
        if (errorMessageText != null)
            errorMessageText.gameObject.SetActive(false);
    }

    public void OnButtonClickEvent()
    {
        _ = OnMainButtonClick();
    }

    public async Task OnMainButtonClick()
    {
        if (isLoginMode)
        {
            await TryLogin();
        }
        else
        {
            await TryRegister();
        }
    }

    public void SwitchMode()
    {
        isLoginMode = !isLoginMode;
        UpdateUIForCurrentMode();
        ClearFields();
        HideErrorMessage();
    }

    private void UpdateUIForCurrentMode()
    {
        if (isLoginMode)
        {
            // Modo Login
            if (titleLabel != null) titleLabel.text = "Inicia Sesión";
            if (loginButtonText != null) loginButtonText.text = "Iniciar Sesión";
            if (switchButtonText != null) switchButtonText.text = "Crear Cuenta";
            if (confirmPasswordInputField != null) confirmPasswordInputField.gameObject.SetActive(false);
        }
        else
        {
            // Modo Registro
            if (titleLabel != null) titleLabel.text = "Crear Cuenta";
            if (loginButtonText != null) loginButtonText.text = "Registrarse";
            if (switchButtonText != null) switchButtonText.text = "Ya tengo cuenta";
            if (confirmPasswordInputField != null) confirmPasswordInputField.gameObject.SetActive(true);
        }
    }

    public async Task TryLogin()
    {
        string username = usernameInputField.text.Trim();
        string password = passwordInputField.text;

        // Validaciones básicas
        if (!ValidateLoginInput(username, password))
            return;

        try
        {
            IUserRepository userRepository = ServerUserRepository.GetInstance();
            UserEntity user = await userRepository.GetByUsername(username);

            if (user == null)
            {
                ShowErrorMessage("Usuario no encontrado.");
                return;
            }

            // Verificar contraseña (considera usar hash en producción)
            if (!VerifyPassword(password, user.contrasenna))
            {
                ShowErrorMessage("Contraseña incorrecta.");
                return;
            }

            // Login exitoso
            await OnLoginSuccess(user);
        }
        catch (System.Exception ex)
        {
            ShowErrorMessage("Error de conexión. Inténtalo de nuevo.");
            Debug.LogError($"Login error: {ex.Message}");
        }
    }

    public async Task TryRegister()
    {
        string username = usernameInputField.text.Trim();
        string password = passwordInputField.text;
        string confirmPassword = confirmPasswordInputField.text;

        // Validaciones
        if (!ValidateRegistrationInput(username, password, confirmPassword))
            return;

        try
        {
            IUserRepository userRepository = ServerUserRepository.GetInstance();

            // Verificar si el usuario ya existe
            UserEntity existingUser = await userRepository.GetByUsername(username);
            if (existingUser != null)
            {
                ShowErrorMessage("Ya existe una cuenta con este email.");
                return;
            }

            // Crear nuevo usuario
            UserEntity newUser = CreateNewUser(username, password);

            // Aquí deberías implementar el método para crear usuarios
            // bool success = await userRepository.CreateUser(newUser);

            // Por ahora, simulamos el éxito
            ShowSuccessMessage("Cuenta creada exitosamente.");

            // Cambiar a modo login
            await Task.Delay(2000); // Mostrar mensaje por 2 segundos
            SwitchToLoginMode();
        }
        catch (System.Exception ex)
        {
            ShowErrorMessage("Error al crear la cuenta. Inténtalo de nuevo.");
            Debug.LogError($"Registration error: {ex.Message}");
        }
    }

    private bool ValidateLoginInput(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            ShowErrorMessage("El email y la contraseña no pueden estar vacíos.");
            return false;
        }

        if (!IsValidEmail(username))
        {
            ShowErrorMessage("Por favor, introduce un email válido.");
            return false;
        }

        return true;
    }

    private bool ValidateRegistrationInput(string username, string password, string confirmPassword)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
        {
            ShowErrorMessage("Todos los campos son obligatorios.");
            return false;
        }

        if (!IsValidEmail(username))
        {
            ShowErrorMessage("Por favor, introduce un email válido.");
            return false;
        }

        if (password.Length < 6)
        {
            ShowErrorMessage("La contraseña debe tener al menos 6 caracteres.");
            return false;
        }

        if (password != confirmPassword)
        {
            ShowErrorMessage("Las contraseñas no coinciden.");
            return false;
        }

        return true;
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email && email.Contains("@") && email.Contains(".");
        }
        catch
        {
            return false;
        }
    }

    private bool VerifyPassword(string inputPassword, string storedPassword)
    {
        // En un sistema real, deberías usar hash de contraseñas
        // Por ahora, comparación directa
        return inputPassword == storedPassword;
    }

    private string HashPassword(string password)
    {
        // Implementación básica de hash (en producción usa bcrypt o similar)
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            return System.Convert.ToBase64String(bytes);
        }
    }

    private UserEntity CreateNewUser(string username, string password)
    {
        return new UserEntity
        {
            nombre_usuario = username,
            contrasenna = password, // En producción, usar HashPassword(password)
            // Agregar otros campos según tu UserEntity
        };
    }

    private async Task OnLoginSuccess(UserEntity user)
    {
        EnvDataHandler.Instance.SetUserData(user);
        ShowSuccessMessage("Iniciando sesión...");
        await Task.Delay(1000); // Breve pausa para mostrar el mensaje
        SceneManager.LoadScene(successSceneName);
        ClearFields();
    }

    private void SwitchToLoginMode()
    {
        isLoginMode = true;
        UpdateUIForCurrentMode();
        ClearFields();
        HideErrorMessage();
    }

    private void ClearFields()
    {
        if (usernameInputField != null) usernameInputField.text = string.Empty;
        if (passwordInputField != null) passwordInputField.text = string.Empty;
        if (confirmPasswordInputField != null) confirmPasswordInputField.text = string.Empty;
    }

    private void ShowErrorMessage(string message)
    {
        Debug.LogError(message);
        if (errorMessageText != null)
        {
            errorMessageText.text = message;
            errorMessageText.color = Color.red;
            errorMessageText.gameObject.SetActive(true);
        }
    }

    private void ShowSuccessMessage(string message)
    {
        Debug.Log(message);
        if (errorMessageText != null)
        {
            errorMessageText.text = message;
            errorMessageText.color = Color.green;
            errorMessageText.gameObject.SetActive(true);
        }
    }

    private void HideErrorMessage()
    {
        if (errorMessageText != null)
            errorMessageText.gameObject.SetActive(false);
    }
}