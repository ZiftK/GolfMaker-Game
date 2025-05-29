using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class LevelButton : MonoBehaviour
{
    private LevelEntity levelData;

    public TMP_Text buttonText;
    void Awake()
    {
        buttonText = gameObject.GetComponentInChildren<TMP_Text>();
    }

    public void Customize(LevelEntity levelData)
    {
        this.levelData = levelData;
        
        buttonText.text = levelData.nombre;
    }

}
