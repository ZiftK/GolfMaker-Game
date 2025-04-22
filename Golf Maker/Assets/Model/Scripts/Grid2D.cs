using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Grid), typeof(Tilemap))]
public class Grid2D : MonoBehaviour
{
    public static Grid2D Instance { get; private set; }

    #region //hd: map values
    [Header("Map values")]

    [SerializeField]
    [Range(10, 300)]
    [Tooltip("Width of the map")]
    private int mapWidth = 30;

    [SerializeField]
    [Range(10, 300)]
    [Tooltip("Height of the map")]
    private int mapHeight = 30;

    private int[,] mapIds;

    #endregion

    #region //hd: visuals

    [SerializeField]
    private GameObject visualGrid;

    private int globalTilingId;

    private Tilemap temporalTileMap;

    #endregion

    #region //hd: tilemap values

    [SerializeField]
    float tileBaseWidth = 0.5f;

    private TileMapsFactory tileMapsFactory;


    #endregion
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Evita duplicados
            return;
        }

        GetComponents();
        InitVariables();
        SuscribeEvents();
        InitVisualGrid();
        // TestInitTileMaps();
    }

    private void InitVariables()
    {

        mapWidth = mapWidth % 2 == 0 ? mapWidth : mapWidth + 1;
        mapHeight = mapHeight % 2 == 0 ? mapHeight : mapHeight + 1;

        mapIds = new int[mapWidth, mapHeight];

        for (int i = 0; i < mapIds.GetLength(0); i++)
        {
            for (int j = 0; j < mapIds.GetLength(1); j++)
            {
                mapIds[i, j] = -1;
            }
        }
    }

    private void GetComponents()
    {
        tileMapsFactory = gameObject.GetComponent<TileMapsFactory>();
    }
    private void SuscribeEvents()
    {
        PencilEventsHandler pencilEventsHandler = PencilEventsHandler.GetInstance();

        pencilEventsHandler.DrawTileBaseAtPosition += DrawTileBaseAtPositions;
        pencilEventsHandler.BorrowTileBaseAtPosition += BorrowTileBaseAtPositions;
        pencilEventsHandler.TemporalDrawTileBaseAtPositions += TemporalDrawTileBaseAtPositions;
        pencilEventsHandler.ClearTemporalTiles += ClearTemporalTiles;
    }

    private void InitVisualGrid()
    {
        visualGrid = Instantiate(visualGrid);
        visualGrid.transform.localScale = new Vector3(mapWidth, mapHeight);
        visualGrid.transform.SetParent(gameObject.transform);
        visualGrid.transform.SetLocalPositionAndRotation(
            Vector3.zero, Quaternion.Euler(0, 0, 0)
        );

        globalTilingId = Shader.PropertyToID("_GlobalTiling");
        Material shaderMaterial = visualGrid.GetComponent<Renderer>().material;
        shaderMaterial.SetVector(globalTilingId, new Vector2(mapWidth, mapHeight));

        // temporal tile map
        GameObject temporalTileMapObj = new GameObject("Temporal tile map");
        temporalTileMapObj.transform.SetParent(transform);
        temporalTileMapObj.transform.SetPositionAndRotation(
                new Vector3(-tileBaseWidth, -tileBaseWidth, 0),
                Quaternion.identity
            );
        temporalTileMap = temporalTileMapObj.AddComponent<Tilemap>();
        temporalTileMapObj.AddComponent<TilemapRenderer>();
    }


    public Vector2Int ConvertTileMapPositionToMapIndex(Vector3Int position) => new Vector2Int(position.x + mapWidth / 2, position.y + mapHeight / 2);
    private void SetIdAtPosition(Vector2Int idPosition, int newId)
    {
        mapIds[idPosition.x, idPosition.y] = newId;
    }

    private int GetIdAtPosition(Vector2Int idPosition) => mapIds[idPosition.x, idPosition.y];


    private void TemporalDrawTileBaseAtPositions(object sender, DrawTileBaseAtPositionsArgs args)
    {
        TileMapComponent tileMapComponent = tileMapsFactory.GetTileMapComponent(args.tileBaseId, tileBaseWidth);
        TileBase tile = tileMapComponent.config.temporalTileBase;

        foreach (Vector3Int position in args.positions)
        {
            Vector2Int idPosition = ConvertTileMapPositionToMapIndex(position);

            temporalTileMap.SetTile(position, tile);

        }
    }

    private void ClearTemporalTiles(object sender, EventArgs e)
    {
        temporalTileMap.ClearAllTiles();
    }

    private void DrawTileBaseAtPositions(object sender, DrawTileBaseAtPositionsArgs args)
    {


        TileMapComponent tileMapComponent = tileMapsFactory.GetTileMapComponent(args.tileBaseId, tileBaseWidth);
        TileBase tile = tileMapComponent.config.tileBase;
        Tilemap tilemap = tileMapComponent.obj.GetComponent<Tilemap>();

        foreach (Vector3Int position in args.positions)
        {
            Vector2Int idPosition = ConvertTileMapPositionToMapIndex(position);
            SetIdAtPosition(idPosition, args.tileBaseId);

            tilemap.SetTile(position, tile);

        }
    }

    private void BorrowTileBaseAtPositions(object sender, BorrowTileBaseAtPositionArgs args)
    {

        foreach (Vector3Int position in args.positions)
        {

            Vector2Int idPosition = ConvertTileMapPositionToMapIndex(position);
            int id = GetIdAtPosition(idPosition);
            if (id == -1)
            {
                continue;
            }
            // recovery id tile map
            TileMapComponent tileMapComponent = tileMapsFactory.GetTileMapComponent(id, tileBaseWidth);
            TileBase tile = tileMapComponent.config.tileBase;
            Tilemap tilemap = tileMapComponent.obj.GetComponent<Tilemap>();

            tilemap.SetTile(position, null);
            SetIdAtPosition(idPosition, -1);
        }

    }

    private void LoadMapFromParseMap(int[,] mapIds){
        for (int i = 0; i < mapIds.GetLength(0); i++)
        {
            for (int j = 0; j < mapIds.GetLength(1); j++)
            {
                Vector3Int position = new Vector3Int(i - mapWidth / 2, j - mapHeight / 2, 0);
                DrawTileBaseAtPositions(this, new DrawTileBaseAtPositionsArgs(mapIds[i, j], position));
            }
        }

        this.mapIds = mapIds;
        
    }
    public int[,] GetMapIds()
    {
        return mapIds;
    }

}
