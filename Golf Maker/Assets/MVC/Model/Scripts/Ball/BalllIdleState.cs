
using System;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;

public class BalllIdleState : BallState, IBallLeftClickActor, IBallUpdate
{

    private Vector3 lastFinalPosition = Vector3.zero;
    private bool isClicked = false;

    private Vector3 forceToApply;

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

        context.controller.SetHiteable(true);

        context.controller.SwitchBallLeftClickState(this);
        context.controller.SwitchBallUpdateState(this);
    }

    public override void OnExitState(BallContext context)
    {
        context.controller.SetHiteable(false);
        context.controller.ClearLine();

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
        context.rb.AddForce(forceToApply, ForceMode2D.Impulse);

        isClicked = false;

        context.controller.SwitchBallState(BallMovingState.GetInstance(), context);
    }

    public void Update(BallContext context)
    {
        if (isClicked && !lastFinalPosition.Equals(context.position))
        {
            CastLine(context.controller, out Vector3 forceDirection, out float hitSlider);
            forceToApply = forceDirection * context.forceMultiplier * hitSlider;
            lastFinalPosition = context.position;
        }
    }

    public void CastLine(BallController controller, out Vector3 forceDirection, out float hitSlider)
    {
        forceDirection = Vector3.zero;
        hitSlider = 0;

        Vector3 start = controller.gameObject.transform.position;
        Vector3 end = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        start.z = 0;
        end.z = 0;

        

        RaycastHit2D hit = Physics2D.Raycast(start, start - end, 1000, controller.layerMask);

        if (hit.collider != null)
        {
            float mouseDistance = Vector3.Distance(start, end);


            Vector3 hitPoint = hit.point;
            Vector3 direction = Vector3.Normalize(hitPoint);
            forceDirection = direction;

            float hitDistance = Math.Min(mouseDistance, controller.GetLineDistance());
            float lineDistance = Math.Min(hitDistance, Vector3.Distance(start, hitPoint));

            controller.DrawLine(start, start + direction * lineDistance);
            
            hitSlider = hitDistance/controller.GetLineDistance();
        }

    }
    
    
}
