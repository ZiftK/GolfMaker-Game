using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PencilController : MonoBehaviour
{
    
    private PencilState currentState;

    public float temporalTileOpacity = 0.5f;

    private int tileId = 0;

    private EditorLevelEvents editorLevelHandler;

    void Awake()
    {
        // currentState = PointPencilState.GetInstance();
        currentState = PointPencilState.GetInstance();
        editorLevelHandler = EditorLevelEvents.GetInstance();

        editorLevelHandler.SelectBlock += OnSelectBlock;
        editorLevelHandler.SelectPencil += OnSelectPencil;

    }

    void Update()
    {

        currentState.Update(
            new PencilContext(
                Vector3Int.RoundToInt(transform.position),
                tileId
                )
                );
    }

    public void OnSelectBlock(object sender, SelectBlockArgs args)
    {
        tileId = TileMapsFactory.GetTileIdByName(args.blockName);
    }

    public void OnSelectPencil(object sender, SelectPencilArgs args)
    {
        // Handle the selection of different pencil types
        switch (args.pincelName)
        {
            case "pen":
                currentState = PointPencilState.GetInstance();
                break;
            case "fill":
                currentState = BucketPencilState.GetInstance();
                break;
            case "brush":
                currentState = SquarePencilState.GetInstance();
                break;
            case "ruler":
                currentState = LinePencilState.GetInstance();
                break;
            default:
                Debug.LogWarning("Unhandled pencil type: " + args.pincelName);
                break;
        }
    }

    
    public void OnLeftClick(InputAction.CallbackContext context)
    {

        if (context.started)
        {
            currentState.OnLeftClick(
                new PencilContext(
                Vector3Int.RoundToInt(transform.position),
                tileId
                )
                );
        }

        if (context.canceled)
        {
            currentState.OnLeftUnClikc(
                new PencilContext(
                Vector3Int.RoundToInt(transform.position),
                tileId
                )
            );
        }
    }

    public void OnRightClick(InputAction.CallbackContext context){
        if (context.started){
            currentState.OnRightClick(
                new PencilContext(
                Vector3Int.RoundToInt(transform.position),
                tileId
                )
            );
        }

        if (context.canceled){
            currentState.OnRightUnClick(
                new PencilContext(
                Vector3Int.RoundToInt(transform.position),
                tileId
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
