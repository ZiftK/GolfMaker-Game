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

    public override void OnLeftClick()
    {
        drawing = true;
        borrowing = false;
    }

    public override void OnLeftUnClikc()
    {
        drawing = false;
    }

    public override void Update(PencilContext context)
    {
        if (drawing){
            DrawTileBaseAtPositionsArgs args = new DrawTileBaseAtPositionsArgs(0, context.position);
            pencilEventsHandler.OnDrawTileBaseAtPosition(args);
        }
        if (borrowing){
            
            BorrowTileBaseAtPositionArgs args = new BorrowTileBaseAtPositionArgs(context.position);
            pencilEventsHandler.OnBorrowTileBaseAtPosition(args);
        }
    }

    public override void OnRightClick()
    {
        
        borrowing = true;
        drawing = false;
    }

    public override void OnRightUnClick()
    {
        borrowing = false;
    }
}
