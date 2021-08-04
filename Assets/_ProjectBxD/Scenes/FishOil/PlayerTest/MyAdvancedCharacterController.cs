using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CMF;

public class MyAdvancedCharacterController : Controller
{

    //References to attached components;
    protected Transform tr;
    protected Mover mover;
    protected CharacterInput characterInput;

    //Movement speed;
    public float movementSpeed = 7f;

    //How fast the controller can change direction while in the air;
    //Higher values result in more air control;
    public float airControlRate = 2f;

    //'AirFriction' determines how fast the controller loses its momentum while in the air;
    //'GroundFriction' is used instead, if the controller is grounded;
    public float airFriction = 0.5f;
    public float groundFriction = 100f;

    //Current momentum;
    protected Vector3 momentum = Vector3.zero;

    //Saved velocity from last frame;
    Vector3 savedVelocity = Vector3.zero;

    //Saved horizontal movement velocity from last frame;
    Vector3 savedMovementVelocity = Vector3.zero;

    //Amount of downward gravity;
    public float gravity = 30f;

    [Tooltip("Whether to calculate and apply momentum relative to the controller's transform.")]
    public bool useLocalMomentum = false;

    public Transform cameraTransform;

    #region
    void Awake()
    {
        mover = GetComponent<Mover>();
        tr = transform;
        characterInput = GetComponent<CharacterInput>();

        if (characterInput == null)
            Debug.LogWarning("No character input script has been attached to this gameobject", this.gameObject);

        Setup();
    }

    protected virtual void Setup()
    {
    }
    #endregion

    #region
    private void FixedUpdate()
    {
        
    }

    void ControllerUpdate()
    {
        mover.CheckForGround();


    }
    #endregion

    public override Vector3 GetVelocity()
    {
        throw new System.NotImplementedException();
    }

    public override Vector3 GetMovementVelocity()
    {
        throw new System.NotImplementedException();
    }

    public override bool IsGrounded()
    {
        throw new System.NotImplementedException();
    }

    private Vector3 CalculateMovementDirection()
    {

        if (characterInput == null)
        {
            return Vector3.zero;
        }

        Vector3 _direction = Vector3.zero;


        //If no camera transform has been assigned, use the character's transform axes to calculate the movement direction;
        if (cameraTransform == null)
        {
            _direction += tr.right * characterInput.GetHorizontalMovementInput();
            _direction += tr.forward * characterInput.GetVerticalMovementInput();
        }
        else
        {
            //If a camera transform has been assigned, use the assigned transform's axes for movement direction;
            //Project movement direction so movement stays parallel to the ground;
            _direction += Vector3.ProjectOnPlane(cameraTransform.right, tr.up).normalized * characterInput.GetHorizontalMovementInput();
            _direction += Vector3.ProjectOnPlane(cameraTransform.forward, tr.up).normalized * characterInput.GetVerticalMovementInput();
        }


        if (_direction.magnitude > 1f)
        {
            _direction.Normalize();
        }
        return _direction;
    }
}
