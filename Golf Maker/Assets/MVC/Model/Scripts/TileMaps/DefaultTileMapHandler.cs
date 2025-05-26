using UnityEngine;
using UnityEngine.Tilemaps;

public interface ITileHandler
{
    public void SetTile(Vector3Int position, TileBase tilebase);
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

    public void SetTile(Vector3Int position, TileBase tilebase)
    {
        tilemap = GetTilemap();
        tilemap.SetTile(position, tilebase);
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
