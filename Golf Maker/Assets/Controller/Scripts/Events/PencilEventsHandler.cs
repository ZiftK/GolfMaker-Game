using System;
using UnityEngine;


public class DrawTileBaseAtPositionsArgs: EventArgs{

    public Vector3Int[] positions {get;}
    public int tileBaseId {get;}

    public DrawTileBaseAtPositionsArgs(int tileBaseId, Vector3Int[] positions){
        this.positions = positions;
        this.tileBaseId = tileBaseId;
    }

    public DrawTileBaseAtPositionsArgs(int tileBaseId, Vector3Int position){
        this.positions = new Vector3Int[] {position};
        this.tileBaseId = tileBaseId;
    }
}
public class PencilEventsHandler{

    static PencilEventsHandler instance;

    public event EventHandler<DrawTileBaseAtPositionsArgs> DrawTileBaseAtPosition;


    public static PencilEventsHandler GetInstance(){
        if (instance == null){
            instance = new PencilEventsHandler();
        }

        return instance;
    }

    public void OnDrawTileBaseAtPosition(DrawTileBaseAtPositionsArgs e){
        DrawTileBaseAtPosition?.Invoke(this, e);
    }

}