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

    #region Line Methods
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
    #endregion Line Methods

    #region Behavior Methods
    public void KillBall()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
    #endregion Behavior Methods

    #region State Methods
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
    #endregion State Methods

    #region Event Methods
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

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Ball collided with a {other.name}");
        if (other.CompareTag("water"))
        {
            KillBall();
        }     
    }

    #endregion Event Methods

    #region Getters and Setters
    public bool IsHiteable() => isHiteable;
    public void SetHiteable(bool value) => isHiteable = value;

    public float GetLineDistance() => lineDistance;

    public float GetForceMultiplier() => forceMultiplier;
    
    #endregion Getters and Setters

}
