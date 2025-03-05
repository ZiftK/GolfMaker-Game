using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Rendering;

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

    public override void OnLeftClick(PencilContext context)
    {
        initialPoint = context.position;
    }

    public override void OnLeftUnClikc(PencilContext context)
    {
        finalPoint = context.position;
        Vector3Int[] lineCoords = Vector3IntOperations.InterpolateVectors(initialPoint, finalPoint);
        DrawTileBaseAtPositionsArgs args = new DrawTileBaseAtPositionsArgs(context.tileId, lineCoords);
        pencilEventsHandler.OnDrawTileBaseAtPosition(args);

        
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
        
    }
}
