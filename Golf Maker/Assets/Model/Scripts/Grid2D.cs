using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Tilemaps;

// [RequireComponent(typeof(Grid))]
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

    #endregion

    #region //hd: tilemap values

    private Grid gridComponent;



    [SerializeField]
    private TileBase[] tileBases;

    private GameObject[] tileMapObjects;

    #endregion 

    private void Awake() {
        InitVisualGrid();
        InitGrid();
        // TestInitTileMaps();
    }



    private void InitVisualGrid(){
        visualGrid  = Instantiate(visualGrid);
        visualGrid.transform.localScale = new Vector3(mapWidth, mapHeight);
        visualGrid.transform.SetParent(gameObject.transform);
        visualGrid.transform.SetLocalPositionAndRotation(
            Vector3.zero, Quaternion.Euler(0,0,0)
        );
    }

    private void InitGrid(){
        gridComponent = gameObject.AddComponent<Grid>();
    }

    private void TestInitTileMaps(){

        tileMapObjects = new GameObject[tileBases.Length];

        for (int i = 0; i < tileBases.Length; i++){

            // instantiate new object
            GameObject newTileMapObject = new GameObject($"TileMapID-{i}");
            newTileMapObject.transform.SetParent(gameObject.transform); // link to this object
            newTileMapObject.transform.SetLocalPositionAndRotation( // set in the center of the map
                Vector3.zero, Quaternion.Euler(0,0,0));
            newTileMapObject.transform.localScale = new Vector3(3,3);// tune scale

            // add tile map base components
            newTileMapObject.AddComponent<Tilemap>();
            newTileMapObject.AddComponent<TilemapRenderer>();

            // add rgb
            Rigidbody2D tileRb = newTileMapObject.AddComponent<Rigidbody2D>();
            tileRb.bodyType = RigidbodyType2D.Static;

            // add colliders
            TilemapCollider2D tileMapCollider = newTileMapObject.AddComponent<TilemapCollider2D>();
            newTileMapObject.AddComponent<CompositeCollider2D>();

            
            tileMapCollider.compositeOperation = Collider2D.CompositeOperation.Merge;

            Debug.Log($"i: {i}");
            newTileMapObject.GetComponent<Tilemap>().SetTile(new Vector3Int(i,0), tileBases[i]);
            newTileMapObject.GetComponent<Tilemap>().SetTile(new Vector3Int(i,1), tileBases[i]);

            tileMapObjects[i] = newTileMapObject;
        }
    }


}
