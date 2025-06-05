using UnityEngine;

public class BallEvents
{
    public delegate void SetBallPosition(Vector3 position);

    public static event SetBallPosition BallReset;

    public static void OnBallReset(Vector3 position)
    {
        BallReset?.Invoke(position);
    }
}
