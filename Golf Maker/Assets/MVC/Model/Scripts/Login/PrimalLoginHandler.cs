using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Security.Cryptography;
using System.Text;

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


    void Awake()
    {
        if (EnvDataHandler.Instance.HasData())
        {
            SwitchCambas(1);
        }

    }

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
        // Reset input fields after submission
        usernameInputField.text = string.Empty;
        passwordInputField.text = string.Empty;
        SwitchCambas(1);

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
