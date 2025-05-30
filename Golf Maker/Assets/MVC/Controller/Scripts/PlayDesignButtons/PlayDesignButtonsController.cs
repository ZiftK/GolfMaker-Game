using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayDesignButtonsController : MonoBehaviour
{
    public void OnClickDesign()
    {
        UIManager.Instance.ShowNewLevel();
    }

    public void OnClickPlay()
    {
        Debug.Log("Esto lleva a la pantalla de lista de niveles");
    }
}
