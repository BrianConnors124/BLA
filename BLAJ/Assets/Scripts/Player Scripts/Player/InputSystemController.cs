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
    public enum Equeue{nothing,jump,attack,dash}

    public Equeue queued;
    [SerializeField] private InputActionReference Walk;
    [SerializeField] private InputActionReference Jump;
    [SerializeField] private InputActionReference Dash;
    [SerializeField] private InputActionReference Attack;
    
    

    private void Awake()
    {
        instance = this;
        _queueTimer = new UniversalTimer();
    }

    private void Update()
    {
        if (queued == Equeue.jump)
            StartCoroutine(_queueTimer.Timer(10, ChangeQueuedToNothing));
        if (queued == Equeue.dash)
            StartCoroutine(_queueTimer.Timer(10, ChangeQueuedToNothing));
        if (queued == Equeue.attack)
            StartCoroutine(_queueTimer.Timer(10, ChangeQueuedToNothing));
    }
    
    public static Vector2 MovementInput() => instance.Walk.action.ReadValue<Vector2>();
    public bool HandleJump() => instance.Jump.action.ReadValue<float>() > 0;
    public static bool HandleDash() => instance.Dash.action.ReadValue<float>() > 0;
    public static bool HandleAttack() => instance.Attack.action.ReadValue<float>() > 0;
    private void ChangeQueuedToNothing() => queued = Equeue.nothing;

}
