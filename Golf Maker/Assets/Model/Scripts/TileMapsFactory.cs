using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

class TileMapComponent{

    public GameObject obj;
    public TileMapConfig config;

    public TileMapComponent(GameObject obj, TileMapConfig config){
        this.config = config;
        this.obj = obj;
    }
}

public class TileMapsFactory: MonoBehaviour
{
    public TileMapStorage tileMapStorage;

    Dictionary<string, TileMapComponent> tileMapByName;

    void Awake()
    {
        tileMapByName = new Dictionary<string, TileMapComponent>();

        foreach(TileMapConfig component in tileMapStorage.tileMapComponents){
                tileMapByName.Add(component.name, new TileMapComponent(null, component));
        }
    }

    public Tilemap GetTileMap(string tileBaseName, float tileBaseWidth, out TileBase tile){

        if (!tileMapByName.ContainsKey(tileBaseName)){
            throw new System.Exception($"TileBase {tileBaseName} not exists in TileBaseStorage");
        }

        tileMapByName.TryGetValue(tileBaseName, out TileMapComponent tileMapComponent);

        if (tileMapComponent.obj == null){
            tileMapComponent.obj = new GameObject($"Tile map - {tileBaseName} - {tileMapComponent.config.id}");

            // relative position
            tileMapComponent.obj.transform.SetParent(transform);

            tileMapComponent.obj.transform.SetLocalPositionAndRotation(
                new Vector3(-tileBaseWidth, -tileBaseWidth, 0),
                Quaternion.identity
            );
            
            // add components
            tileMapComponent.obj.AddComponent<Tilemap>();
            tileMapComponent.obj.AddComponent<TilemapRenderer>();
            // physics
            Rigidbody2D tileMapRgb = tileMapComponent.obj.AddComponent<Rigidbody2D>();
            tileMapRgb.bodyType = RigidbodyType2D.Static;
            TilemapCollider2D tileMapCollider = tileMapComponent.obj.AddComponent<TilemapCollider2D>();
            tileMapComponent.obj.AddComponent<CompositeCollider2D>();
            tileMapCollider.compositeOperation = Collider2D.CompositeOperation.Merge;
        }

        tile = tileMapComponent.config.tileBase;

        
        return tileMapComponent.obj.GetComponent<Tilemap>();
    }

    public Tilemap GetTileMap(int id, float tileBaseWidth, out TileBase tile){
        if (id < 0 || id >= tileMapStorage.tileMapComponents.Length){
            throw new System.Exception($"TileBase id {id} out of range");
        }

        Tilemap tileMap = GetTileMap(tileMapStorage.tileMapComponents[id].name, tileBaseWidth, out TileBase tile1);
        tile = tile1;
        return tileMap;
    }

    public Tilemap GetTileMap(int id, float tileBaseWidth){
        return GetTileMap(tileMapStorage.tileMapComponents[id].name, tileBaseWidth, out TileBase tile1);
    }

    public Tilemap GetTileMap(string tileBaseName, float tileBaseWidth){
        return GetTileMap(tileBaseName, tileBaseWidth, out TileBase tileBase);
    }


}
