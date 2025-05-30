using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;


[SerializeField]
public struct ObjectInGrid {
    public int id;
    public Vector3Int position;

}
public class GridObjectsPlacer : MonoBehaviour
{
    List<ObjectInGrid> objectsInGrid;

    public void InitGridObjectsPlacer()
    {
        objectsInGrid = new List<ObjectInGrid>();
        // string json = JsonConvert.SerializeObject(objectInGrid);
    }

    public string GetParsedStructure() => LevelParser.SerializeLevelObjects(objectsInGrid);
    public void SetFromParsedStructure(string serializedStruct)
    {
        objectsInGrid = LevelParser.DeserializeLevelObjects(serializedStruct);
    }
}
