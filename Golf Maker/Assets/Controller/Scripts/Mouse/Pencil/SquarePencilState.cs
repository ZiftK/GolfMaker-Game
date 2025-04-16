using UnityEngine;

public class SquarePencilState : PencilState
{

    private static SquarePencilState instance;
    public static PencilState GetInstance(){
        if (instance == null){
            instance = new SquarePencilState();
        }
        return instance;
    }


    private Vector3Int lastPosition, initialPoint, finalPoint;

    public override void OnLeftClick(PencilContext context)
    {
        this.IsDrawing = true;
        initialPoint = context.position;
    }

    public override void OnLeftUnClikc(PencilContext context)
    {
        // draw confirmation
        if (!this.IsDrawing) return;

        finalPoint = context.position;

        // get positions between initial and final point
        Vector3Int[] squareCoords = Vector3IntOperations.InterpolateVectorsAsSquare(initialPoint, finalPoint);
        // draw tile base at positions
        DrawTileBaseAtPositionsArgs args = new DrawTileBaseAtPositionsArgs(context.tileId, squareCoords);
        pencilEventsHandler.OnClearTemporalTiles(); // remove temporal tiles
        pencilEventsHandler.OnDrawTileBaseAtPosition(args);
        
        this.IsDrawing = false;
    }

    public override void OnRightClick(PencilContext context)
    {
        this.IsBorrowing = true;
        initialPoint = context.position;
    }

    public override void OnRightUnClick(PencilContext context)
    {
        // borrow confirmation
        if (!this.IsBorrowing) return;

        finalPoint = context.position;

        // get positions between initial and final point
        Vector3Int[] squareCoords = Vector3IntOperations.InterpolateVectorsAsSquare(initialPoint, finalPoint);
        // borrow tile base at positions
        BorrowTileBaseAtPositionArgs args = new(squareCoords);
        pencilEventsHandler.OnClearTemporalTiles(); // remove temporal tiles
        pencilEventsHandler.OnBorrowTileBaseAtPosition(args);

        this.IsBorrowing = false;
    }

    public override void Update(PencilContext context)
    {
        if (this.IsDrawing && lastPosition != context.position)
        {
            pencilEventsHandler.OnClearTemporalTiles();

            Vector3Int[] squareCoords = Vector3IntOperations.InterpolateVectorsAsSquare(initialPoint, context.position);
            DrawTileBaseAtPositionsArgs args = new(context.tileId, squareCoords);
            pencilEventsHandler.OnTemporalDrawTileBaseAtPosition(args);
        }

        lastPosition = context.position;
    }
}
