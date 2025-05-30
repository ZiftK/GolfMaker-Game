using UnityEngine;

public class PointPencilState : PencilState, IUpdatePencilState
{

    private static PointPencilState instance;
    public static PointPencilState GetInstance()
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

    public void Update(PencilContext context)
    {
        switch (context.setType)
        {
            case PencilSetType.Tile:
                if (this.IsDrawing)
                {
                    DrawTileBaseAtPositionsArgs args = new DrawTileBaseAtPositionsArgs(context.id, context.position);
                    pencilEventsHandler.OnDrawTileBaseAtPosition(args);
                }
                if (this.IsBorrowing)
                {

                    BorrowTileBaseAtPositionArgs args = new BorrowTileBaseAtPositionArgs(context.position);
                    pencilEventsHandler.OnBorrowTileBaseAtPosition(args);
                }
                break;

            case PencilSetType.PlaceObject:
                if (this.IsDrawing)
                {
                    PlaceObjectAtPositionsArgs args = new PlaceObjectAtPositionsArgs(context.id, context.position);
                    pencilEventsHandler.OnPlaceObjectAtPosition(args);
                }
                break;
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
