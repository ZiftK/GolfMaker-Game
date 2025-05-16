using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(
    typeof(Rigidbody2D),
    typeof(CircleCollider2D),
    typeof(SpriteRenderer)
    )]
public class BallController : MonoBehaviour
{
    private Vector3 initialPosition;
    private bool isTracking = false;

    [SerializeField]
    private float forceMultiplier = 1f;
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnLeftClick(InputAction.CallbackContext context){
        if (context.started){
            Debug.Log("Left Click");
            initialPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isTracking = true;
        }

        if (context.canceled){
            Debug.Log("Left UnClick");

            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = Vector3Operations.DirectionXY(currentPosition, initialPosition);

            rb.AddForce(direction*forceMultiplier, ForceMode2D.Impulse);

            isTracking = false;
        }
        
    }

}
