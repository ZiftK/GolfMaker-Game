using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Grid))]
public class Grid2D : MonoBehaviour
{

    #region //hd: map values
    [SerializeField]
    [Range(10,300)]
    private int mapWidth = 30;

    [SerializeField]
    [Range(10,300)]
    private int mapHeight = 30;

    #endregion

    #region //hd: visuals

    [SerializeField]
    private GameObject visualGrid;

    private int globalTilingId;

    #endregion

    #region //hd: tilemap values

    private Grid gridComponent;

    [SerializeField]
    private TileBase[] tileBases;

    /** Un tile map por cada tile base*/
    private Dictionary<int, GameObject> tileMapsByTileBase;

    #endregion 

    #region //hd: TileBase draw control

    #endregion
    private void Awake() {
        
        SuscribeEvents();
        InitVisualGrid();
        InitGrid();
        // TestInitTileMaps();
    }


    private void SuscribeEvents(){
        PencilEventsHandler pencilEventsHandler = PencilEventsHandler.GetInstance();
        pencilEventsHandler.DrawTileBaseAtPosition += DrawTileBaseAtPositions;
    }

    private void InitVisualGrid(){
        visualGrid  = Instantiate(visualGrid);
        visualGrid.transform.localScale = new Vector3(mapWidth, mapHeight);
        visualGrid.transform.SetParent(gameObject.transform);
        visualGrid.transform.SetLocalPositionAndRotation(
            Vector3.zero, Quaternion.Euler(0,0,0)
        );
        
        globalTilingId = Shader.PropertyToID("_GlobalTiling");
        Material shaderMaterial = visualGrid.GetComponent<Renderer>().material;
        shaderMaterial.SetVector(globalTilingId, new Vector2(mapWidth, mapHeight));
    }

    private void InitGrid(){
        gridComponent = gameObject.AddComponent<Grid>();
    }

    /**
        Create a new game object of a tile map by tileBaseId and add it to the record
    */
    private GameObject NewTileMapObj(int tileBaseId){
        // create tile map object
            GameObject newTileMapObject = new GameObject($"Tile map - {tileBaseId}");
            // relative position
            newTileMapObject.transform.SetParent(transform);
            newTileMapObject.transform.SetLocalPositionAndRotation(
                Vector3.zero,
                Quaternion.identity
            );
            
            // add components
            newTileMapObject.AddComponent<Tilemap>();
            newTileMapObject.AddComponent<TilemapRenderer>();
            // physics
            Rigidbody2D tileMapRgb = newTileMapObject.AddComponent<Rigidbody2D>();
            tileMapRgb.bodyType = RigidbodyType2D.Static;
            TilemapCollider2D tileMapCollider = newTileMapObject.AddComponent<TilemapCollider2D>();
            newTileMapObject.AddComponent<CompositeCollider2D>();
            tileMapCollider.compositeOperation = Collider2D.CompositeOperation.Merge;

            this.tileMapsByTileBase.Add(tileBaseId, newTileMapObject);

            return newTileMapObject;
    }

    private void DrawTileBaseAtPositions(object sender, DrawTileBaseAtPositionsArgs args){
        
        if (!tileMapsByTileBase.TryGetValue(args.tileBaseId, out GameObject tileMapObject)){
            tileMapObject = NewTileMapObj(args.tileBaseId);
        }

        Tilemap tileMapComponent = tileMapObject.AddComponent<Tilemap>();

        foreach (Vector3Int position in args.positions){
            tileMapComponent.SetTile(position, tileBases[args.tileBaseId]);
        }
    }


}
