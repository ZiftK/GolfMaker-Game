using System;
using UnityEngine;

public class DrawTileBaseAtPositionsArgs : EventArgs {

    public Vector3Int[] positions { get; }
    public int tileBaseId { get; }

    public DrawTileBaseAtPositionsArgs(int tileBaseId, Vector3Int[] positions) {
        this.positions = positions;
        this.tileBaseId = tileBaseId;
    }

    public DrawTileBaseAtPositionsArgs(int tileBaseId, Vector3Int position) {
        this.positions = new Vector3Int[] { position };
        this.tileBaseId = tileBaseId;
    }

}

public class PlaceObjectAtPositionsArgs : EventArgs {

    public Vector3Int[] positions;
    public string placeObjectName;

    public PlaceObjectAtPositionsArgs(string placeObjectName, Vector3Int[] positions)
    {
        this.placeObjectName = placeObjectName;
        this.positions = positions;
    }
}


public class BorrowTileBaseAtPositionArgs : EventArgs {
    public Vector3Int[] positions { get; }

    public BorrowTileBaseAtPositionArgs(Vector3Int[] positions) {
        this.positions = positions;
    }

    public BorrowTileBaseAtPositionArgs(Vector3Int position) {
        this.positions = new Vector3Int[] { position };
    }
}


public class PencilEventsHandler{

    static PencilEventsHandler instance;

    public event EventHandler<DrawTileBaseAtPositionsArgs> DrawTileBaseAtPosition;

    public event EventHandler<PlaceObjectAtPositionsArgs> PlaceObjectAtPosition;

    public event EventHandler<DrawTileBaseAtPositionsArgs> TemporalDrawTileBaseAtPositions;

    public event EventHandler ClearTemporalTiles;

    public event EventHandler<BorrowTileBaseAtPositionArgs> BorrowTileBaseAtPosition;


    public static PencilEventsHandler GetInstance(){
        if (instance == null){
            instance = new PencilEventsHandler();
        }

        return instance;
    }

    public void OnDrawTileBaseAtPosition(DrawTileBaseAtPositionsArgs e){
        DrawTileBaseAtPosition?.Invoke(this, e);
    }

    public void OnPlaceObjectAtPosition(PlaceObjectAtPositionsArgs e)
    {
        PlaceObjectAtPosition?.Invoke(this, e);
    }

    public void OnBorrowTileBaseAtPosition(BorrowTileBaseAtPositionArgs e)
    {

        BorrowTileBaseAtPosition?.Invoke(this, e);
    }

    public void OnTemporalDrawTileBaseAtPosition(DrawTileBaseAtPositionsArgs e){
        TemporalDrawTileBaseAtPositions?.Invoke(this, e);
    }

    public void OnClearTemporalTiles(){
        ClearTemporalTiles?.Invoke(this, new EventArgs());
    }

}