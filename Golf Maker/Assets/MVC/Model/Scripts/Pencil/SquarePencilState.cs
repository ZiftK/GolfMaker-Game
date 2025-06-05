using UnityEngine;

public class SquarePencilState : PencilState, IUpdatePencilState
{

    private static SquarePencilState instance;
    public static SquarePencilState GetInstance(){
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
        this.IsDrawing = false;

        if (context.id == 8)
        {
            Debug.LogWarning("You cannot use the square pencil with the initial tile.");
            return;
        }

        finalPoint = context.position;

        // get positions between initial and final point
        Vector3Int[] squareCoords = Vector3IntOperations.InterpolateVectorsAsSquare(initialPoint, finalPoint);
        
        
        switch (context.setType)
        {
            case PencilSetType.Tile:
                // draw tile base at positions
                DrawTileBaseAtPositionsArgs args = new DrawTileBaseAtPositionsArgs(context.id, squareCoords);
                pencilEventsHandler.OnClearTemporalTiles(); // remove temporal tiles
                pencilEventsHandler.OnDrawTileBaseAtPosition(args);
                break;

            case PencilSetType.PlaceObject:
                //todo: implement
                Debug.LogWarning("Bucket pencil can't use objects");
                break;
        }
       
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
        this.IsBorrowing = false;

        finalPoint = context.position;

        // get positions between initial and final point
        Vector3Int[] squareCoords = Vector3IntOperations.InterpolateVectorsAsSquare(initialPoint, finalPoint);
        

        switch (context.setType)
        {
            case PencilSetType.Tile:
                // borrow tile base at positions
                BorrowTileBaseAtPositionArgs args = new(squareCoords);
                pencilEventsHandler.OnClearTemporalTiles(); // remove temporal tiles
                pencilEventsHandler.OnBorrowTileBaseAtPosition(args);
                break;

            case PencilSetType.PlaceObject:
                //todo: implement
                Debug.LogWarning("Square pencil can't use objects");
                break;
        }
    }

    public void Update(PencilContext context)
    {
        if (this.IsDrawing && lastPosition != context.position)
        {
            pencilEventsHandler.OnClearTemporalTiles();

            switch (context.setType)
        {
            case PencilSetType.Tile:
                Vector3Int[] squareCoords = Vector3IntOperations.InterpolateVectorsAsSquare(initialPoint, context.position);
            
                DrawTileBaseAtPositionsArgs args = new(context.id, squareCoords);
                pencilEventsHandler.OnTemporalDrawTileBaseAtPosition(args);
                break;

            case PencilSetType.PlaceObject:
                //todo: implement
                Debug.LogWarning("Square pencil can't use objects");
                break;
        }
        }

        lastPosition = context.position;
    }
}
