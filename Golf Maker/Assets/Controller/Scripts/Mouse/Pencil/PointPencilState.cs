using UnityEngine;

public class PointPencilState : PencilState
{

    private static PointPencilState instance;
    public static PencilState GetInstance()
    {
        if (instance == null){
            instance = new PointPencilState();
        }
        return instance;
    }


    public override void OnLeftClick(PencilContext context)
    {
        this.IsDrawing = true;
    }

    public override void OnLeftUnClikc(PencilContext context)
    {
        this.IsDrawing = false;
    }

    public override void Update(PencilContext context)
    {
        if (this.IsDrawing){
            DrawTileBaseAtPositionsArgs args = new DrawTileBaseAtPositionsArgs(context.tileId, context.position);
            pencilEventsHandler.OnDrawTileBaseAtPosition(args);
        }
        if (this.IsBorrowing){
            
            BorrowTileBaseAtPositionArgs args = new BorrowTileBaseAtPositionArgs(context.position);
            pencilEventsHandler.OnBorrowTileBaseAtPosition(args);
        }
    }

    public override void OnRightClick(PencilContext context)
    {
        
        this.IsBorrowing = true;
    }

    public override void OnRightUnClick(PencilContext context)
    {
        this.IsBorrowing = false;
    }

}
