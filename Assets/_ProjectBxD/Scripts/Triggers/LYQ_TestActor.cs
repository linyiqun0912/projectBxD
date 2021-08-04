using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LYQ_TestActor : MonoBehaviour
{
    #region variables
    public float moveSpeed = 1.0f;
    private Rigidbody actorRigidbody;
    private Vector3 dVector3;
    private float dVector3Z = 0f;
    public float dVector3DampFactor=0.5f;
    public bool isTestOrReal;
    #endregion
    private void Awake()
    {
        actorRigidbody = GetComponent<Rigidbody>();

        if (isTestOrReal)
        {
            //actorRigidbody.useGravity = true;
            moveSpeed = 10.0f;
            dVector3Z = - 9.8f;
        }
        else if (!isTestOrReal)
        {
            //actorRigidbody.useGravity = false;
            moveSpeed = 50.0f;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        var keyboard = Keyboard.current;
        if (keyboard == null)
            return;
        
        ///if(keyboard.wKey.isPressed)
        ///    transform.position = transform.position + new Vector3(1,0,-1).normalized * moveSpeed;
        ///if (keyboard.sKey.isPressed)
        ///    transform.position = transform.position + new Vector3(-1, 0, 1).normalized * moveSpeed;
        ///if (keyboard.aKey.isPressed)
        ///    transform.position = transform.position + new Vector3(1, 0, 1).normalized * moveSpeed;
        ///if (keyboard.dKey.isPressed)
        ///    transform.position = transform.position + new Vector3(-1, 0, -1).normalized * moveSpeed;
        ///if (keyboard.qKey.isPressed)
        ///    transform.position = transform.position + Vector3.down * moveSpeed;
        ///if (keyboard.eKey.isPressed)
        ///    transform.position = transform.position + Vector3.up * moveSpeed;
        

        dVector3 = ((keyboard.wKey.isPressed ? 1 : 0) * new Vector3(1, 0, -1).normalized
                    + (keyboard.sKey.isPressed ? 1 : 0) * new Vector3(-1, 0, 1).normalized
                    + (keyboard.aKey.isPressed ? 1 : 0) * new Vector3(1, 0, 1).normalized
                    + (keyboard.dKey.isPressed ? 1 : 0) * new Vector3(-1, 0, -1).normalized
                    + (keyboard.qKey.isPressed ? 1 : 0) * Vector3.down
                    + (keyboard.eKey.isPressed ? 1 : 0) * Vector3.up
                    ) * moveSpeed + dVector3Z * Vector3.up;

    }

    private void FixedUpdate()
    {
        actorRigidbody.velocity = Vector3.Slerp(actorRigidbody.velocity,dVector3, dVector3DampFactor) ;
    }
}
