using UnityEngine;
using UnityEngine.InputSystem;

public class Pencil : MonoBehaviour
{
    
    private PencilState currentState;

    public float temporalTileOpacity = 0.5f;

    void Awake()
    {
        // currentState = PointPencilState.GetInstance();
        currentState = SquarePencilState.GetInstance();
    }

    void Update()
    {

        currentState.Update(
            new PencilContext(
                Vector3Int.RoundToInt(transform.position),
                0
                )
                );
    }
    public void OnLeftClick(InputAction.CallbackContext context){

        if (context.started){
            currentState.OnLeftClick(
                new PencilContext(
                Vector3Int.RoundToInt(transform.position),
                0
                )
                );
        }

        if (context.canceled){
            currentState.OnLeftUnClikc(
                new PencilContext(
                Vector3Int.RoundToInt(transform.position),
                0
                )
            );
        }
    }

    public void OnRightClick(InputAction.CallbackContext context){
        if (context.started){
            currentState.OnRightClick(
                new PencilContext(
                Vector3Int.RoundToInt(transform.position),
                0
                )
            );
        }

        if (context.canceled){
            currentState.OnRightUnClick(
                new PencilContext(
                Vector3Int.RoundToInt(transform.position),
                0
                )
            );
        }
    }
}
