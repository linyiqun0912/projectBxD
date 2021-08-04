using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;


public class InputTester : MonoBehaviour
{
    #region Params
    [SerializeField] private Vector2 locomotionVectorFromKeyboard;
    [SerializeField] private Vector2 locomotionVectorFromGamepad;
    [SerializeField] private Vector3 locomotionVector;
    [SerializeField] private float locomotionLength;
    [SerializeField] bool attackTrigger;
    #endregion


    // Update is called once per frame
    void Update()
    {
        UpdateLocomotionVector();
    }


    #region Locomotion
    public void LocomotionFromKeyboard(InputAction.CallbackContext context)
    {
        locomotionVectorFromKeyboard = context.ReadValue<Vector2>();
        //Debug.Log("What");
    }

    public void LocomotionFromGamepad(InputAction.CallbackContext context)
    {
        locomotionVectorFromGamepad = context.ReadValue<Vector2>();
        //Debug.Log("Gamepad");
    }

    private void UpdateLocomotionVector()
    {
        //三种做法，一种是以手柄或键盘为主，第二种是以前虚幻里的做法，第三种如下，有待测试三选一
        //first do forward input
        locomotionVector.y = Mathf.Clamp(locomotionVectorFromKeyboard.y + locomotionVectorFromGamepad.y, -1, 1);
        //first do right input
        locomotionVector.x = Mathf.Clamp(locomotionVectorFromKeyboard.x + locomotionVectorFromGamepad.x, -1, 1);

        locomotionLength = locomotionVector.magnitude;
    }


    #endregion

    #region MeleeAttack
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("MeleeAttack");
            attackTrigger = true;
        }
    }

    public void OnAttackTried()
    {
        attackTrigger = false;
    }

    #endregion
}
