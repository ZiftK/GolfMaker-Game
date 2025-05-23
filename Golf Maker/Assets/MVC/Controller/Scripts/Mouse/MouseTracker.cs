using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

[RequireComponent(typeof(SpriteRenderer))]
public class MouseTracker : MonoBehaviour
{

    [SerializeField]
    private Sprite currentSprite;

    [SerializeField]
    private Vector2Int gridScale;

    [SerializeField]
    private Vector2 tileOffset;

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private LayerMask layerMaskFilter;

    [SerializeField]
    private UIDocument uiDocument;

    void Awake()
    {
        this.spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        this.spriteRenderer.sprite = currentSprite;
    }

    void GetMouseOverMap()
    {
        transform.position = Vector3.right * 10_000;
    }

    // Update is called once per frame
    void Update()
    {
        //todo: refactor to event system
        if (PencilTypeManager.isPointerOverUI)
        {

            GetMouseOverMap();
            return;
        }

        Vector2 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);

        if (!Physics.Raycast(castPoint, out RaycastHit hit, Mathf.Infinity, layerMaskFilter))
        {
            GetMouseOverMap();
            return;
        }

        if (hit.collider != null && hit.collider.gameObject.name == "Panel")
        {
            GetMouseOverMap();
            return;
        }

        transform.position = new Vector3(
            ((int)Math.Floor(hit.point.x)) + tileOffset.x,
            ((int)Math.Floor(hit.point.y)) + tileOffset.y,
            hit.point.z
        );
    }
}
