using System;
using Unity.VisualScripting;
using UnityEditor.Embree;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class CameraMove : MonoBehaviour
{

    #region //hd: linear move

    [Header("Linear move")]
    [SerializeField]
    private float movingSpeed = 2;

    [SerializeField]
    private float speedMultiplier = 2;

    private float currentMultiplier;

    [Space(20)]

    #endregion
    
    #region //hd: zoom

    [Header("Zoom")]

    [SerializeField]
    private float zoomSpeed = 2;

    private float zoomVelocity;

    #endregion
    

    #region //hd: limits

    [SerializeField]
    private Vector3 lowLimits = new Vector3(-100, -100, -5);

    [SerializeField]
    private Vector3 highLimits = new Vector3(100, 100, -30);

    #endregion

    private Rigidbody rb;

    private Vector3 linearVelocity;

    private delegate float DrageDelegate(float value);

    private void Awake() {
        rb = gameObject.GetComponent<Rigidbody>();
        currentMultiplier = 1;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        linearVelocity *= currentMultiplier;
    }

    private void FixedUpdate() {

        rb.linearVelocity = this.linearVelocity;

        CheckXYBounds();
        CheckZBounds();
    }


    #region XY move
    private void CheckXYBounds(){
        if (transform.position.x < lowLimits.x){
            transform.position = OnlyReadVector3.GetNewUsingX(transform.position, lowLimits.x);
        }

        if (transform.position.y < lowLimits.y){
            transform.position = OnlyReadVector3.GetNewUsingY(transform.position, lowLimits.y);
        }

        if (transform.position.x > highLimits.x){
            transform.position = OnlyReadVector3.GetNewUsingX(transform.position, highLimits.x);
        }

        if (transform.position.y > highLimits.y){
            transform.position = OnlyReadVector3.GetNewUsingY(transform.position, highLimits.y);
        }

    }

    private void ModifyPlaneVelocity(Vector2 planeVelocity){
        OnlyReadVector3.SetXY(ref this.linearVelocity, planeVelocity);
    }

    //* event to move
    public void OnMove(InputAction.CallbackContext context){

        Vector2 linearDirection = context.ReadValue<Vector2>();
        
        
        if (context.performed){

            ModifyPlaneVelocity(linearDirection*movingSpeed);
        }
        if (context.canceled){
            this.linearVelocity = Vector3.zero;
        }
    }

    #endregion

    #region Z move

    private void CheckZBounds(){

        if (transform.position.z > lowLimits.z ){
            transform.position = OnlyReadVector3.GetNewUsingZ(transform.position, lowLimits.z);
        }
        if (transform.position.z < highLimits.z ){
            transform.position = OnlyReadVector3.GetNewUsingZ(transform.position, highLimits.z);
        }
    }

    private void ModifyZVelocity(float zVel){
        
        OnlyReadVector3.SetZ(ref linearVelocity, zVel);
    }

    //* event to zoom
    public void OnZoom(InputAction.CallbackContext context){
        
        
        float zoomDirection = context.ReadValue<float>();
        
        if (context.performed){
            
            ModifyZVelocity(zoomDirection*zoomSpeed);
        }
        if (context.canceled){
            this.linearVelocity = Vector3.zero;
        }
    }

    #endregion

    #region run

    public void OnRun(InputAction.CallbackContext context){
        
        if (context.performed){
            currentMultiplier = this.speedMultiplier;
        }
        if (context.canceled){
            currentMultiplier = 1;
        }
    }

    #endregion
}
