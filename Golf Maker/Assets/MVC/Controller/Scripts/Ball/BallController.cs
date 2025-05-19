using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(
    typeof(Rigidbody2D),
    typeof(CircleCollider2D),
    typeof(SpriteRenderer)
    )]
[RequireComponent(
    typeof(LineRenderer)
    )]
public class BallController : MonoBehaviour
{
    public LayerMask layerMask;
    private bool isHiteable;


    Rigidbody2D rb;
    LineRenderer lineRenderer;

    [SerializeField]
    float lineDistance = 5f;
    [SerializeField]
    private float forceMultiplier = 1f;


    // Ball states
    private BallState currentState;
    private IBallLeftClickActor currentLeftClickState;
    private IBallUpdate currentUpdateState;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();


        currentState = BalllIdleState.GetInstance();
        currentLeftClickState = BalllIdleState.GetInstance();
        currentUpdateState = BalllIdleState.GetInstance();
        isHiteable = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (currentUpdateState != null)
        {

            currentUpdateState.Update(
                new BallContext(
                    Camera.main.ScreenToWorldPoint(Input.mousePosition),
                    this,
                    rb,
                    forceMultiplier
                    )
                );
        }


    }


    public void DrawLine(Vector3 start, Vector3 end)
    {

        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }

    public void ClearLine()
    {
        lineRenderer.SetPosition(0, Vector3.zero);
        lineRenderer.SetPosition(1, Vector3.zero);
    }

    public void SwitchBallState(BallState newState, BallContext context)
    {
        currentState.OnExitState(context);
        currentState = newState;
        currentState.OnEnterState(context);
    }

    public void SwitchBallLeftClickState(IBallLeftClickActor newState)
    {
        currentLeftClickState = newState;
    }

    public void SwitchBallUpdateState(IBallUpdate newState)
    {
        currentUpdateState = newState;
    }

    public void OnLeftClick(InputAction.CallbackContext context)
    {


        if (currentLeftClickState == null)
        {
            return;
        }

        if (!isHiteable) return;

        if (context.started)
        {

            currentLeftClickState.OnLeftClick(
                new BallContext(
                    Camera.main.ScreenToWorldPoint(Input.mousePosition),
                    this,
                    rb,
                    forceMultiplier
                    )
                );
        }

        if (context.canceled)
        {

            currentLeftClickState.OnLeftUnClick(
                new BallContext(
                    Camera.main.ScreenToWorldPoint(Input.mousePosition),
                    this,
                    rb,
                    forceMultiplier
                    )
                );
        }

    }

    public bool IsHiteable() => isHiteable;
    public void SetHiteable(bool value) => isHiteable = value;

    public float GetLineDistance() => lineDistance;
    
    public float GetForceMultiplier() => forceMultiplier;

}
