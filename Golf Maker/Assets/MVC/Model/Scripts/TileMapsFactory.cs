using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapComponent{

    public GameObject obj;
    public TileMapConfig config;

    public TileMapComponent(GameObject obj, TileMapConfig config){
        this.config = config;
        this.obj = obj;
    }
}

[DefaultExecutionOrder(-199)]
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

    public TileMapComponent GetTileMapComponent(string tileBaseName, float tileBaseWidth){

        if (!tileMapByName.ContainsKey(tileBaseName)){
            throw new System.Exception($"TileBase {tileBaseName} not exists in TileBaseStorage");
        }

        tileMapByName.TryGetValue(tileBaseName, out TileMapComponent tileMapComponent);

        if (tileMapComponent.obj == null){
            tileMapComponent.obj = new GameObject($"Tile map - {tileBaseName} - {tileMapComponent.config.id}");

            // set collision layer mask
            Debug.Log("TileMapComponent: " + tileMapComponent.config.objectLayer);
            tileMapComponent.obj.layer = tileMapComponent.config.objectLayer;

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
            CompositeCollider2D compositeCollider = tileMapComponent.obj.AddComponent<CompositeCollider2D>();
            tileMapCollider.compositeOperation = Collider2D.CompositeOperation.Merge;
            compositeCollider.sharedMaterial = tileMapComponent.config.physicsMaterial;
        }
        
        return tileMapComponent;
    }

    public TileMapComponent GetTileMapComponent(int tileBaseId, float tileBaseWidth){
        if (tileBaseId < 0 || tileBaseId >= tileMapStorage.tileMapComponents.Length){
            throw new System.Exception($"Tile base id {tileBaseId} not exists");
        }
        string name = tileMapStorage.tileMapComponents[tileBaseId].name;
        return GetTileMapComponent(name, tileBaseWidth);
    }


}
