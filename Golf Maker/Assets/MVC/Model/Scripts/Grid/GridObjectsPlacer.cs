using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEditor.Rendering;
using UnityEngine;


[SerializeField]
public struct PlaceObject
{
    public string name;
    public Vector3Int position;

    public PlaceObject(string name, Vector3Int position)
    {
        this.name = name;
        this.position = position;
    }

}
[RequireComponent(typeof(PlaceObjectsFactory))]
public class GridObjectsPlacer : MonoBehaviour
{
    Dictionary<Vector3Int, PlaceObject> objectsInGrid;

    Dictionary<Vector3Int, GameObject> objectsPos;

    PlaceObjectsFactory placeObjectsFactory;

    float tileBaseWidth;

    void Awake()
    {
        placeObjectsFactory = gameObject.GetComponent<PlaceObjectsFactory>();
    }
    public void InitGridObjectsPlacer(float tileBaseWidth)
    {
        objectsInGrid = new Dictionary<Vector3Int, PlaceObject>();
        objectsPos = new Dictionary<Vector3Int, GameObject>();
        

        this.tileBaseWidth = tileBaseWidth;

        // string json = JsonConvert.SerializeObject(objectInGrid);
    }

    private Vector3 FixPosition(Vector3Int tilePosition) => tilePosition + new Vector3(tileBaseWidth, tileBaseWidth, 0);

    private void RecordObject(Vector3Int position, GameObject instance, string placeObjectName)
    {
        objectsPos.Add(position, instance);
        objectsInGrid.Add(position, new PlaceObject(placeObjectName, position));
    }


    public void PlaceObjectAtPosition(Vector3Int position, string placeObjectName)
    {
        if (objectsPos.ContainsKey(position)) return;

        GameObject prefab = PlaceObjectsFactory.GetPlaceObjectByName(placeObjectName);
        if (prefab == null)
        {
            Debug.LogError($"[GridObjectsPlacer] El prefab para '{placeObjectName}' es null. Revisa PlaceObjectsFactory y el storage.");
            return;
        }
        GameObject instance = Instantiate(prefab, transform);

        instance.transform.position = FixPosition(position);
        RecordObject(position, instance, placeObjectName);
    }
    public void PlaceObjectAtPosition(Vector3Int position, int id)
    {
        if (objectsPos.ContainsKey(position)) return;

        GameObject prefab = PlaceObjectsFactory.GetPlaceObjectById(id, out string name);
        if (prefab == null)
        {
            Debug.LogError($"[GridObjectsPlacer] El prefab para id '{id}' (nombre: {name}) es null. Revisa PlaceObjectsFactory y el storage.");
            return;
        }
        GameObject instance = Instantiate(prefab, transform);

        instance.transform.position = FixPosition(position);
        RecordObject(position, instance, name);
    }

    public void RemoveObjectAtPosition(Vector3Int position)
    {
        if (!objectsPos.ContainsKey(position)) return;

        objectsPos.TryGetValue(position, out GameObject instance);

        Destroy(instance);
        objectsPos.Remove(position);
        objectsInGrid.Remove(position);
    }

    public string GetParsedStructure() => LevelParser.SerializeLevelObjects(objectsInGrid.Values.ToList());
    
    public void SetFromParsedStructure(string serializedStruct)
    {
        var objectList = LevelParser.DeserializeLevelObjects(serializedStruct);

        foreach (PlaceObject placeObject in objectList)
        {
            PlaceObjectAtPosition(placeObject.position, placeObject.name);
        }
    }
}
