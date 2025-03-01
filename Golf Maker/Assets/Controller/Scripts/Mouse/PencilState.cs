using JetBrains.Annotations;
using UnityEngine;

public struct PencilContext{

    public PencilContext(Vector3Int position){
        this.position = position;
    }
    public Vector3Int position;
}

public abstract class PencilState
{

    protected PencilEventsHandler pencilEventsHandler;

    public PencilState(){
        pencilEventsHandler = PencilEventsHandler.GetInstance();
    }

    protected static PencilState instance;

    public abstract void OnClick(PencilContext context);

    public abstract void OnUnClick(PencilContext context);

    
}
