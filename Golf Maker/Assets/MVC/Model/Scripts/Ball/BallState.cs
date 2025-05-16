using UnityEngine;

public struct BallContext
{
    public BallContext(
        Vector3Int position,
        BallController controller,
        Rigidbody2D rb,
        float forceMultiplier
        )
    {
        this.position = position;
        this.controller = controller;
        this.rb = rb;
        this.forceMultiplier = forceMultiplier;
    }
    public Vector3Int position;
    public BallController controller;
    public Rigidbody2D rb;
    public float forceMultiplier;
}

public abstract class BallState
{

    protected Vector3 initialPosition;

    public abstract void OnEnterState(BallContext context);
    public abstract void OnExitState(BallContext context);

    
}

public interface IBallLeftClickActor
{
    public void OnLeftClick(BallContext context);
    public void OnLeftUnClick(BallContext context);
}

public interface IBallUpdate
{
    public void Update(BallContext context);
}
