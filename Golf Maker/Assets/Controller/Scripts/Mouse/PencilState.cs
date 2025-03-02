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

    public abstract void Update(PencilContext context);

    public abstract void OnLeftClick();

    public abstract void OnLeftUnClikc();

    public abstract void OnRightClick();

    public abstract void OnRightUnClick();
}
