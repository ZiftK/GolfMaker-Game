using System.Collections.Generic;
using UnityEngine;

public class PlaceObjectsFactory : MonoBehaviour
{
    static Dictionary<string, PlaceObjectConfig> map;


    public PlaceObjectStorage placeObjectStorage;

    void Awake()
    {
        map = new Dictionary<string, PlaceObjectConfig>();

        for (int i = 0; i < placeObjectStorage.placeObjectConfigs.Length; i++)
        {
            PlaceObjectConfig currentConfig = placeObjectStorage.placeObjectConfigs[i];

            currentConfig.id = i;
            map.Add(currentConfig.name, currentConfig);
        }
    }

    public GameObject GetPlaceObjectByName(string name)
    {
        if (!map.ContainsKey(name))
        {
            Debug.Log($"Not exist an object with name: {name}");
        }

        map.TryGetValue(name, out PlaceObjectConfig val);
        return val.prefab;
    }

}
