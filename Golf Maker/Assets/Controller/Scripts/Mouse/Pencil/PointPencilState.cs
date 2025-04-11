using UnityEngine;

public class PointPencilState : PencilState
{

    
    public static PencilState GetInstance()
    {
        if (instance == null){
            instance = new PointPencilState();
        }
        return instance;
    }

    private bool drawing;
    private bool borrowing;

    public override void OnLeftClick(PencilContext context)
    {
        drawing = true;
        borrowing = false;
    }

    public override void OnLeftUnClikc(PencilContext context)
    {
        drawing = false;
    }

    public override void Update(PencilContext context)
    {
        if (drawing){
            DrawTileBaseAtPositionsArgs args = new DrawTileBaseAtPositionsArgs(context.tileId, context.position);
            pencilEventsHandler.OnDrawTileBaseAtPosition(args);
        }
        if (borrowing){
            
            BorrowTileBaseAtPositionArgs args = new BorrowTileBaseAtPositionArgs(context.position);
            pencilEventsHandler.OnBorrowTileBaseAtPosition(args);
        }
    }

    public override void OnRightClick(PencilContext context)
    {
        
        borrowing = true;
        drawing = false;
    }

    public override void OnRightUnClick(PencilContext context)
    {
        borrowing = false;
    }

}
