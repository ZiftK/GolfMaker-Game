using System;
using TMPro.EditorUtilities;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class MouseTracker : MonoBehaviour
{

    private Vector3 _worldPosition;

    [SerializeField]
    private Sprite currentSprite;

    [SerializeField]
    private Vector2Int gridScale;

    [SerializeField]
    private Vector2 tileOffset;

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private LayerMask layerMaskFilter;

    void Awake()
    {
        this.spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        this.spriteRenderer.sprite = currentSprite;
    }

    // Update is called once per frame
    void Update()
    {   
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);

        if (Physics.Raycast(castPoint, out RaycastHit hit, Mathf.Infinity, layerMaskFilter))
        {
            transform.position = new Vector3(
                ((int) Math.Floor(hit.point.x)) + tileOffset.x,
                ((int) Math.Floor(hit.point.y)) + tileOffset.y,
                hit.point.z
            );
        }

    }



    public Vector3 worldPosition{
        get {return _worldPosition;}
    }
}
