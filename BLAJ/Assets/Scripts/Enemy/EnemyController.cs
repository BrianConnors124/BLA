using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    #region Data

    

    
    [Header("Info")] 
    public EnemyInfo info;
    private string name;
    private float health;
    private string description;
    private float jumpHeight;
    private float npcMovementSpeed;
    private float damage;


    [Header("Mechanics")]
    private Vector2 startingPos;
    private GameObject player;
    private bool returning;
    private bool playerInProximity;
    public bool takingDamage;
    private bool stunned;
    private float reach;
    private bool canMove;
    private UniversalTimer stunLength;
    private bool resetJump;
    private UniversalTimer jumpCD;
    
    
    
    [Header("Misc")]
    private Rigidbody2D rb;
#endregion

    #region Start




    private void Awake()
    {
        GetComponent<Animator>().runtimeAnimatorController = info.animatorController;
    }

    private void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        startingPos = transform.position;
        SetInfo();
        stunLength = new UniversalTimer();
        jumpCD = new UniversalTimer();
        StartCoroutine(jumpCD.Timer(2)); 
    }

    void SetInfo()
    {
        health = info.health;
        name = info.name;
        description = info.description;
        damage = info.damage;
        jumpHeight = info.jumpHeight;
        npcMovementSpeed = info.movementSpeed * transform.localScale.x;
        reach = info.baseReach * transform.localScale.x * 0.5f;
    }
#endregion

    #region Movement
    private void FixedUpdate()
    {
        if (!takingDamage && IsTouchingGround())
        {
            if(Mathf.Abs(rb.velocityX) >= 0.3f){
                if (playerInProximity && !PlayerOutOfSight() && OutOfReach() &&
                    ThereIsAFloorMovement())
                {
                    Move(npcMovementSpeed * PlayerDirection(), rb.velocity.y);
                }

                if ((!ThereIsAFloorMovement() || !OutOfReach()))
                {
                    Move(0, rb.velocityY);
                }
            }
            else
            {
                if (playerInProximity && !PlayerOutOfSight() && OutOfReach() &&
                    ThereIsAFloorPlayer())
                {
                    Move(npcMovementSpeed * PlayerDirection(), rb.velocity.y);
                }

                if ((!ThereIsAFloorPlayer() || !OutOfReach()))
                {
                    Move(0, rb.velocityY);
                } 
            } 
            
        
            if (ForwardObjDetection() && !ForwardObjTooHigh() && jumpCD.TimerDone)
            {
                if(Mathf.Abs(rb.velocityX) >= 0.3f && !resetJump)
                {
                    Move(rb.velocityX, jumpHeight);
                    StartCoroutine(jumpCD.Timer(2));   
                } else
                {
                    resetJump = true;
                    Move(npcMovementSpeed * -LeftOrRight(transform.position.x, ForwardObjDetection().collider.GetComponent<Transform>().position.x), rb.velocityY);
                    StartCoroutine(SituateJump());
                }
            }   
        }
    }

    private void Move(float x, float y)
    { 
        rb.velocity = new Vector2(x, y);
    }

    private IEnumerator SituateJump()
    {
        yield return new WaitUntil(() => ForwardObjDetection().collider == null);
        resetJump = false;
        Move(npcMovementSpeed * PlayerDirection(), rb.velocityY);
    }
    private IEnumerator ReturnToOrigin()
    {
        yield return new WaitUntil(() => !takingDamage);
        yield return new WaitUntil(() => IsTouchingGround());
        rb.velocity = new Vector2(npcMovementSpeed * LeftOrRight(transform.position.x, startingPos.x), rb.velocity.y);
        if (LeftOrRight(transform.position.x, startingPos.x) == 1)
        {
            yield return new WaitUntil(() => transform.position.x > startingPos.x);
            rb.velocity = new Vector2(0, rb.velocity.y);
            returning = false;
            yield break;
        }
        if (LeftOrRight(transform.position.x, startingPos.x) == -1)
        {
            yield return new WaitUntil(() => transform.position.x < startingPos.x);
            rb.velocity = new Vector2(0, rb.velocity.y);
            returning = false;
        }
        
    }
