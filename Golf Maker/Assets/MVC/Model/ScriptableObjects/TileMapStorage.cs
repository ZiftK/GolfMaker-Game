using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public enum TileType
{
    Solid,
    Background
}

[Serializable]
public enum TileSetType
{
    Multiple,
    Single
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

    public int sortingLayer;

    public TileSetType tileSetType;

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

public static class TileMapStorageConversions
{
    public static ITileHandler GetTileHandler(TileMapConfig config, GameObject tileMapObject)
    {
        TileSetType tileSetType = config.tileSetType;

        switch (tileSetType)
        {
            case TileSetType.Multiple:
                return tileMapObject.GetComponent<MultipleTileMapHandler>();

            case TileSetType.Single:
                return tileMapObject.GetComponent<SingleTileMapHandler>();

            default:
                throw new Exception($"TileBase {config.name} has an invalid tile set type");
        }
    }

    public static void AddTileHandler(ref TileMapComponent tileMapComponent)
    {
        TileSetType tileSetType = tileMapComponent.config.tileSetType;

        switch (tileSetType)
        {
            case TileSetType.Multiple:
                tileMapComponent.obj.AddComponent<MultipleTileMapHandler>();
                break;

            case TileSetType.Single:
                tileMapComponent.obj.AddComponent<SingleTileMapHandler>();
                break;

            default:
                throw new Exception($"TileBase {tileMapComponent.config.name} has an invalid tile set type");
        }
    }
}