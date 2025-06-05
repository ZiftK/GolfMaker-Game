using UnityEngine;
using UnityEngine.Tilemaps;

public interface ITileHandler
{
    public bool SetTile(Vector3Int position, TileBase tilebase);
    public void ClearAllTiles();
}

public class MultipleTileMapHandler : MonoBehaviour, ITileHandler
{
    protected Tilemap tilemap;

    public void ClearAllTiles()
    {
        tilemap = GetTilemap();
        tilemap.ClearAllTiles();
    }

    public bool SetTile(Vector3Int position, TileBase tilebase)
    {
        tilemap = GetTilemap();
        Debug.Log(tilemap.cellBounds.size);
        tilemap.SetTile(position, tilebase);
        return true;
    }

    protected Tilemap GetTilemap()
    {
        if (tilemap == null)
        {
            tilemap = GetComponent<Tilemap>();
            if (tilemap == null)
            {
                Debug.LogError("Tilemap component not found on the GameObject.");
            }
        }
        return tilemap;
    }
    
}
