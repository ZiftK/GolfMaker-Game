using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public struct TileMapConfig{
    
    public string name;
    public int id;
    public TileBase tileBase;
    public TileBase temporalTileBase;
    
}
[CreateAssetMenu(fileName = "TileMapStorage", menuName = "Scriptable Objects/TileMapStorage")]
public class TileMapStorage : ScriptableObject
{
    public TileMapConfig[] tileMapComponents;
}
