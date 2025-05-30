using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Grid), typeof(Tilemap), typeof(VisualGridRenderer))]
[RequireComponent(typeof(VisualGridTileRenderer), typeof(GridIdStorage))]
[DefaultExecutionOrder(-200)]
public class GridFacade : MonoBehaviour
{
    public static GridFacade Instance { get; private set; }

    #region Level config

    [Space(10)]
    [Header("Config")]

    [Tooltip("It must be an even number. If it's not, the value will be rounded up to the next even number.")]
    [SerializeField]
    [Range(10, 60)]
    private int levelWidth;

    [Tooltip("It must be an even number. If it's not, the value will be rounded up to the next even number.")]
    [SerializeField]
    [Range(10, 60)]
    private int levelHeight;

    [Tooltip("Set the number of tiles count in the visual grid")]
    [SerializeField]
    private int globalTilingId;

    [Tooltip("Set the width and height of each tile in the grid")]
    [SerializeField]
    private float tileBaseWidth;


    #endregion Level config

    #region Visuals

    private VisualGridRenderer gridRenderer;
    private VisualGridTileRenderer tilesRenderer;

    #endregion Visuals

    #region Funcionals
    GridIdStorage idStorage;

    GridObjectsPlacer objectsPlacer;
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
        SuscribeEvents();

        gridRenderer.InitVisualGrid(levelWidth, levelHeight, globalTilingId);
        tilesRenderer.InitVisualGridTiles(tileBaseWidth);
        idStorage.InitGridIdStorage(levelWidth, levelHeight);
        objectsPlacer.InitGridObjectsPlacer();

    }

    #region Initializing methods


    private void GetComponents()
    {
        gridRenderer = gameObject.GetComponent<VisualGridRenderer>();
        tilesRenderer = gameObject.GetComponent<VisualGridTileRenderer>();
        idStorage = gameObject.GetComponent<GridIdStorage>();
        objectsPlacer = gameObject.GetComponent<GridObjectsPlacer>();
    }
    private void SuscribeEvents()
    {
        PencilEventsHandler pencilEventsHandler = PencilEventsHandler.GetInstance();
        EditorLevelEvents editorLevelEvents = EditorLevelEvents.GetInstance();

        pencilEventsHandler.DrawTileBaseAtPosition += DrawTileBaseAtPositions;
        pencilEventsHandler.BorrowTileBaseAtPosition += BorrowTileBaseAtPositions;
        pencilEventsHandler.TemporalDrawTileBaseAtPositions += TemporalDrawTileBaseAtPositions;
        pencilEventsHandler.ClearTemporalTiles += ClearTemporalTiles;

        editorLevelEvents.ResetLevel += ResetLevel;


    }

    #endregion Initializing methods

    #region Expose Methods
    public void ActivateVisualGrid(bool activate)
    {
        if (gridRenderer != null)
        {
            gridRenderer.SetActive(activate);
        }
    }

    public Vector2Int ConvertTileMapPositionToLevelIndex(Vector3Int position) => new Vector2Int(position.x + levelWidth / 2, position.y + levelHeight / 2);
    public Vector2 ConvertLevelIndexToTileMapPosition(Vector2Int position) => new Vector2(position.x - levelWidth / 2, position.y - levelHeight / 2);
    #endregion Expose Methods


    #region Map ids



    #endregion Map ids


    #region Alter tiles
    private void TemporalDrawTileBaseAtPositions(object sender, DrawTileBaseAtPositionsArgs args)
    {

        foreach (Vector3Int position in args.positions)
        {
            Vector2Int idPosition = ConvertTileMapPositionToLevelIndex(position);

            if (idPosition.x < 0 || idPosition.x >= levelWidth || idPosition.y < 0 || idPosition.y >= levelHeight)
            {
                break;
            }

            tilesRenderer.TemporalDrawTileBaseAtPosition(tileBaseWidth, position, args.tileBaseId);

        }
    }

    private void ClearTemporalTiles(object sender, EventArgs e)
    {
        tilesRenderer.ClearTemporalTiles();
    }

    private void DrawTileBaseAtPositions(object sender, DrawTileBaseAtPositionsArgs args)
    {

        if (args.tileBaseId == -1)
        {
            return;
        }
        BorrowTileBaseAtPositionArgs borrowArgs = new BorrowTileBaseAtPositionArgs(args.positions);
        BorrowTileBaseAtPositions(sender, borrowArgs);

        foreach (Vector3Int position in args.positions)
        {

            bool canSetTile = tilesRenderer.DrawTileBaseAtPosition(args.tileBaseId, tileBaseWidth, position);
            if (!canSetTile) break;

            Vector2Int idPosition = ConvertTileMapPositionToLevelIndex(position);
            idStorage.SetIdAtPosition(idPosition, args.tileBaseId);

        }
    }

    private void BorrowTileBaseAtPositions(object sender, BorrowTileBaseAtPositionArgs args)
    {

        foreach (Vector3Int position in args.positions)
        {

            Vector2Int idPosition = ConvertTileMapPositionToLevelIndex(position);
            int id = idStorage.GetIdAtPosition(idPosition);
            if (id == -1)
            {
                continue;
            }
            tilesRenderer.BorrowTileBaseAtPosition(id, tileBaseWidth, position);
            idStorage.SetIdAtPosition(idPosition, -1);
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

    #endregion Alter tiles

    public void LoadLevelFromParseLevel(int[,] levelIds)
    {

        ClearAllTiles();

        for (int i = 0; i < levelIds.GetLength(0); i++)
        {
            for (int j = 0; j < levelIds.GetLength(1); j++)
            {
                if (levelIds[i, j] == -1)
                {
                    continue; // Skip empty tiles
                }
                if (levelIds[i, j] < 0)
                {
                    Debug.LogError($"Invalid tile ID at position ({i}, {j}): {levelIds[i, j]}");
                    continue; // Skip invalid tile IDs
                }

                if (levelIds[i, j] == 8)
                {
                    Vector2 tilePosition = ConvertLevelIndexToTileMapPosition(new Vector2Int(i, j));
                    GameLevelEvents.TriggerSetBallInitialPosition(tilePosition);
                }

                Vector3Int position = new Vector3Int(i - levelWidth / 2, j - levelHeight / 2, 0);
                DrawTileBaseAtPositions(this, new DrawTileBaseAtPositionsArgs(levelIds[i, j], position));
            }
        }

        idStorage.SetAllIds(levelIds);

    }

    void ResetLevel(object sender, ResetLevelArgs args)
    {
        SetStructure(args.levelStruct);
    }

    #region Getters and Setters
    public int[,] GetLevelIds()
    {
        return idStorage.GetAllIds();
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

    public string GetStructure()
    {
        string idStruct = idStorage.GetParsedStructure();
        string objectStruct = objectsPlacer.GetParsedStructure();

        return idStruct + "$$split$$" + objectStruct;
    }

    public void SetStructure(string serializedStruct)
    {
        string[] structures = serializedStruct.Split("$$split$$");

        if (structures.Length < 2)
        {
            throw new Exception("No split structure");
        }

        idStorage.SetFromParsedStructure(structures[0]);
        objectsPlacer.SetFromParsedStructure(structures[1]);
    }
    #endregion Getters and Setters

}
