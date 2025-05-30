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
    Stack<GameObject> instantiateObjects;
    HashSet<Vector3Int> positions;

    PlaceObjectsFactory placeObjectsFactory;

    float tileBaseWidth;

    void Awake()
    {
        placeObjectsFactory = gameObject.GetComponent<PlaceObjectsFactory>();
    }
    public void InitGridObjectsPlacer(float tileBaseWidth)
    {
        objectsInGrid = new List<PlaceObject>();
        positions = new HashSet<Vector3Int>();
        instantiateObjects = new Stack<GameObject>();

        this.tileBaseWidth = tileBaseWidth;

        // string json = JsonConvert.SerializeObject(objectInGrid);
    }

    public void PlaceObjectAtPosition(Vector3Int position, string placeObjectName)
    {
        if (positions.Contains(position)) return;

        GameObject prefab = PlaceObjectsFactory.GetPlaceObjectByName(placeObjectName);
        GameObject instance = Instantiate(prefab);

        instance.transform.position = position + new Vector3(tileBaseWidth, tileBaseWidth, 0);

        instantiateObjects.Push(instance);
        positions.Add(position);
    }
    public void PlaceObjectAtPosition(Vector3Int position, int id)
    {
        if (positions.Contains(position)) return;

        GameObject prefab = PlaceObjectsFactory.GetPlaceObjectById(id);
        GameObject instance = Instantiate(prefab);

        instance.transform.position = position + new Vector3(tileBaseWidth, tileBaseWidth, 0);
        instantiateObjects.Push(instance);
        positions.Add(position);
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
