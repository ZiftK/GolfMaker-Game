using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PencilController : MonoBehaviour
{
    
    private PencilState currentState;
    private IUpdatePencilState currentUpdateState;

    public float temporalTileOpacity = 0.5f;

    private int id = 0;

    private PencilSetType pencilSetType;

    private EditorLevelEvents editorLevelHandler;

    void Awake()
    {
        // currentState = PointPencilState.GetInstance();
        currentState = PointPencilState.GetInstance();
        currentUpdateState = PointPencilState.GetInstance();
        editorLevelHandler = EditorLevelEvents.GetInstance();

        editorLevelHandler.SelectBlock += OnSelectBlock;
        editorLevelHandler.SelectPencil += OnSelectPencil;

    }

    void Update()
    {

        Debug.Log(currentUpdateState);
        if (currentUpdateState is not null)
        {
            currentUpdateState.Update(
            new PencilContext(
                Vector3Int.RoundToInt(transform.position),
                id,
                pencilSetType
                )
                );
        }
    }

    public void OnSelectBlock(object sender, SelectBlockArgs args)
    {
        if (args.pencilSetType == PencilSetType.Tile)
        {
            id = TileMapsFactory.GetTileIdByName(args.blockName);
        }

        if (args.pencilSetType == PencilSetType.PlaceObject)
        {
            id = PlaceObjectsFactory.GetPlaceObjectIdByName(args.blockName);
        }

        pencilSetType = args.pencilSetType;
    }

    public void OnSelectPencil(object sender, SelectPencilArgs args)
    {
        // Handle the selection of different pencil types
        switch (args.pincelName)
        {

            case "pen":
                currentState = PointPencilState.GetInstance();
                currentUpdateState = PointPencilState.GetInstance();
                break;
            case "ruler":
                currentState = LinePencilState.GetInstance();
                currentUpdateState = LinePencilState.GetInstance();
                break;
            case "brush":
                currentState = SquarePencilState.GetInstance();
                currentUpdateState = SquarePencilState.GetInstance();
                break;
            case "fill":
                currentState = BucketPencilState.GetInstance();
                currentUpdateState = null;
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
                id,
                pencilSetType
                )
                );
        }

        if (context.canceled)
        {
            currentState.OnLeftUnClikc(
                new PencilContext(
                Vector3Int.RoundToInt(transform.position),
                id,
                pencilSetType
                )
            );
        }
    }

    public void OnRightClick(InputAction.CallbackContext context){
        if (context.started){
            currentState.OnRightClick(
                new PencilContext(
                Vector3Int.RoundToInt(transform.position),
                id,
                pencilSetType
                )
            );
        }

        if (context.canceled){
            currentState.OnRightUnClick(
                new PencilContext(
                Vector3Int.RoundToInt(transform.position),
                id,
                pencilSetType
                )
            );
        }
    }
}
