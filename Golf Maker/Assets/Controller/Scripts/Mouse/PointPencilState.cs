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

    public override void OnClick(PencilContext context)
    {
        DrawTileBaseAtPositionsArgs args = new DrawTileBaseAtPositionsArgs(0, context.position);
        pencilEventsHandler.OnDrawTileBaseAtPosition(args);
    }

    public override void OnUnClick(PencilContext context)
    {
        
    }
}
