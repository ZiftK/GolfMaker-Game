using System.Collections.Generic;
using Newtonsoft.Json;
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

    PlaceObjectsFactory placeObjectsFactory;

    void Awake()
    {
        placeObjectsFactory = gameObject.GetComponent<PlaceObjectsFactory>();
    }
    public void InitGridObjectsPlacer()
    {
        objectsInGrid = new List<PlaceObject>();
        // string json = JsonConvert.SerializeObject(objectInGrid);
    }

    public string GetParsedStructure() => LevelParser.SerializeLevelObjects(objectsInGrid);
    public void SetFromParsedStructure(string serializedStruct)
    {
        objectsInGrid = LevelParser.DeserializeLevelObjects(serializedStruct);

        foreach (PlaceObject placeObject in objectsInGrid)
        {
            GameObject prefab = placeObjectsFactory.GetPlaceObjectByName(placeObject.name);
            GameObject instance = Instantiate(prefab);

            instance.transform.position = placeObject.position;
        }
    }
}
