using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CMF;
public class MyCharacterController : Controller
{
    private Mover mover;
    [SerializeField] bool isGrounded;
    bool lastFrameIsGrounded = true;
    bool useRootMotion;
    Transform tr;
    CharacterInput characterInput;
    Vector3 lastVelocity = Vector3.zero;
    public Transform cameraTransform;
    [SerializeField] float movementSpeed = 7f;
    [SerializeField] float gravity = 10f;
    float currentVerticalSpeed = 0f;

    private void Start()
    {
        tr = transform;
        mover = GetComponent<Mover>();
        characterInput = GetComponent<CharacterInput>();
    }

    private void FixedUpdate()
    {
        //Run initial mover ground check;
        mover.CheckForGround();

        isGrounded = mover.IsGrounded();

        //If character was not grounded int the last frame and is now grounded, call 'OnGroundContactRegained' function;
        if (lastFrameIsGrounded == false && isGrounded == true)
        {
            OnGroundedContactRegained(lastVelocity);
        }

        Vector3 _velocity = Vector3.zero;
        if (useRootMotion)
        {

        }
        else
        {
            _velocity += CalculateMovementDirection() * movementSpeed;
        }

        //Handle gravity
        if (!isGrounded)
        {
            currentVerticalSpeed -= gravity * Time.fixedDeltaTime;
        }
        else
        {
            if(currentVerticalSpeed <= 0f)
            {
                currentVerticalSpeed = 0f;
            }
        }

        _velocity += tr.up * currentVerticalSpeed;
        // do this last
        lastVelocity = _velocity;
        lastFrameIsGrounded = isGrounded;
        mover.SetExtendSensorRange(isGrounded);
        mover.SetVelocity(_velocity);
        
    }

    public override Vector3 GetVelocity()
    {
        return lastVelocity;
    }

    public override Vector3 GetMovementVelocity()
    {
        return lastVelocity;
    }

    public override bool IsGrounded()
    {
        return isGrounded;
    }

    void OnGroundedContactRegained(Vector3 _collisionVelocity)
    {
        if (OnLand != null)
        {
            OnLand(_collisionVelocity);
        }
    }

    private Vector3 CalculateMovementDirection()
    {

        if(characterInput == null)
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
        if (!isGrounded)
        {
            return _direction/3;
        }
        return _direction;
    }
}
