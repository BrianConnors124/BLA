using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;


public class InputSystemController : MonoBehaviour
{

    public static InputSystemController instance;


    private UniversalTimer _queueTimer;
    private enum Equeue{jump,attack,dash,nothing}

    private Equeue queued;
    [SerializeField] private InputActionReference Walk;
    [SerializeField] private InputActionReference Jump;
    [SerializeField] private InputActionReference Dash;
    [SerializeField] private InputActionReference Attack;
    
    

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (HandleJump())
            StartCoroutine(_queueTimer.Timer(0.1f, delegate { switch(queued){ case Equeue.nothing : break;}}));
        if (HandleDash())
            StartCoroutine(_queueTimer.Timer(0.1f, delegate { switch(queued){ case Equeue.nothing : break;}}));
        if (HandleAttack())
            StartCoroutine(_queueTimer.Timer(0.1f, delegate { switch(queued){ case Equeue.nothing : break;}}));
        
        print(queued);
    }

    private void SwitchQueue()
    {
        
    }
    
    public static Vector2 MovementInput() => instance.Walk.action.ReadValue<Vector2>();
    
    //public static bool HandleJump() => instance.Jump.action.ReadValue<float>() > 0;
    public bool HandleJump()
    {
        switch (queued)
        {
            case Equeue.jump :
                break;
        }

        return instance.Jump.action.ReadValue<float>() > 0;
    }
    public static bool HandleDash() => instance.Dash.action.ReadValue<float>() > 0;
    public static bool HandleAttack() => instance.Attack.action.ReadValue<float>() > 0;

}
