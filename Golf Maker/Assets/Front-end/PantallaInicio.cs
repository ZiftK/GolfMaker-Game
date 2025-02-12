using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PantallaInicio : MonoBehaviour
{
    UIDocument menu;

    private void OnEnable() 
    {
        menu = GetComponent<UIDocument>();
        VisualElement root = menu.rootVisualElement;
    }
}
