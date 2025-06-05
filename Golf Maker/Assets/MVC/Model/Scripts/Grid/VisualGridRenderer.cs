using UnityEditor.EditorTools;
using UnityEngine;

public class VisualGridRenderer : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField]
    private GameObject visualGridPrefab;

    private GameObject visualGridInstance;

    public void InitVisualGrid(
        int levelWidth,
        int levelHeight,
        int globalTilingId
    )
    {
        if (visualGridInstance is null)
        {
            if (visualGridPrefab == null)
            {
                Debug.LogError("[VisualGridRenderer] visualGridPrefab is null. Asigna el prefab en el inspector.");
                return;
            }
            visualGridInstance = Instantiate(visualGridPrefab);
        }

        visualGridInstance.transform.localScale = new Vector3(levelWidth, levelHeight);
        visualGridInstance.transform.SetParent(gameObject.transform);
        visualGridInstance.transform.SetLocalPositionAndRotation(
            Vector3.zero, Quaternion.Euler(0, 0, 0)
        );

        globalTilingId = Shader.PropertyToID("_GlobalTiling");
        Material shaderMaterial = visualGridInstance.GetComponent<Renderer>().material;
        shaderMaterial.SetVector(globalTilingId, new Vector2(levelWidth, levelHeight));
    }

    public void SetActive(bool active) => visualGridInstance.gameObject.SetActive(active);

}
