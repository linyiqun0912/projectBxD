using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LYQ_TestCameraController : MonoBehaviour
{
    private Camera myCamera;
    private Vector3 targetPosition;
    public Vector3 headPositionOffset = new Vector3(0,0,1);
    private Vector3 cameraDampVelocity;
    public float cameraChasePositionTime = 0.1f;



    private void Awake()
    {
        myCamera = Camera.main;
        myCamera.transform.position = transform.position + headPositionOffset;
    }
    // Start is called before the first frame update
    void Start()
    {
        //myCamera = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        targetPosition = transform.position + headPositionOffset;
        myCamera.transform.position = Vector3.SmoothDamp
            (myCamera.transform.position, targetPosition
            , ref cameraDampVelocity, cameraChasePositionTime);
    }
}
