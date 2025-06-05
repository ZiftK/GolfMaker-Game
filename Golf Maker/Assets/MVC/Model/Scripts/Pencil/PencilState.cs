
using UnityEngine;
public enum PencilSetType
{
    Tile,
    PlaceObject
}

public struct PencilContext
{


    public PencilContext(Vector3Int position, int id, PencilSetType setType)
    {
        this.position = position;
        this.id = id;
        this.setType = setType;
    }
    public Vector3Int position;
    public int id;

    public PencilSetType setType;

}

public interface IUpdatePencilState
{
    public void Update(PencilContext context);
}

public abstract class PencilState
{

    protected PencilEventsHandler pencilEventsHandler;

    private bool drawing, borrowing;

    public PencilState()
    {
        pencilEventsHandler = PencilEventsHandler.GetInstance();
    }

    public bool IsDrawing
    {
        get { return drawing; }
        protected set
        {
            if (value && borrowing) return;
            drawing = value;
        }
    }

    public bool IsBorrowing
    {
        get { return borrowing; }
        protected set
        {
            if (value && drawing) return;
            borrowing = value;
        }
    }

    public abstract void OnLeftClick(PencilContext context);

    public abstract void OnLeftUnClikc(PencilContext context);

    public abstract void OnRightClick(PencilContext context);

    public abstract void OnRightUnClick(PencilContext context);
}
