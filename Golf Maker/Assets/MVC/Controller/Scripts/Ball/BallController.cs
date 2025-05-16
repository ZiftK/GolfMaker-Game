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
    void Update()
    {
        if (currentUpdateState != null)
        {
            Debug.Log("Update");
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
        start.z = 0;
        end.z = 0;

        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
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
                    Vector3Int.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition)),
                    this,
                    rb,
                    forceMultiplier
                    )
                );
        }

        if (context.canceled){
            
            currentLeftClickState.OnLeftUnClick(
                new BallContext(
                    Vector3Int.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition)),
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
