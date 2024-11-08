using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputSystemController : MonoBehaviour
{

    public static InputSystemController instance;
    
    [SerializeField] private InputActionReference walking;
    
    
    
    public Action jumpAction;
    public Action endJump;
    public Action dashAction;
    public Action primaryAction;
    public Action secondaryAction;

    private void Awake()
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
                //print("big jump");
            }
            else if (context.canceled)
            {
                endJump.Invoke();
            }
    }
    public void HandleDash(InputAction.CallbackContext context)
    {
        if (dashAction != null)
        {
            dashAction.Invoke();
        }
        else
        {
            Debug.LogError("dashAction is not assigned a value");
        }
    }
    
    public void HandlePrimary(InputAction.CallbackContext context)
    {
        if(context.performed)
            primaryAction.Invoke();
    }
    
    public void HandleSecondary(InputAction.CallbackContext context)
    {
        if(context.performed)
            secondaryAction.Invoke();
    }
}
