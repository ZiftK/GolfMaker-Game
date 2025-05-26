using UnityEngine;

[DefaultExecutionOrder(-201)]
public class BallEventsHandler : MonoBehaviour
{
    void Awake()
    {
        GameLevelEvents.SetBallInitialPositionEvent += SetPosition;
    }

    void SetPosition(Vector3 position)
    {
        position.x += 0.5f; // Adjusting position to center the ball on the tile
        position.y += 0.5f; // Adjusting position to center the ball on the tile
        gameObject.transform.position = position;
    }
}
