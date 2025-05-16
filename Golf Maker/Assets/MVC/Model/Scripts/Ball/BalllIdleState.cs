using UnityEngine;

public class BalllIdleState : BallState, IBallLeftClickActor
{

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
    }

    public override void OnExitState(BallContext context)
    {
        context.controller.SetHiteable(false);

        context.controller.SwitchBallLeftClickState(null);
    }

    public void OnLeftClick(BallContext context)
    {
        initialPosition = context.position;
    }

    public void OnLeftUnClick(BallContext context)
    {
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = Vector3Operations.DirectionXY(currentPosition, initialPosition);
        context.rb.AddForce(direction*context.forceMultiplier, ForceMode2D.Impulse);

        context.controller.SwitchBallState(BallMovingState.GetInstance(), context);
    }
}
