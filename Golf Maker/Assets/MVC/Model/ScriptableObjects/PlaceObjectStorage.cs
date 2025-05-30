using System;
using UnityEngine;

[Serializable]
public struct PlaceObjectConfig
{
    [NonSerialized]
    public int id;
    public string name;
    public GameObject prefab;
}
[CreateAssetMenu(fileName = "PlaceObjectStorage", menuName = "Scriptable Objects/PlaceObjectStorage")]
public class PlaceObjectStorage : ScriptableObject
{

    public PlaceObjectConfig[] placeObjectConfigs;
}
