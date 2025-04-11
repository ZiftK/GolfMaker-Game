
using UnityEngine;
public class LinePencilState : PencilState
{
    Vector3Int initialPoint, finalPoint;


    public static PencilState GetInstance()
    {
        if (instance == null){
            instance = new LinePencilState();
        }
        return instance;
    }
    
    bool drawing = false;
    Vector3Int lastPosition;

    public override void OnLeftClick(PencilContext context)
    {
        initialPoint = context.position;
        drawing = true;
    }

    public override void OnLeftUnClikc(PencilContext context)
    {
        finalPoint = context.position;
        Vector3Int[] lineCoords = Vector3IntOperations.InterpolateVectors(initialPoint, finalPoint);
        DrawTileBaseAtPositionsArgs args = new DrawTileBaseAtPositionsArgs(context.tileId, lineCoords);
        pencilEventsHandler.OnClearTemporalTiles();
        pencilEventsHandler.OnDrawTileBaseAtPosition(args);

        drawing = false;
    }

    public override void OnRightClick(PencilContext context)
    {
        initialPoint = context.position;
    }

    public override void OnRightUnClick(PencilContext context)
    {
        finalPoint = context.position;
        Vector3Int[] lineCoords = Vector3IntOperations.InterpolateVectors(initialPoint, finalPoint);
        BorrowTileBaseAtPositionArgs args = new BorrowTileBaseAtPositionArgs(lineCoords);
        pencilEventsHandler.OnBorrowTileBaseAtPosition(args);
    }

    public override void Update(PencilContext context)
    {


        if (drawing && lastPosition != context.position){
            pencilEventsHandler.OnClearTemporalTiles();

            Vector3Int[] lineCoords = Vector3IntOperations.InterpolateVectors(initialPoint, context.position);
            DrawTileBaseAtPositionsArgs args = new DrawTileBaseAtPositionsArgs(context.tileId, lineCoords);
            pencilEventsHandler.OnTemporalDrawTileBaseAtPosition(args);
            
        }

        lastPosition = context.position;
    }
}
