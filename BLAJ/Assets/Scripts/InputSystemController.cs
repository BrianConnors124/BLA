using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputSystemController : MonoBehaviour
{

    public static InputSystemController instance;
    
    [SerializeField] private InputActionReference walking;
    [SerializeField] private InputActionReference jumping;
    [SerializeField] private InputActionReference dashing;
    [SerializeField] private InputActionReference zoomOut;

    public Action _zoomOut;
    public Action _zoomIn;
    public Action jumpAction;
    public Action endJump;
    public Action dashAction;

    private void Start()
    {
        instance = this;
    }

    public static Vector2 MovementInput()
    {
        return instance.walking.action.ReadValue<Vector2>();
    }
    

    public void HandleJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //Debug.Log("VAR");
                jumpAction.Invoke();
        }
        else if (context.canceled)
        {
            endJump.Invoke();
        }
    }
    public void HandleDash(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            dashAction.Invoke();
        }
    }
    
    public void HandleZoom(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            print("zoom");
            _zoomOut.Invoke();
        }

        if (context.canceled)
        {
            _zoomIn.Invoke();
        }
    }
}
