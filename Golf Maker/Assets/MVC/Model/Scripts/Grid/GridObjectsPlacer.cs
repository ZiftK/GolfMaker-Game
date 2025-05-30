using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEditor.Rendering;
using UnityEngine;


[SerializeField]
public struct PlaceObject {
    public string name;
    public Vector3Int position;

}
[RequireComponent(typeof(PlaceObjectsFactory))]
public class GridObjectsPlacer : MonoBehaviour
{
    List<PlaceObject> objectsInGrid;

    Dictionary<Vector3Int, GameObject> objectsPos;

    PlaceObjectsFactory placeObjectsFactory;

    float tileBaseWidth;

    void Awake()
    {
        placeObjectsFactory = gameObject.GetComponent<PlaceObjectsFactory>();
    }
    public void InitGridObjectsPlacer(float tileBaseWidth)
    {
        objectsInGrid = new List<PlaceObject>();
        objectsPos = new Dictionary<Vector3Int, GameObject>();
        

        this.tileBaseWidth = tileBaseWidth;

        // string json = JsonConvert.SerializeObject(objectInGrid);
    }

    public void PlaceObjectAtPosition(Vector3Int position, string placeObjectName)
    {
        if (objectsPos.ContainsKey(position)) return;

        GameObject prefab = PlaceObjectsFactory.GetPlaceObjectByName(placeObjectName);
        GameObject instance = Instantiate(prefab, transform);

        instance.transform.position = position + new Vector3(tileBaseWidth, tileBaseWidth, 0);

        objectsPos.Add(position, instance);
    }
    public void PlaceObjectAtPosition(Vector3Int position, int id)
    {
        if (objectsPos.ContainsKey(position)) return;

        GameObject prefab = PlaceObjectsFactory.GetPlaceObjectById(id);
        GameObject instance = Instantiate(prefab, transform);

        instance.transform.position = position + new Vector3(tileBaseWidth, tileBaseWidth, 0);

        objectsPos.Add(position, instance);
    }

    public void RemoveObjectAtPosition(Vector3Int position)
    {
        if (!objectsPos.ContainsKey(position)) return;

        objectsPos.TryGetValue(position, out GameObject instance);

        Destroy(instance);
    }

    public string GetParsedStructure() => LevelParser.SerializeLevelObjects(objectsInGrid);
    public void SetFromParsedStructure(string serializedStruct)
    {
        objectsInGrid = LevelParser.DeserializeLevelObjects(serializedStruct);

        foreach (PlaceObject placeObject in objectsInGrid)
        {
            PlaceObjectAtPosition(placeObject.position, placeObject.name);
        }
    }
}
