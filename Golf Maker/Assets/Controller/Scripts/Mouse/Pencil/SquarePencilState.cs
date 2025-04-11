using UnityEngine;

public class SquarePencilState : PencilState
{


    public static PencilState GetInstance(){
        if (instance == null){
            instance = new SquarePencilState();
        }
        Debug.Log("Construyendo instancia");
        return instance;
    }
    
    private bool drawing;
    private Vector3Int lastPosition, initialPoint, finalPoint;

    public override void OnLeftClick(PencilContext context)
    {
        drawing = true;
        initialPoint = context.position;
        Debug.Log("Click");
    }

    public override void OnLeftUnClikc(PencilContext context)
    {
        finalPoint = context.position;
        Vector3Int[] squareCoords = Vector3IntOperations.InterpolateVectorsAsSquare(initialPoint, finalPoint);
        DrawTileBaseAtPositionsArgs args = new DrawTileBaseAtPositionsArgs(context.tileId, squareCoords);
        pencilEventsHandler.OnClearTemporalTiles();
        pencilEventsHandler.OnDrawTileBaseAtPosition(args);

        drawing = false;

        Debug.Log("Unclick");
    }

    public override void OnRightClick(PencilContext context)
    {
        throw new System.NotImplementedException();
    }

    public override void OnRightUnClick(PencilContext context)
    {
        throw new System.NotImplementedException();
    }

    public override void Update(PencilContext context)
    {
        
    }
}
