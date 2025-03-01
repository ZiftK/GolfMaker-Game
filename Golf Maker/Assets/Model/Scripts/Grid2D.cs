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

    private int [,] mapIds;

    #endregion

    #region //hd: visuals

    [SerializeField]
    private GameObject visualGrid;

    private int globalTilingId;

    #endregion

    #region //hd: tilemap values

    private Grid gridComponent;

    [SerializeField]
    float tileBaseWidth = 0.5f;

    [SerializeField]
    private TileBase[] tileBases;

    /** Un tile map por cada tile base*/
    private Dictionary<int, GameObject> tileMapsByTileBase;
    

    #endregion 
    private void Awake() {
        
        InitVariables();
        SuscribeEvents();
        InitVisualGrid();
        InitGrid();
        // TestInitTileMaps();
    }

    private void InitVariables(){
        tileMapsByTileBase = new Dictionary<int, GameObject>();

        mapWidth = mapWidth%2 == 0 ? mapWidth : mapWidth + 1;
        mapHeight = mapHeight%2 == 0 ? mapHeight : mapHeight + 1;

        mapIds = new int [mapWidth, mapHeight];

        for (int i = 0; i < mapIds.GetLength(0); i++){
            for (int j = 0; j < mapIds.GetLength(1); j++){
                mapIds[i,j] = -1;
            }
        }
    }

    private void SuscribeEvents(){
        PencilEventsHandler pencilEventsHandler = PencilEventsHandler.GetInstance();

        pencilEventsHandler.DrawTileBaseAtPosition += DrawTileBaseAtPositions;
        // pencilEventsHandler.BorrowTileBaseAtPosition += BorrowTileBaseAtPositions;
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


    private Vector2Int ConvertTileMapPositionToMapIndex(Vector3Int position) => new Vector2Int(position.x + mapWidth/2, position.y + mapHeight/2);
    private GameObject NewTileMapObj(int tileBaseId){
        // create tile map object
            GameObject newTileMapObject = new GameObject($"Tile map - {tileBaseId}");
            // relative position
            newTileMapObject.transform.SetParent(transform);

            newTileMapObject.transform.SetLocalPositionAndRotation(
                new Vector3(-tileBaseWidth, -tileBaseWidth, 0),
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

        Tilemap tileMapComponent = tileMapObject.GetComponent<Tilemap>();

        foreach (Vector3Int position in args.positions){
            
            Debug.Log("Puesto en posición: " + ConvertTileMapPositionToMapIndex(position));
            tileMapComponent.SetTile(position, tileBases[args.tileBaseId]);
        }
    }

    // private void BorrowTileBaseAtPositions(object sender, BorrowTileBaseAtPositionArgs args){
    //     if (!tileMapsByTileBase.TryGetValue(args.tileBaseId, out GameObject tileMapObject)){
    //         tileMapObject = NewTileMapObj(args.tileBaseId);
    //     }

    //     Tilemap tileMapComponent = tileMapObject.GetComponent<Tilemap>();

    //     foreach (Vector3Int position in args.positions){
            
    //         tileMapComponent.RefreshTile(position);
    //     }
    // }

}
