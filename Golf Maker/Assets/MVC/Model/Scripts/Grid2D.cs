using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Grid), typeof(Tilemap))]
[DefaultExecutionOrder(-200)]
public class Grid2D : MonoBehaviour
{
    public static Grid2D Instance { get; private set; }

    #region //hd: level values
    [Header("Level values")]

    [SerializeField]
    [Range(10, 300)]
    [Tooltip("Width of the level")]
    private int levelWidth = 30;

    [SerializeField]
    [Range(10, 300)]
    [Tooltip("Height of the level")]
    private int levelHeight = 30;

    private int[,] levelIds;

    #endregion

    #region //hd: visuals

    [SerializeField]
    private GameObject visualGrid;

    private int globalTilingId;

    private Tilemap temporalTileLevel;

    #endregion

    #region //hd: tilelevel values

    [SerializeField]
    float tileBaseWidth = 0.5f;

    private TileMapsFactory tileLevelsFactory;


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
        // TestInitTileLevels();
    }

    private void InitVariables()
    {

        levelWidth = levelWidth % 2 == 0 ? levelWidth : levelWidth + 1;
        levelHeight = levelHeight % 2 == 0 ? levelHeight : levelHeight + 1;

        levelIds = new int[levelWidth, levelHeight];

        for (int i = 0; i < levelIds.GetLength(0); i++)
        {
            for (int j = 0; j < levelIds.GetLength(1); j++)
            {
                levelIds[i, j] = -1;
            }
        }
    }

    private void GetComponents()
    {
        tileLevelsFactory = gameObject.GetComponent<TileMapsFactory>();
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
        visualGrid.transform.localScale = new Vector3(levelWidth, levelHeight);
        visualGrid.transform.SetParent(gameObject.transform);
        visualGrid.transform.SetLocalPositionAndRotation(
            Vector3.zero, Quaternion.Euler(0, 0, 0)
        );

        globalTilingId = Shader.PropertyToID("_GlobalTiling");
        Material shaderMaterial = visualGrid.GetComponent<Renderer>().material;
        shaderMaterial.SetVector(globalTilingId, new Vector2(levelWidth, levelHeight));

        // temporal tile level
        GameObject temporalTileLevelObj = new GameObject("Temporal tile level");
        temporalTileLevelObj.transform.SetParent(transform);
        temporalTileLevelObj.transform.SetPositionAndRotation(
                new Vector3(-tileBaseWidth, -tileBaseWidth, 0),
                Quaternion.identity
            );
        temporalTileLevel = temporalTileLevelObj.AddComponent<Tilemap>();
        temporalTileLevelObj.AddComponent<TilemapRenderer>();
    }

    public void ActivateVisualGrid(bool activate)
    {
        if (visualGrid != null)
        {
            visualGrid.SetActive(activate);
        }
    }

    public Vector2Int ConvertTileMapPositionToLevelIndex(Vector3Int position) => new Vector2Int(position.x + levelWidth / 2, position.y + levelHeight / 2);
    private void SetIdAtPosition(Vector2Int idPosition, int newId)
    {
        if (idPosition.x < 0 || idPosition.x >= levelWidth)
            return;
        if (idPosition.y < 0 || idPosition.y >= levelHeight)
            return;

        levelIds[idPosition.x, idPosition.y] = newId;
    }

    private int GetIdAtPosition(Vector2Int idPosition)
    {
        if (idPosition.x < 0 || idPosition.x >= levelWidth)
            return -1;
        if (idPosition.y < 0 || idPosition.y >= levelHeight)
            return -1;

        return levelIds[idPosition.x, idPosition.y];
    }


    private void TemporalDrawTileBaseAtPositions(object sender, DrawTileBaseAtPositionsArgs args)
    {
        TileMapComponent tileLevelComponent = tileLevelsFactory.GetTileMapComponent(args.tileBaseId, tileBaseWidth);
        TileBase tile = tileLevelComponent.config.temporalTileBase;

        foreach (Vector3Int position in args.positions)
        {
            Vector2Int idPosition = ConvertTileMapPositionToLevelIndex(position);

            if (idPosition.x < 0 || idPosition.x >= levelWidth || idPosition.y < 0 || idPosition.y >= levelHeight)
            {
                break;
            }

            temporalTileLevel.SetTile(position, tile);

        }
    }

    private void ClearTemporalTiles(object sender, EventArgs e)
    {
        temporalTileLevel.ClearAllTiles();
    }

    private void DrawTileBaseAtPositions(object sender, DrawTileBaseAtPositionsArgs args)
    {

        if (args.tileBaseId == -1)
        {
            return;
        }
        BorrowTileBaseAtPositionArgs borrowArgs = new BorrowTileBaseAtPositionArgs(args.positions);
        BorrowTileBaseAtPositions(sender, borrowArgs);

        TileMapComponent tileLevelComponent = tileLevelsFactory.GetTileMapComponent(args.tileBaseId, tileBaseWidth);
        TileBase tile = tileLevelComponent.config.tileBase;
        Tilemap tilelevel = tileLevelComponent.obj.GetComponent<Tilemap>();

        foreach (Vector3Int position in args.positions)
        {

            Vector2Int idPosition = ConvertTileMapPositionToLevelIndex(position);
            SetIdAtPosition(idPosition, args.tileBaseId);

            tilelevel.SetTile(position, tile);

        }
    }

    private void BorrowTileBaseAtPositions(object sender, BorrowTileBaseAtPositionArgs args)
    {

        foreach (Vector3Int position in args.positions)
        {

            Vector2Int idPosition = ConvertTileMapPositionToLevelIndex(position);
            int id = GetIdAtPosition(idPosition);
            if (id == -1)
            {
                continue;
            }
            // recovery id tile level
            TileMapComponent tileLevelComponent = tileLevelsFactory.GetTileMapComponent(id, tileBaseWidth);
            TileBase tile = tileLevelComponent.config.tileBase;
            Tilemap tilelevel = tileLevelComponent.obj.GetComponent<Tilemap>();

            tilelevel.SetTile(position, null);
            SetIdAtPosition(idPosition, -1);
        }

    }

    public void ClearAllTiles()
    {
        foreach (Transform child in transform)
        {
            Tilemap tilemap = child.gameObject.GetComponent<Tilemap>();
            if (tilemap != null)
            {
                tilemap.ClearAllTiles();
            }
        }
    }
    public void LoadLevelFromParseLevel(int[,] levelIds)
    {

        ClearAllTiles();

        for (int i = 0; i < levelIds.GetLength(0); i++)
        {
            for (int j = 0; j < levelIds.GetLength(1); j++)
            {
                Vector3Int position = new Vector3Int(i - levelWidth / 2, j - levelHeight / 2, 0);
                DrawTileBaseAtPositions(this, new DrawTileBaseAtPositionsArgs(levelIds[i, j], position));
            }
        }

        this.levelIds = levelIds;

    }

    public int[,] GetLevelIds()
    {
        return levelIds;
    }

    public int GetLevelWidth()
    {
        return levelWidth;
    }
    public int GetLevelHeight()
    {
        return levelHeight;
    }

    public void SetLevelWidth(int newWidth)
    {
        if (newWidth < 0)
        {
            newWidth = Mathf.Abs(newWidth);
        }

        if (newWidth % 2 != 0)
        {
            newWidth++;
        }
        levelWidth = newWidth;
    }
    
    public void SetLevelHeight(int newHeight)
    {
        if (newHeight < 0)
        {
            newHeight = Mathf.Abs(newHeight);
        }

        if (newHeight % 2 != 0)
        {
            newHeight++;
        }
        levelHeight = newHeight;
    }

}
