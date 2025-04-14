using UnityEngine;

public class SquarePencilState : PencilState
{


    public static PencilState GetInstance(){
        if (instance == null){
            instance = new SquarePencilState();
        }
        return instance;
    }


    private Vector3Int lastPosition, initialPoint, finalPoint;

    public override void OnLeftClick(PencilContext context)
    {
        setIsDrawing(true);
        initialPoint = context.position;
    }

    public override void OnLeftUnClikc(PencilContext context)
    {
        finalPoint = context.position;

        // get positions between initial and final point
        Vector3Int[] squareCoords = Vector3IntOperations.InterpolateVectorsAsSquare(initialPoint, finalPoint);
        // draw tile base at positions
        DrawTileBaseAtPositionsArgs args = new DrawTileBaseAtPositionsArgs(context.tileId, squareCoords);
        pencilEventsHandler.OnClearTemporalTiles(); // remove temporal tiles
        pencilEventsHandler.OnDrawTileBaseAtPosition(args);

        setIsDrawing(false);
    }

    public override void OnRightClick(PencilContext context)
    {
        setIsBorrowing(true);
        initialPoint = context.position;
    }

    public override void OnRightUnClick(PencilContext context)
    {
        finalPoint = context.position;

        // get positions between initial and final point
        Vector3Int[] squareCoords = Vector3IntOperations.InterpolateVectorsAsSquare(initialPoint, finalPoint);
        // borrow tile base at positions
        BorrowTileBaseAtPositionArgs args = new BorrowTileBaseAtPositionArgs(squareCoords);
        pencilEventsHandler.OnClearTemporalTiles(); // remove temporal tiles
        pencilEventsHandler.OnBorrowTileBaseAtPosition(args);
        
        setIsBorrowing(false);
        
    }

    public override void Update(PencilContext context)
    {
        
    }
}
