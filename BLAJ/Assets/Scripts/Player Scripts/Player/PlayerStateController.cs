using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using Input = UnityEngine.Windows.Input;

public class PlayerStateController : MonoBehaviour 
{
    private Rigidbody2D rb;
    public Animator animator;
    enum PlayerStates{Idle,Running,Jump,Attack,Airborne}
    private PlayerStates state;
    private bool stateComplete = true;

    
    private int jumpButton;
    private float movementInputX;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private bool grounded => GameObject.Find("PlayerGroundCheck").GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("WorldObj"));

    // Update is called once per frame
    void Update()
    {
        MovementUpdate();
        if (stateComplete)
        {
            SelectState();
        }
        UpdateState();
    }

    private void FixedUpdate()
    {
        Move(movementInputX * 10,rb.velocityY);
    }

    private void Move(float x, float y)
    {
        rb.velocity = new Vector2(x, y);
    }

    private void MovementUpdate()
    {
        movementInputX = InputSystemController.MovementInput().x;
        //jumpButton = InputSystemController._HandleJump();
    }

    private void UpdateState()
    {
        switch (state)
        {
            case PlayerStates.Idle:
                UpdateIdle();
                break;
            case PlayerStates.Airborne:
                UpdateAirborne();
                break;
            case PlayerStates.Attack:
                UpdateAttack();
                break;
            case PlayerStates.Running:
                UpdateRunning();
                break;
            case PlayerStates.Jump:
                UpdateJump();
                break;
        }
    }
    private void SelectState()
    {
        print("p1");
        stateComplete = false;
        if (grounded) {
            print("p2");
            if (jumpButton > 0)
            {
                print("p3");
                state = PlayerStates.Jump;
                StartJump();
            } else if (movementInputX == 0 && Mathf.Abs(rb.velocityX) < 0.1f) {
                print("p4");
                state = PlayerStates.Idle;
                StartIdle();
            } else {
                print("p5");
                state = PlayerStates.Running;
                StartRunning();
            }
        }
        else {
            state = PlayerStates.Airborne;
            StartAirborne();
        }
        
    }
    
    public void UpdateIdle()
    {
        if (!grounded || Mathf.Abs(movementInputX) > 0 || jumpButton > 0)
        {
            stateComplete = true;
        }
    }
    public void StartIdle()
    {
        animator.Play("Idle");
    }
    
    public void UpdateAirborne()
    {
        if (grounded)
        {
            stateComplete = true;
        }
    }
    public void StartAirborne()
    {
        animator.Play("Airborne");
    }
    public void UpdateRunning()
    {
        float velX = rb.velocity.x;
        
        if (!grounded || Mathf.Abs(velX) < 0.1f)
        {
            stateComplete = true;
        }
    }
    public void StartRunning()
    {
        animator.Play("Running");
    }

    public void UpdateAttack()
    {
        
    }
    public void StartAttack()
    {
        animator.Play("Attack");
    }
    
    public void UpdateJump()
    {
        float velY = rb.velocity.y;
        if (jumpButton  < 0 || Mathf.Abs(velY) > 0.1f)
        {
            stateComplete = true;
        }
    }
    public void StartJump()
    {
        Debug.Log("Jump");
    }
    
}


