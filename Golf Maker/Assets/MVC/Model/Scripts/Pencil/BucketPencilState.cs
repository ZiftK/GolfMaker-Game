using System.Collections.Generic;
using System.Numerics;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BucketPencilState : PencilState
{
    private static BucketPencilState instance;
    
    public static BucketPencilState GetInstance()
    {
        if (instance == null)
        {
            instance = new BucketPencilState();
        }
        return instance;
    }

    public override void OnLeftClick(PencilContext context)
    {
        this.IsDrawing = true;
    }
    public override void OnLeftUnClikc(PencilContext context)
    {
        if (!this.IsDrawing) return;
        this.IsDrawing = false;

        if (context.id == 8)
        {
            Debug.LogWarning("You cannot use the bucket pencil with the initial tile.");
            return;
        }


        int[,] mapIds = GridFacade.Instance.GetLevelIds();

        Vector3Int[] floodFillCoords = this.GetFloodPoints(Vector3Int.RoundToInt(context.position), mapIds);

        DrawTileBaseAtPositionsArgs args = new DrawTileBaseAtPositionsArgs(context.id, floodFillCoords);
        pencilEventsHandler.OnClearTemporalTiles(); // remove temporal tiles
        pencilEventsHandler.OnDrawTileBaseAtPosition(args); 
        
    }

    public  Vector3Int[] GetFloodPoints(Vector3Int position, int[,] mapIds)
    {

        Vector2Int tileMapIndex = GridFacade.Instance.ConvertTileMapPositionToLevelIndex(position);

        int pointId = mapIds[tileMapIndex.x, tileMapIndex.y];

        List<Vector3Int> floodPoints = new List<Vector3Int>();
        Queue<Vector3Int> queue = new Queue<Vector3Int>();
        HashSet<Vector3Int> visited = new HashSet<Vector3Int>();
        queue.Enqueue(position);

        Vector3Int[] directions = new Vector3Int[]
        {
            Vector3Int.left,
            Vector3Int.right,
            Vector3Int.up,
            Vector3Int.down
        };

        while (queue.Count > 0)
        {
            Vector3Int current = queue.Dequeue();
            floodPoints.Add(current);
            visited.Add(current);
            
            // Check neighbors
            foreach (Vector3Int direction in directions){
                
                // Relative position since the world to tile map position
                tileMapIndex = GridFacade.Instance.ConvertTileMapPositionToLevelIndex(current + direction);

                // Check bounds
                if (tileMapIndex.x < 0 || tileMapIndex.x >= mapIds.GetLength(0)) continue;
                if (tileMapIndex.y < 0 || tileMapIndex.y >= mapIds.GetLength(1)) continue;

                // Check if already visited
                if (visited.Contains(current + direction)) continue;

                // Check if the point is the same tile
                if (mapIds[tileMapIndex.x, tileMapIndex.y] == pointId)
                {
                    queue.Enqueue(current + direction);

                }
                visited.Add(current + direction);
            }

        }

        return floodPoints.ToArray();
        
    }


    public override void OnRightClick(PencilContext context)
    {
        this.IsBorrowing = true;
    }

    public override void OnRightUnClick(PencilContext context)
    {
        if (!this.IsBorrowing) return;

        this.IsBorrowing = false;

        Vector3Int[] floodBorrowCoords = this.GetFloodPoints(Vector3Int.RoundToInt(context.position), GridFacade.Instance.GetLevelIds());
        BorrowTileBaseAtPositionArgs args = new BorrowTileBaseAtPositionArgs(floodBorrowCoords);
        pencilEventsHandler.OnBorrowTileBaseAtPosition(args);
    }
}