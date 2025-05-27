using UnityEngine;
using UnityEngine.Tilemaps;

public class SingleTileMapHandler : MonoBehaviour, ITileHandler
{
    private bool hasOne = false;

    protected Tilemap tilemap;

    public void ClearAllTiles()
    {
        tilemap = GetTilemap();
        tilemap.ClearAllTiles();
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

    public bool SetTile(Vector3Int position, TileBase tilebase)
    {

        tilemap = GetTilemap();

        // If tilebase is null, we are removing the tile
        if (tilebase == null && hasOne)
        {
            tilemap.SetTile(position, null);
            hasOne = false;
            return true;
        }

        // Check if there is already a tile in the tilemap
        if (hasOne)
        {
            Debug.LogError("SingleTileMapHandler can only have one tile at a time. Attempted to set another tile.");
            return false;
        }

        if (tilebase == null) return false;

        // Set the tile and mark that the tilemap now has one tile
        tilemap.SetTile(position, tilebase);
        hasOne = true;
        return true;

    }

}
