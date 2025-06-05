using System.Collections.Generic;
using System.Data;
using System.Linq;
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

    public static GameObject GetPlaceObjectByName(string name)
    {
        if (!map.ContainsKey(name))
        {
            Debug.LogError($"[PlaceObjectsFactory] No existe un objeto con el nombre: {name} en el diccionario. Revisa el storage.");
            return null;
        }

        map.TryGetValue(name, out PlaceObjectConfig val);
        return val.prefab;
    }

    public static GameObject GetPlaceObjectById(int id)
    {
        List<PlaceObjectConfig> configs = map.Values.Where(config => config.id == id).ToList();
        if (configs.Count == 0)
        {
            Debug.LogError($"[PlaceObjectsFactory] No existe un objeto con el id: {id} en el diccionario. Revisa el storage.");
            return null;
        }
        return configs[0].prefab;
    }

    public static GameObject GetPlaceObjectById(int id, out string name)
    {
        List<PlaceObjectConfig> configs = map.Values.Where(config => config.id == id).ToList();
        if (configs.Count == 0)
        {
            Debug.LogError($"[PlaceObjectsFactory] No existe un objeto con el id: {id} en el diccionario. Revisa el storage.");
            name = null;
            return null;
        }
        name = configs[0].name;
        return configs[0].prefab;
    }

    public static int GetPlaceObjectIdByName(string name)
    {
        if (!map.ContainsKey(name))
        {
            Debug.Log($"Not exist an object with name: {name}");
        }

        map.TryGetValue(name, out PlaceObjectConfig val);
        return val.id;
    }

}
