using System;
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

        // Subscribe to the initial position event
        GameLevelEvents.SetBallInitialPositionEvent += SetInitialPosition;
        Debug.Log("BallController: Subscribed to SetBallInitialPositionEvent");
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event when destroyed
        GameLevelEvents.SetBallInitialPositionEvent -= SetInitialPosition;
    }

    private void SetInitialPosition(Vector3 position)
    {
        Debug.Log($"BallController: Setting initial position to {position}");
        
        // Set the ball's position
        transform.position = new Vector3(position.x + 0.5f, position.y + 0.5f, transform.position.z);
        
        // Update the GameLevelHandler's initial position
        if (GameLevelHandler.Instance != null)
        {
            GameLevelHandler.Instance.initialBallPosition = position;
            Debug.Log($"BallController: Updated GameLevelHandler initial position to {position}");
        }
        else
        {
            Debug.LogWarning("BallController: GameLevelHandler.Instance is null");
        }
    }

    void Start()
    {
        // If there is a pending initial position, set it
        if (GameLevelEvents.PendingBallInitialPosition.HasValue)
        {
            Debug.Log("BallController: Applying pending initial position");
            SetInitialPosition(GameLevelEvents.PendingBallInitialPosition.Value);
        }
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

    public void ResetBall()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        gameObject.GetComponent<Rigidbody2D>().simulated = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;

        rb.linearVelocity = Vector2.zero;

        transform.position = GameLevelHandler.Instance.initialBallPosition;
        // EffectsEvents.ThrowEffect("ResetBall", transform.position);

        GameLevelEvents.TriggerResetBall();
    }

    public void KillAndResetBall()
    {
        KillBall();
        ResetBall();
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
        if (!other.isTrigger) return;

        if (other.CompareTag("water"))
        {
            EffectsEvents.ThrowEffect("WaterSplash", transform.position);
            KillAndResetBall();
        }

        if (other.CompareTag("fall"))
        {
            Debug.Log("Win!!!");
        }

        if (other.CompareTag("coin"))
        {
            EffectsEvents.ThrowEffect("TakeCoin", other.transform.position);
            GameLevelEvents.TriggerTakeCoin();
            other.gameObject.SetActive(false);
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
