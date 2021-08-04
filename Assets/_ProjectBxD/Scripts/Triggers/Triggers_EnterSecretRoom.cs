using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggers_EnterSecretRoom : MonoBehaviour
{
    #region variables
    private Camera myCamera;
    [Range(0, 5)]
    public int insideState_z_Reverse = 1;
    [Range(0, 5)]
    public int outsideState_z = 0;
    private int insideCullingMask = 0;
    private int outsideCullingMask = 0;
    #endregion
    void Awake()
    {
        myCamera = Camera.main;
        //if (myCamera != null)            Debug.Log(transform.forward+ name +"yes");
        //else             Debug.Log("no");
    }

    // Start is called before the first frame update
    void Start()
    {
        insideCullingMask = GetTargetCullingMask (insideState_z_Reverse);
        outsideCullingMask = GetTargetCullingMask(outsideState_z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private int GetTargetCullingMask(int doorType)
    {
        switch (doorType)
        {
            case 0:
                return -1;
            case 1:
                return (int)(Mathf.Pow(2.0f, LayerMask.NameToLayer("SecretRoom1"))
                     + Mathf.Pow(2.0f, LayerMask.NameToLayer("Player")));
            case 2:
                return (int)(Mathf.Pow(2.0f, LayerMask.NameToLayer("SecretRoom2"))
                     + Mathf.Pow(2.0f, LayerMask.NameToLayer("Player")));
            case 3:
                return (int)(Mathf.Pow(2.0f, LayerMask.NameToLayer("SecretRoom3"))
                     + Mathf.Pow(2.0f, LayerMask.NameToLayer("Player")));
            default:
                return -1;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            myCamera.cullingMask = insideCullingMask;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Vector3.Dot((other.transform.position - transform.position), transform.forward) > 0)
            {
                myCamera.cullingMask = outsideCullingMask;
            }
        }
    }
}
