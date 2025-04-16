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
            Debug.Log($"SwitchPencil action performed with binding index: {bindingIndex}");

            // Handle the binding index to switch pencil states
            switch (bindingIndex){
                case 0:
                    Debug.Log("Switching to PointPencilState");
                    currentState = PointPencilState.GetInstance();
                    Debug.Log(currentState);
                    break;
                case 1:
                    Debug.Log("Switching to LinePencilState");
                    currentState = LinePencilState.GetInstance();
                    Debug.Log(currentState);
                    break;
                case 2:
                    Debug.Log("Switching to SquarePencilState");
                    currentState = SquarePencilState.GetInstance();
                    Debug.Log(currentState);
                    break;
                case 3:
                    Debug.Log("Switching to BucketPencilState");
                    currentState = BucketPencilState.GetInstance();
                    Debug.Log(currentState);
                    break;
                default:
                    Debug.LogWarning("Unhandled binding index for SwitchPencil action");
                    break;
            }
        }
    }
}
