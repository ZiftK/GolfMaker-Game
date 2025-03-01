using UnityEngine;
using UnityEngine.InputSystem;

public class Pencil : MonoBehaviour
{
    
    private PencilState currentState;

    void Awake()
    {
        currentState = PointPencilState.GetInstance();
    }

    public void OnClick(InputAction.CallbackContext context){

        if (context.started){
            PencilContext pencilContext = new PencilContext(Vector3Int.RoundToInt(transform.position));
            currentState.OnClick(pencilContext);
        }
        if (context.performed){
            Debug.Log("Manteniendo el click");
        }

        if (context.canceled){
            Debug.Log("Dejando el click");
        }
    }
}
