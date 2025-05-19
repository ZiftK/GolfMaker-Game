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

    [SerializeField]
    private float forceMultiplier = 1f;
    Rigidbody2D rb;
    LineRenderer lineRenderer;


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
        Debug.Log("DrawLine: " + start + " " + end);
        start.z = 0;
        end.z = 0;

        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }
    
    public Vector3 GetOppositeDirection(Vector3 direction)
    {
        Vector3 start = transform.position;
        Vector3 end = start - direction;
        end.z = 0;
        direction = Vector3.Normalize(direction);

        return direction;
    }

    public void CastLine(Vector3 direction, float distance)
    {
        Vector3 start = transform.position;

        RaycastHit2D hit = Physics2D.Raycast(start,
            direction,
            distance,
            layerMask
            );

        Debug.Log("Raycast: " + hit.collider);
        if (hit.collider != null)
        {
            Vector3 end = hit.point;
            end.z = 0;
            Debug.DrawLine(start, end, Color.red, 1000f);
            Debug.Log("Hit point: " + hit.point);

            DrawLine(start, end);
            return;
        }

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

    public void OnLeftClick(InputAction.CallbackContext context){

        
        if (currentLeftClickState == null)
        {
            return;
        }

        if (!isHiteable) return;
        
        if (context.started){

            currentLeftClickState.OnLeftClick(
                new BallContext(
                    Camera.main.ScreenToWorldPoint(Input.mousePosition),
                    this,
                    rb,
                    forceMultiplier
                    )
                );
        }

        if (context.canceled){
            
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

}
