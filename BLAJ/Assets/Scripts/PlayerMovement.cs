using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    
    
    [Header("Movement")] 
    [SerializeField] private float speed;
    [SerializeField] private float speedInAir;
    [SerializeField]private float coyoteTime;
    private float coyotePH;
    
    [Header("Jump")]
    [SerializeField] private float jumpMultiplier;
    private float inputsystemcontrollerDash;
    private float baseGrav;
    [SerializeField] private float jumpCD;
    [SerializeField] private float endJumpMultiplier;
    [SerializeField] private float extraJumps;
    private float jumpAmountPH;
    [SerializeField] private bool extraJump;
    public bool touchingGround;
    public bool jumped;
    

    [Header("Dash")] 
    [SerializeField] private float dashDistance;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCD;
    private Action endDash = null;
    private float dashDistancePH;
    [SerializeField] private float dashSlowDownInterval;
    

    [Header("Raycast")] 
    [SerializeField] private bool coyoteJump;
    [SerializeField] private float lengthOfRay;

    private UniversalTimer jumpCooldown;
    private UniversalTimer dashDuration;
    private UniversalTimer dashCooldown;
    private UniversalTimer groundCheck;
    
    
    void Start()
    {   
        rb = GetComponent<Rigidbody2D>();
        Actions();
        ActivateTimers();
        ActivatePresets();

    }

    private void ActivatePresets()
    {
        baseGrav = rb.gravityScale;
        coyotePH = coyoteTime;
        dashDistancePH = dashDistance;
        dashDistance = 0;
        jumpAmountPH = extraJumps;
    }
    private void Actions()
    {
        StartCoroutine(Retrying());
    }

    IEnumerator Retrying()
    {
        //yield return new WaitForSeconds(0.01f);
        endDash += EndDash;
        InputSystemController.instance.endJump += EndJump;
        InputSystemController.instance.jumpAction += AttemptJump;
        InputSystemController.instance.dashAction += Dash;  
        yield break;
    }
    private void ActivateTimers()
    {
        jumpCooldown = new UniversalTimer();
        jumpCooldown.Reset();
        dashDuration = new UniversalTimer();
        dashDuration.Reset();
        dashCooldown = new UniversalTimer();
        dashCooldown.Reset();
        groundCheck = new UniversalTimer();
        groundCheck.Reset();
    }
    //~~~~~~~~~~~~~~~~~~WALK~~~~~~~~~~~~~~~~~~~~~~WALK~~~~~~~~~~~~~~~~~~~~~WALK~~~~~~~~~~~~~~~~~~~~~~WALK~~~~~~~~~~~~~~~~~~~~~~~~~~WALK~~~~~~~~~~~~~~~~~~~~~~~~~
    void FixedUpdate()
    {
        
        rb.velocity = new Vector2( (InputSystemController.MovementInput().x  + dashDistance) * speed * Time.deltaTime * 100, rb.velocity.y);
        
    }
    //~~~~~~~~~~~~~~~~~~WALK~~~~~~~~~~~~~~~~~~~~~~WALK~~~~~~~~~~~~~~~~~~~~~WALK~~~~~~~~~~~~~~~~~~~~~~WALK~~~~~~~~~~~~~~~~~~~~~~~~~~WALK~~~~~~~~~~~~~~~~~~~~~~~~~
    
    
    
    
    
    
    
    

    //~~~~~~~~~~~~~~~~~~JUMP~~~~~~~~~~~~~~~~~~~~~~JUMP~~~~~~~~~~~~~~~~~~~~~JUMP~~~~~~~~~~~~~~~~~~~~~~JUMP~~~~~~~~~~~~~~~~~~~~~~~~~~JUMP~~~~~~~~~~~~~~~~~~~~~~~~~
    private void AttemptJump()
    {
        
        if (TouchingGround() && jumpCooldown.TimerDone || coyoteJump && jumpCooldown.TimerDone)
        {
            //Debug.Log("Jumped");
            jumped = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpMultiplier);
            StartCoroutine(jumpCooldown.Timer(jumpCD));
        } else if (extraJumps > 0)
        {
            //Debug.Log("Jumped Extra");
            rb.gravityScale = baseGrav;
            rb.velocity = new Vector2(rb.velocity.x, jumpMultiplier);
            extraJumps--;
        }
        
    }
    
    private void EndJump()
    {
        if (!TouchingGround() && extraJump && rb.velocityY >= 0)
        {
            //print("end jump early");
            rb.gravityScale = baseGrav * endJumpMultiplier;
        }
    }
    //~~~~~~~~~~~~~~~~~~JUMP~~~~~~~~~~~~~~~~~~~~~~JUMP~~~~~~~~~~~~~~~~~~~~~JUMP~~~~~~~~~~~~~~~~~~~~~~JUMP~~~~~~~~~~~~~~~~~~~~~~~~~~JUMP~~~~~~~~~~~~~~~~~~~~~~~~~
    private bool TouchingGround()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, lengthOfRay, LayerMask.GetMask("WorldObj"));
    }
               
    private void OnDrawGizmos()
    { 
        Vector3 endPoint = new Vector3(transform.position.x, transform.position.y - lengthOfRay, transform.position.z);
        Gizmos.DrawLine(transform.position, endPoint);
    }
                   
    void Update()
    {
        touchingGround = TouchingGround();
        if (jumped)
        {
            coyoteJump = false;
        } 
        
        if (!touchingGround && !jumped)
        {
            coyoteTime -= Time.deltaTime;
            if (coyoteTime <= 0)
            { 
                coyoteJump = false;
            } 
        }
        
        if(touchingGround)
        {
            
            extraJump = true;
            rb.gravityScale = baseGrav;
            coyoteTime = coyotePH;
            coyoteJump = true;
            extraJumps = jumpAmountPH;
        }
    }
    
    
    
    
    
    
    
    
    
    
    
    //~~~~~~~~~~~~~~~~~~DASH~~~~~~~~~~~~~~~~~~~~~~DASH~~~~~~~~~~~~~~~~~~~~~DASH~~~~~~~~~~~~~~~~~~~~~~DASH~~~~~~~~~~~~~~~~~~~~~~~~~~DASH~~~~~~~~~~~~~~~~~~~~~~~~~
    private void Dash()
    {
        if (dashCooldown.TimerDone)
        {
            rb.gravityScale = baseGrav/2;
            if(InputSystemController.MovementInput().x < 0)
                dashDistance = dashDistancePH * -1;
            if(InputSystemController.MovementInput().x > 0)
                dashDistance = dashDistancePH * 1;
            StartCoroutine(dashDuration.Timer(dashTime, endDash));
            StartCoroutine(dashCooldown.Timer(dashCD));
        }
    }
    private void EndDash()
    {
        rb.gravityScale = baseGrav;
        StartCoroutine(SlowDown());
    }
    public IEnumerator SlowDown() 
    {
        
        while (dashDistance > 0)
        {
            dashDistance -= dashSlowDownInterval;
            dashDistance = Mathf.Clamp(dashDistance, 0, dashDistancePH); yield return new WaitForEndOfFrame();
        } 
        while (dashDistance < 0)
        {
            dashDistance += dashSlowDownInterval;
            dashDistance = Mathf.Clamp(dashDistance, dashDistancePH * -1, 0);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
    }
    //~~~~~~~~~~~~~~~~~~DASH~~~~~~~~~~~~~~~~~~~~~~DASH~~~~~~~~~~~~~~~~~~~~~DASH~~~~~~~~~~~~~~~~~~~~~~DASH~~~~~~~~~~~~~~~~~~~~~~~~~~DASH~~~~~~~~~~~~~~~~~~~~~~~~~
    
    
    
    
    
    
    
    
    
    
}
