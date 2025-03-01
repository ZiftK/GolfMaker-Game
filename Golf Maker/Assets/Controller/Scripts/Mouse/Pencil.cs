using UnityEngine;
using UnityEngine.InputSystem;

public class Pencil : MonoBehaviour
{
    
    private PencilState currentState;

    void Awake()
    {
        currentState = PointPencilState.GetInstance();
    }

    void Update()
    {

        currentState.Update(new PencilContext(Vector3Int.RoundToInt(transform.position)));
    }
    public void OnLeftClick(InputAction.CallbackContext context){

        if (context.started){
            currentState.OnLeftClick();
        }

        if (context.canceled){
            currentState.OnLeftUnClikc();
        }
    }

    public void OnRightClick(InputAction.CallbackContext context){
        if (context.started){
            currentState.OnRightClick();
        }

        if (context.canceled){
            currentState.OnRightUnClick();
        }
    }
}
