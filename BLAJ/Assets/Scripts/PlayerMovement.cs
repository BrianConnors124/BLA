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
    [SerializeField] private float jumpHeight;
    [SerializeField] private float jumpCD;
    [SerializeField] private float endJumpMultiplier;
    [SerializeField] private float extraJumps;
    private float jumpAmountPH;
    private float baseGrav;
    private bool extraJump = false;
    

    [Header("Dash")] 
    [SerializeField] private float dashDistance;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCD;
    private Action endDash;
    private float dashDistancePH;
    [SerializeField] private float dashSlowDownInterval;
    

    [Header("Raycast")] 
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
        endDash += EndDash;
        InputSystemController.instance.endJump += EndJump;
        InputSystemController.instance.jumpAction += AttemptJump;
        InputSystemController.instance.dashAction += Dash;  
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
    // Movement (Velocity workings) ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    void FixedUpdate()
    {
        if(!TouchingGround())
            rb.velocity = new Vector2( (InputSystemController.MovementInput().x  + dashDistance) * speedInAir * Time.deltaTime * 100, rb.velocity.y);
        if(TouchingGround())
            rb.velocity = new Vector2( (InputSystemController.MovementInput().x  + dashDistance) * speed * Time.deltaTime * 100, rb.velocity.y);
        
        if (!TouchingGround())
            coyoteTime -= Time.deltaTime;

        if (TouchingGround())
        {
            coyoteTime = coyotePH;
            rb.gravityScale = baseGrav;
            extraJumps = jumpAmountPH;
            extraJump = false;
        }
        
    }
    
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        StartCoroutine(jumpCooldown.Timer(jumpCD));
    }
    
    private void EndJump()
    {
        if(rb.velocityY >= 0 && !TouchingGround() && !extraJump)
            rb.gravityScale = baseGrav * endJumpMultiplier;
    }
    
    // Jump Configuration ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    private void AttemptJump()
    {
        if (jumpCooldown.TimerDone)
        {
            if (coyoteTime > 0)
            { 
                coyoteTime = 0;
                Jump();
            }  else if (extraJumps > 0)
            {
                extraJump = true;
                rb.gravityScale = baseGrav;
                Jump();
                extraJumps--;
            }
            
        }
        
    }
    
    // Dash Configuration ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    private void Dash()
    {
        if (dashCooldown.TimerDone)
        {
            rb.gravityScale = baseGrav/1.5f;
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
    // Extra ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    
    
    private bool TouchingGround()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, lengthOfRay, LayerMask.GetMask("WorldObj"));
    }
               
    private void OnDrawGizmos()
    { 
        Vector3 endPoint = new Vector3(transform.position.x, transform.position.y - lengthOfRay, transform.position.z);
        Gizmos.DrawLine(transform.position, endPoint);
    }
    
    
    
    
    
    
    
}
