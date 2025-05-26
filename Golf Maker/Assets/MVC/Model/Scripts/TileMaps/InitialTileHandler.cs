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
        if (tilebase == null && tilemap.GetTile(position) != null)
        {
            hasOne = false;
            tilemap.SetTile(position, tilebase);
            return false;
        }

        if (tilebase == null) return false;

        if (hasOne)
        {
            Debug.LogError("SingleTileMapHandler can only have one tile at a time. Attempted to set another tile.");
            return false;
        }

        tilemap.SetTile(position, tilebase);
        hasOne = true;
        return true;

    }

}