#endregion

    #region TriggerEnter/Exit

    

    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            returning = true;
            StartCoroutine(ReturnToOrigin());
            playerInProximity = false;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StopCoroutine(ReturnToOrigin());
            playerInProximity = true;
        }
    }
#endregion

    #region Damage

    

    
    [Header("Damage")] int DAMAGE;
    
    public void DamageDelt(float d, float knockback, float stun)
    {
        takingDamage = true;
        health -= d;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        StartCoroutine(DoKnockBack(knockback));
        StartCoroutine(stunLength.Timer(stun, () => takingDamage = false));
        StartCoroutine(jumpCD.Timer(4));
        Debug.Log(name + ", " + description+ ", took " + d + " damage and now has " + health + " hp.");
    }
    private IEnumerator DoKnockBack(float playerKnockBack)
    {
        rb.velocity = new Vector2(playerKnockBack * -LeftOrRight(transform.position.x, player.transform.position.x), playerKnockBack);
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => IsTouchingGround());
        rb.velocity = new Vector2(0, 0);
    }
    #endregion

    #region Raycast

    

    
    [Header("Raycast")] int RAYCAST;
    private RaycastHit2D PlayerOutOfSight() => Line.CreateAndDraw(transform.position, player.transform.position - transform.position, Line.Length(transform.position,player.transform.position),LayerMask.GetMask("WorldObj"), Color.black);
    //private RaycastHit2D ForwardObjDetection() => Line.CreateAndDraw(transform.position, new Vector2(1,0), transform.localScale.x * reach * 0.5f * PlayerDirection(), LayerMask.GetMask("WorldObj"), Color.green);
    private RaycastHit2D ForwardObjDetection() => BoxCastDrawer.BoxCastAndDraw(new Vector2(transform.position.x +(GetComponent<BoxCollider2D>().size.x * transform.localScale.x * PlayerDirection()), transform.position.y), new Vector2(transform.localScale.x, transform.localScale.y * .94f), 0, Vector2.right, 0, LayerMask.GetMask("WorldObj"));
    private RaycastHit2D ForwardObjTooHigh() => Line.CreateAndDraw(transform.position + new Vector3(0,transform.localScale.y * .6f, 0), Vector2.right, transform.localScale.x * reach * 0.5f * PlayerDirection(), LayerMask.GetMask("WorldObj"), Color.green);
    
    private RaycastHit2D IsTouchingGround() => Line.CreateAndDraw(transform.position, Vector2.down, transform.localScale.y * 0.55f, LayerMask.GetMask("WorldObj"), Color.cyan);
    private RaycastHit2D ThereIsAFloorMovement() => Line.CreateAndDraw(new Vector2(transform.position.x + (reach * MovementDirection() * transform.localScale.x * 0.5f), transform.position.y), Vector2.down, transform.localScale.y * 1.5f, LayerMask.GetMask("WorldObj"), Color.red);
    private RaycastHit2D ThereIsAFloorPlayer() => Line.CreateAndDraw(new Vector2(transform.position.x + (reach * PlayerDirection() * transform.localScale.x * 0.5f), transform.position.y), Vector2.down, transform.localScale.y * 1.5f, LayerMask.GetMask("WorldObj"), Color.red);
    #endregion

    #region Misc

    

   
    [Header("Miscelaneous")] int MISCELANEOUS;
    private int LeftOrRight(float origin, float other)
    {
        if (origin < other)
        {
            return 1;
        } else if (origin > other)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    private int PlayerDirection()
    {
        if (transform.position.x < player.transform.position.x)
        {
            return 1;
        }
        if (transform.position.x > player.transform.position.x)
        {
            return -1;
        }

        return 1;

    }

    private bool OutOfReach() => (Mathf.Abs(player.transform.position.x - transform.position.x) > transform.localScale.x * reach);
    private bool Walking() => rb.velocity.x != 0;
    private int MovementDirection()
    {
        if (rb.velocityX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            return -1;
        }
        if (rb.velocityX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            return 1;
        }

        return 1;
    }

    private void Update()
    {
        MovementDirection();
        GetComponent<EnemyAnimator>().UpdateAnimator(Walking(), false, takingDamage);
    }

    #endregion
}
