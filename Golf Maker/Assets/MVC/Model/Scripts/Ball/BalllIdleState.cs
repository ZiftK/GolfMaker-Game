
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

        Vector3 ballPos = controller.transform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ballPos.z = 0;
        mousePos.z = 0;

        // Dirección opuesta al mouse
        Vector3 direction = (ballPos - mousePos).normalized;
        forceDirection = direction;

        // Distancia entre la bola y el mouse
        float mouseDistance = Vector3.Distance(ballPos, mousePos);
        float maxDistance = controller.GetLineDistance();
        float desiredDistance = Mathf.Min(mouseDistance, maxDistance);

        // Lanzamos el raycast en dirección opuesta al mouse
        RaycastHit2D hit = Physics2D.Raycast(ballPos, direction, desiredDistance, controller.layerMask);

        float finalDistance = desiredDistance;

        if (hit.collider != null)
        {
            // Si colisiona, usamos la distancia hasta el impacto
            finalDistance = hit.distance;
        }

        hitSlider = finalDistance / maxDistance;
        controller.DrawLine(ballPos, ballPos + direction * finalDistance);
        

    }
    
    
}
