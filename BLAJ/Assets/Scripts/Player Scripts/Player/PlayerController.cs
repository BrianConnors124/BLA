using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public static PlayerController instance;
    [Header("ArmStuffs")]
    public bool armAttached;

    [Header("Healh")] 
    [SerializeField] private float health;
    private UniversalTimer stunLength;
    private bool stunned;
    private bool knockedBack;
    
    [Header("Movement")] 
    [SerializeField] private float speed;
    [SerializeField] private float speedInAir;
    [SerializeField]private float coyoteTime;
    private float coyotePH;
    public float direction;
    
    [Header("Jump")]
    [SerializeField] private float jumpHeight;
    [SerializeField] private float jumpCD;
    [SerializeField] private float endJumpMultiplier;
    [SerializeField] private float extraJumps;
    private float jumpAmountPH;
    private float baseGrav;
    private bool extraJump;
    private UniversalTimer jumpCooldown;
    

    [Header("Dash")] 
    [SerializeField] private float dashDistance;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCD;
    private Action endDash;
    private float dashDistancePH;
    [SerializeField] private float dashSlowDownInterval;
    private UniversalTimer dashDuration;
    private UniversalTimer dashCooldown;
    
    [Header("Animator Bools")] 
    private bool walkingRight;
    private bool walkingLeft;
    private bool walking;
    private bool takingDamage;
    private bool jumping;
    private bool attacking;
    private bool idleLeft;
    private bool idleRight;
    private bool idle;

    
    

    [Header("Raycast")] 
    [SerializeField] private float lengthOfRay;
    
    
    
    void Start()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        Actions();
        ActivateTimers();
        ActivatePresets();
        direction = 1;
    }

    private void ActivatePresets()
    {
        lengthOfRay *= transform.localScale.y;
        baseGrav = rb.gravityScale;
        coyotePH = coyoteTime;
        dashDistancePH = dashDistance;
        dashDistance = 0;
        jumpAmountPH = extraJumps;
        
        if (armAttached == false)
        {
            GameObject.Find("Shoulder").SetActive(false);
            GameObject.Find("ShoulderFreeMove").SetActive(true);
        }
        else
        {
            GameObject.Find("Shoulder").SetActive(true);
            GameObject.Find("ShoulderFreeMove").SetActive(false);
        }
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
        dashDuration = new UniversalTimer();
        dashCooldown = new UniversalTimer();
        groundCheck = new UniversalTimer();
        stunLength = new UniversalTimer();
    }
    // Movement (Velocity workings) ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    void FixedUpdate()
    {
        if (!stunned && !knockedBack)
        {
            walking = true;
            rb.velocity = new Vector2( (InputSystemController.MovementInput().x  + dashDistance) * speed * Time.deltaTime * 100, rb.velocity.y);   
        }
        
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
        jumping = true;
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
            rb.gravityScale = baseGrav/100;
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
    //Damage~~~~~~~~~~~~~~~~~~~~~~~~~~
    
    
    public void DamageDelt(float d, float knockback, float stun, GameObject enemy)
    {
        health -= d;
        if (health <= 0)
        {
            print("Player Has Died");
        }

        if (knockback != 0)
        {
            StartCoroutine(DoKnockBack(knockback, enemy));
        }
        if (stun != 0)
        {
            stunned = true;
            StartCoroutine(stunLength.Timer(stun, () => stunned = false));
        }
    }
    
    private IEnumerator DoKnockBack(float enemyKnockBack, GameObject enemy)
    {
        knockedBack = true;
        rb.velocity = new Vector2(enemyKnockBack * -Line.LeftOrRight(transform.position.x, enemy.transform.position.x ), enemyKnockBack);
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => TouchingGround());
        knockedBack = false;
        rb.velocity = new Vector2(0, 0);
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

    private int UpdateFace()
    {
        if (InputSystemController.MovementInput().x < 0)
        {
            return -1;
        }
        if (InputSystemController.MovementInput().x > 0)
        {
            return  1;
        }

        return 0;
    }

    private void Update()
    {
        direction = UpdateFace();
        
        if (walking)
        {
            if (direction == -1)
            {
                walkingLeft = true;
                walkingRight = false;
            }
            else
            {
                walkingRight = true;
                walkingLeft = false;
            }
        }
        else
        {
            walkingRight = false;
            walkingLeft = false;
        }

        if (rb.velocityY == 0)
        {
            jumping = false;
        }
        
        
        
        
        GetComponent<PlayerAnimation>().UpdateAnimator(walkingRight,walkingLeft,takingDamage,attacking,jumping);
    }
}
