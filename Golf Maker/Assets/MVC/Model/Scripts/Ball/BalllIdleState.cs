using UnityEngine;

public class BalllIdleState : BallState, IBallLeftClickActor, IBallUpdate
{

    private Vector3 lastFinalPosition = Vector3.zero;
    private bool isClicked = false;

    public static BalllIdleState instance;
    public static BalllIdleState GetInstance()
    {
        if (instance == null)
        {
            instance = new BalllIdleState();
        }
        return instance;
    }

    public override void OnEnterState(BallContext context)
    {
        Debug.Log("Ball Idle State");
        context.controller.SetHiteable(true);
        
        context.controller.SwitchBallLeftClickState(this);
        context.controller.SwitchBallUpdateState(this);
    }

    public override void OnExitState(BallContext context)
    {
        context.controller.SetHiteable(false);

        context.controller.SwitchBallLeftClickState(null);
        context.controller.SwitchBallUpdateState(null);
    }

    public void OnLeftClick(BallContext context)
    {
        initialPosition = context.position;
        lastFinalPosition = context.position;
        isClicked = true;
    }

    public void OnLeftUnClick(BallContext context)
    {
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = Vector3Operations.DirectionXY(currentPosition, initialPosition);
        context.rb.AddForce(direction*context.forceMultiplier, ForceMode2D.Impulse);

        isClicked = false;

        context.controller.SwitchBallState(BallMovingState.GetInstance(), context);
    }

    public void Update(BallContext context)
    {
        if (isClicked)
        {
            context.controller.DrawLine(initialPosition, context.position);
        }
    }
}
