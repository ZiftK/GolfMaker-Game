
using UnityEngine;
public class LinePencilState : PencilState, IUpdatePencilState
{
    Vector3Int initialPoint, finalPoint;
    
    private static LinePencilState instance;

    public static LinePencilState GetInstance()
    {
        if (instance == null){
            instance = new LinePencilState();
        }
        return instance;
    }
    
    Vector3Int lastPosition;

    public override void OnLeftClick(PencilContext context)
    {
        this.IsDrawing = true;
        if (!this.IsDrawing) return;

        initialPoint = context.position;
    }

    public override void OnLeftUnClikc(PencilContext context)
    {
        // check if the pencil is drawing
        if (!this.IsDrawing) return;

        this.IsDrawing = false;

        if (context.id == 8)
        {
            Debug.LogWarning("You cannot use the line pencil with the initial tile.");
            return;
        }
        
        finalPoint = context.position;
        Vector3Int[] lineCoords = Vector3IntOperations.InterpolateVectors(initialPoint, finalPoint);
        DrawTileBaseAtPositionsArgs args = new DrawTileBaseAtPositionsArgs(context.id, lineCoords);
        pencilEventsHandler.OnClearTemporalTiles();
        pencilEventsHandler.OnDrawTileBaseAtPosition(args);

    }

    public override void OnRightClick(PencilContext context)
    {
        this.IsBorrowing = true;
        if (!this.IsBorrowing) return;

        initialPoint = context.position;
    }

    public override void OnRightUnClick(PencilContext context)
    {
        // check if the pencil is borrowing
        if (!this.IsBorrowing) return;

        finalPoint = context.position;
        Vector3Int[] lineCoords = Vector3IntOperations.InterpolateVectors(initialPoint, finalPoint);
        BorrowTileBaseAtPositionArgs args = new BorrowTileBaseAtPositionArgs(lineCoords);
        pencilEventsHandler.OnBorrowTileBaseAtPosition(args);

        this.IsBorrowing = false;
    }

    public void Update(PencilContext context)
    {


        if (this.IsDrawing && lastPosition != context.position){
            pencilEventsHandler.OnClearTemporalTiles();

            Vector3Int[] lineCoords = Vector3IntOperations.InterpolateVectors(initialPoint, context.position);
            DrawTileBaseAtPositionsArgs args = new DrawTileBaseAtPositionsArgs(context.id, lineCoords);
            pencilEventsHandler.OnTemporalDrawTileBaseAtPosition(args);
            
        }

        lastPosition = context.position;
    }
}
