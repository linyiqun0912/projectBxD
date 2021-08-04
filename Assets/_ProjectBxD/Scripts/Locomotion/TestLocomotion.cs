using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLocomotion : MonoBehaviour
{
    public Camera mainCamera;
    private Rigidbody playerRigidbody;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //rigidbody.AddForce(transform.forward * 10 * Time.fixedDeltaTime);
        playerRigidbody.velocity = transform.forward ;
    }
}
