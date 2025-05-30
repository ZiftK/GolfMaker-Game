using UnityEngine;

public class handleCanvas : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject[] canvas;

    public void OnClick(int index)
    {
        for (int i = 0; i < canvas.Length; i++)
        {
            if (canvas[i] != null)
                canvas[i].SetActive(index == i);
        }

    }
}
