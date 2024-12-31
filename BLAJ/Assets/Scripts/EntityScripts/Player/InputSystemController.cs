using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using Debug = System.Diagnostics.Debug;


public class InputSystemController : MonoBehaviour
{

    public static InputSystemController instance;
    private bool queueActive;
    private bool buttonPressed;


    private UniversalTimer _queueTimer;
    public enum Equeue{nothing,jump,attack,dash}

    public Equeue queued;
    [SerializeField] private InputActionReference Walk;
    [SerializeField] private InputActionReference Jump;
    [SerializeField] private InputActionReference Dash;
    [SerializeField] private InputActionReference Attack;
    
    

    private void Awake()
    {
        instance = this;
        _queueTimer = GetComponent<UniversalTimer>();
    }

    private void Update()
    {
        if (queued != Equeue.nothing)
        { 
            if(!queueActive) _queueTimer.SetActionTimer("InputQue", .1f, ChangeQueuedToNothing); queueActive = true;
        }
        
    }
    
    public static Vector2 MovementInput() => instance.Walk.action.ReadValue<Vector2>();
    //public bool HandleJump() => instance.Jump.action.triggered;
    public bool HandleJump()
    {
        bool j = instance.Jump.action.triggered;
        if (j)
        {
            _queueTimer.RemoveActionTimer("InputQue");
            queueActive = false;
            queued = Equeue.jump; 
        }
        return j;
    }
    public bool HandleDash()
    {
        bool d = instance.Dash.action.triggered;
        if (d)
        {
            _queueTimer.RemoveActionTimer("InputQue");
            queueActive = false;
            queued = Equeue.dash; 
        }

        return d;
    }
    public bool HandleAttack()
    {
        bool a = instance.Attack.action.inProgress;
        if (a)
        {
            _queueTimer.RemoveActionTimer("InputQue");
            queueActive = false;
            queued = Equeue.attack; 
        }

        return a;
    }
    public void ChangeQueuedToNothing()
    {
        queued = Equeue.nothing;
        queueActive = false;
    }

    public void ButtonReleassed()
    {
        buttonPressed = false;
    }
}
