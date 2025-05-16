using UnityEngine;
using UnityEngine.InputSystem;

public class Pencil : MonoBehaviour
{
    
    private PencilState currentState;

    public float temporalTileOpacity = 0.5f;

    void Awake()
    {
        // currentState = PointPencilState.GetInstance();
        currentState = PointPencilState.GetInstance();
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

    public void OnSwitchPencil(InputAction.CallbackContext context){
        if (context.performed){
            int bindingIndex = context.action.GetBindingIndexForControl(context.control);

            // Handle the binding index to switch pencil states
            switch (bindingIndex){
                case 0:
                    currentState = PointPencilState.GetInstance();
                    break;
                case 1:
                    currentState = LinePencilState.GetInstance();
                    break;
                case 2:
                    currentState = SquarePencilState.GetInstance();
                    break;
                case 3:
                    currentState = BucketPencilState.GetInstance();
                    break;
                default:
                    Debug.LogWarning("Unhandled binding index for SwitchPencil action");
                    break;
            }
        }
    }
}
