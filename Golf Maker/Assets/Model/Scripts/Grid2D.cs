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

    private TileMapsFactory tileMapsFactory;
    

    #endregion 
    private void Awake() {
        
        GetComponents();
        InitVariables();
        SuscribeEvents();
        InitVisualGrid();
        // TestInitTileMaps();
    }

    private void InitVariables(){

        mapWidth = mapWidth%2 == 0 ? mapWidth : mapWidth + 1;
        mapHeight = mapHeight%2 == 0 ? mapHeight : mapHeight + 1;

        mapIds = new int [mapWidth, mapHeight];

        for (int i = 0; i < mapIds.GetLength(0); i++){
            for (int j = 0; j < mapIds.GetLength(1); j++){
                mapIds[i,j] = -1;
            }
        }
    }

    private void GetComponents(){
        tileMapsFactory = gameObject.GetComponent<TileMapsFactory>();
    }
    private void SuscribeEvents(){
        PencilEventsHandler pencilEventsHandler = PencilEventsHandler.GetInstance();

        pencilEventsHandler.DrawTileBaseAtPosition += DrawTileBaseAtPositions;
        pencilEventsHandler.BorrowTileBaseAtPosition += BorrowTileBaseAtPositions;
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


    private Vector2Int ConvertTileMapPositionToMapIndex(Vector3Int position) => new Vector2Int(position.x + mapWidth/2, position.y + mapHeight/2);
    private void SetIdAtPosition(Vector2Int idPosition, int newId){
        mapIds[idPosition.x, idPosition.y] = newId;
    }

    private int GetIdAtPosition(Vector2Int idPosition) => mapIds[idPosition.x, idPosition.y];


    private void DrawTileBaseAtPositions(object sender, DrawTileBaseAtPositionsArgs args){
        

        Tilemap tileMapComponent = tileMapsFactory.GetTileMap(args.tileBaseId, tileBaseWidth, out TileBase tile);
        
        foreach (Vector3Int position in args.positions){
            Vector2Int idPosition = ConvertTileMapPositionToMapIndex(position);
            SetIdAtPosition(idPosition, args.tileBaseId);
            tileMapComponent.SetTile(position, tile);
        }
    }

    private void BorrowTileBaseAtPositions(object sender, BorrowTileBaseAtPositionArgs args){
        
        foreach (Vector3Int position in args.positions){
            
            Vector2Int idPosition = ConvertTileMapPositionToMapIndex(position);
            int id = GetIdAtPosition(idPosition);
            if ( id == -1){
                continue;
            }
            // recovery id tile map
            Tilemap tileMapComponent = tileMapsFactory.GetTileMap(id, tileBaseWidth);
            
            tileMapComponent.SetTile(position, null);
            SetIdAtPosition(idPosition, -1);
        }
        
    }

}
