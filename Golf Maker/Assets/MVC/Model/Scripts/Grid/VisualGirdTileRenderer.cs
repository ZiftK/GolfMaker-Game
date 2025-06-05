using System.Data.Common;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(TileMapsFactory))]
public class VisualGridTileRenderer : MonoBehaviour
{

    private Tilemap temporalTileLevel;

    private TileMapsFactory tileMapsFactory;

    void Awake()
    {
        tileMapsFactory = gameObject.GetComponent<TileMapsFactory>();
    }

    void SuscribeEvents()
    {

    }

    public void InitVisualGridTiles(
        float tileBaseWidth
    )
    {
        //todo: poner esto en algun lugar menos este
        GameObject temporalTileLevelObj = new GameObject("Temporal tile level");
        temporalTileLevelObj.transform.SetParent(transform);
        temporalTileLevelObj.transform.SetPositionAndRotation(
                new Vector3(-tileBaseWidth, -tileBaseWidth, 0),
                Quaternion.identity
            );

        temporalTileLevel = temporalTileLevelObj.AddComponent<Tilemap>();
        temporalTileLevelObj.AddComponent<TilemapRenderer>();
    }

    public void TemporalDrawTileBaseAtPosition(
        float tileBaseWidth,
        Vector3Int position,
        int tileBaseId
    )
    {
        TileMapComponent tileLevelComponent = tileMapsFactory.GetTileMapComponent(tileBaseId, tileBaseWidth);
        TileBase tile = tileLevelComponent.config.temporalTileBase;
        temporalTileLevel.SetTile(position, tile);
    }

    public void ClearTemporalTiles()
    {
        temporalTileLevel.ClearAllTiles();
    }

    public bool DrawTileBaseAtPosition(
        int tileBaseId,
        float tileBaseWidth,
        Vector3Int position
    )
    {
        TileMapComponent tileLevelComponent = tileMapsFactory.GetTileMapComponent(tileBaseId, tileBaseWidth);
        TileBase tile = tileLevelComponent.config.tileBase;
        ITileHandler tilelevel = TileMapStorageConversions.GetTileHandler(tileLevelComponent.config, tileLevelComponent.obj);

        return tilelevel.SetTile(position, tile);
    }

    public void BorrowTileBaseAtPosition(
        int gridIdAtPosition,
        float tileBaseWidth,
        Vector3Int position
    )
    {
        // recovery id tile level
        TileMapComponent tileLevelComponent = tileMapsFactory.GetTileMapComponent(gridIdAtPosition, tileBaseWidth);
        ITileHandler tilelevel = TileMapStorageConversions.GetTileHandler(tileLevelComponent.config, tileLevelComponent.obj);

        tilelevel.SetTile(position, null);
    }
}
