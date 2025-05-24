using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public enum TileType
{
    Solid,
    Background
}

[Serializable]
public struct TileMapConfig
{

    public string name;
    public int id;
    public TileBase tileBase;
    public TileBase temporalTileBase;

    public TileType tileType;

    public Material material;

    [Header("If is a solid block")]
    public PhysicsMaterial2D physicsMaterial;

    [Header("If is a background block")]
    public float viscosity;
    
    
}
[CreateAssetMenu(fileName = "TileMapStorage", menuName = "Scriptable Objects/TileMapStorage")]
public class TileMapStorage : ScriptableObject
{
    public TileMapConfig[] tileMapComponents;
}
