using UnityEngine;

[System.Serializable]
public struct Effect
{
    public string name;
    public GameObject prefab;
}



[CreateAssetMenu(fileName = "EffectStorage", menuName = "Scriptable Objects/EffectStorage")]
public class EffectStorage : ScriptableObject
{
    public Effect[] effects;
    
}
