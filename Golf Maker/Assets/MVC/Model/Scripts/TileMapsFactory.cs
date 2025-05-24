using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapComponent
{

    public GameObject obj;
    public TileMapConfig config;

    public TileMapComponent(GameObject obj, TileMapConfig config)
    {
        this.config = config;
        this.obj = obj;
    }

    public int GetComponentLayer()
    {
        switch (config.tileType)
        {
            case TileType.Solid:
                return 6;
            case TileType.Background:
                return 6;
            default:
                throw new System.Exception($"TileBase {config.name} has an invalid tile type");
        }
    }
}

[DefaultExecutionOrder(-199)]
public class TileMapsFactory : MonoBehaviour
{
    public TileMapStorage tileMapStorage;

    private static Dictionary<string, TileMapComponent> tileMapByName;

    void Awake()
    {
        tileMapByName = new Dictionary<string, TileMapComponent>();

        foreach (TileMapConfig component in tileMapStorage.tileMapComponents)
        {
            tileMapByName.Add(component.name, new TileMapComponent(null, component));
        }
    }

    public void BuildTileColliderByType(ref CompositeCollider2D compositeCollider, TileMapComponent tileMapComponent)
    {
        switch (tileMapComponent.config.tileType)
        {
            case TileType.Solid:
                compositeCollider.isTrigger = false;
                compositeCollider.sharedMaterial = tileMapComponent.config.physicsMaterial;
                break;

            case TileType.Background:
                compositeCollider.isTrigger = true;
                // todo: add viscosity implementation
                break;

        }
        

    }
    public void BuildTileMapComponent(string tileBaseName, float tileBaseWidth, ref TileMapComponent tileMapComponent)
    {

        tileMapComponent.obj = new GameObject($"Tile map - {tileBaseName} - {tileMapComponent.config.id}");

        // set collision layer mask
        tileMapComponent.obj.layer = tileMapComponent.GetComponentLayer();

        // relative position
        tileMapComponent.obj.transform.SetParent(transform);

        tileMapComponent.obj.transform.SetLocalPositionAndRotation(
            new Vector3(-tileBaseWidth, -tileBaseWidth, 0),
            Quaternion.identity
        );

        // add components
        tileMapComponent.obj.AddComponent<Tilemap>();
        var tileMapRenderer = tileMapComponent.obj.AddComponent<TilemapRenderer>();

        if (tileMapComponent.config.material != null)
        {
            tileMapRenderer.material = tileMapComponent.config.material;
        }

        tileMapRenderer.sortingOrder = tileMapComponent.config.sortingLayer;

        // physics
        Rigidbody2D tileMapRgb = tileMapComponent.obj.AddComponent<Rigidbody2D>();
        tileMapRgb.bodyType = RigidbodyType2D.Static;

        // Collisions
        TilemapCollider2D tileMapCollider = tileMapComponent.obj.AddComponent<TilemapCollider2D>();
        CompositeCollider2D compositeCollider = tileMapComponent.obj.AddComponent<CompositeCollider2D>();
        tileMapCollider.compositeOperation = Collider2D.CompositeOperation.Merge;


        BuildTileColliderByType(ref compositeCollider, tileMapComponent);
        
    }
    
    public TileMapComponent GetTileMapComponent(string tileBaseName, float tileBaseWidth)
    {
        /*
        se revisa que existe un tile con ese nombre,
        este caso se diferencia de obtener un tilemapComponent null porque
        el nombre del tile puede estar registrado pero no tener un tilemapComponent, ya
        que los tile components se inicializan a null para identificar los que ya han sido 
        construidos de los que no.
        */
        if (!tileMapByName.ContainsKey(tileBaseName))
        {
            throw new System.Exception($"TileBase {tileBaseName} not exists in TileBaseStorage");
        }

        tileMapByName.TryGetValue(tileBaseName, out TileMapComponent tileMapComponent);

        if (tileMapComponent.obj == null)
        {
            BuildTileMapComponent(tileBaseName, tileBaseWidth, ref tileMapComponent);
        }

        return tileMapComponent;
    }

    public TileMapComponent GetTileMapComponent(int tileBaseId, float tileBaseWidth)
    {
        if (tileBaseId < 0 || tileBaseId >= tileMapStorage.tileMapComponents.Length)
        {
            throw new System.Exception($"Tile base id {tileBaseId} not exists");
        }
        string name = tileMapStorage.tileMapComponents[tileBaseId].name;
        return GetTileMapComponent(name, tileBaseWidth);
    }
    
    public static int GetTileIdByName(string tileBaseName)
    {
        tileMapByName.TryGetValue(tileBaseName, out TileMapComponent tileMapComponent);
        if (tileMapComponent == null)
        {
            throw new System.Exception($"TileBase {tileBaseName} not exists in TileBaseStorage");
        }
        return tileMapComponent.config.id;
    }


}
