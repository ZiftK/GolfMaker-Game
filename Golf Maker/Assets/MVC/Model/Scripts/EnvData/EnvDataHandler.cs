using UnityEngine;

[DefaultExecutionOrder(-300)]
public class EnvDataHandler : MonoBehaviour
{
    private UserEntity userData;
    public static EnvDataHandler Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this instance across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
        DontDestroyOnLoad(this.gameObject); // Ensure this object persists across scene loads
    }

    public void SetUserData(UserEntity user)
    {
        userData = user;
    }

    public bool HasData() => !(userData is null);

    public int GetCurrentUserId() => userData.id_usuario;
}
