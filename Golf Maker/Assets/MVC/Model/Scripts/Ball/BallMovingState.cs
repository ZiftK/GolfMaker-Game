using UnityEngine;

public class BallMovingState : BallState, IBallUpdate
{
    
    public static BallMovingState instance;
    public static BallMovingState GetInstance()
    {
        if (instance == null)
        {
            instance = new BallMovingState();
        }
        return instance;
    }

    public override void OnEnterState(BallContext context)
    {
        
        context.controller.SwitchBallUpdateState(this);
    }

    public override void OnExitState(BallContext context)
    {
        context.controller.SwitchBallUpdateState(null);
        context.controller.SwitchBallLeftClickState(BalllIdleState.GetInstance());
    }

    public void Update(BallContext context)
    {
        if (context.rb.linearVelocity.magnitude < 0.1f)
        {
            context.controller.SwitchBallState(BalllIdleState.GetInstance(), context);
        }
    }
}
