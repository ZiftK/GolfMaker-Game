using System.Data.Common;
using JetBrains.Annotations;
using UnityEngine;

public struct PencilContext{

    
    public PencilContext(Vector3Int position, int tileId){
        this.position = position;
        this.tileId = tileId;
    }
    public Vector3Int position;
    public int tileId;
}

public abstract class PencilState
{

    protected PencilEventsHandler pencilEventsHandler;

    private bool drawing, borrowing;

    public PencilState(){
        pencilEventsHandler = PencilEventsHandler.GetInstance();
    }

    protected void setIsDrawing(bool value){
        
        if (value && this.borrowing){
            return;
        }
        this.drawing = value;
    }

    protected void setIsBorrowing(bool value){
        
        if (value && this.drawing){
            return;
        }
        this.borrowing = value;
    }

    protected static PencilState instance;

    public abstract void Update(PencilContext context);

    public abstract void OnLeftClick(PencilContext context);

    public abstract void OnLeftUnClikc(PencilContext context);

    public abstract void OnRightClick(PencilContext context);

    public abstract void OnRightUnClick(PencilContext context);
}
