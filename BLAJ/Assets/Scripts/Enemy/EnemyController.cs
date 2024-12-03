using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
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
    private float knockBack;
    private float stun;
    private float primaryCooldown;
    public GameObject warningSign;


    [Header("Mechanics")]
    private Vector2 startingPos;
    private GameObject player;
    private bool returning;
    private bool playerInProximity;
    private bool takingDamage;
    private bool stunned;
    private float reach;
    private bool canMove;
    private UniversalTimer stunLength;
    private bool resetJump;
    private UniversalTimer jumpCD;
    [SerializeField] private bool attacking;
    private int attackCount = 1;
    private bool boss;
    private UniversalTimer primaryCD;
    
    
    
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
        primaryCD = new UniversalTimer();
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
        npcMovementSpeed = info.movementSpeed;
        reach = info.baseReach * transform.localScale.x / 4;
        stun = info.stun;
        knockBack = info.knockBack;
        primaryCooldown = info.primaryCD;
        boss = info.bossType;
    }
#endregion

    #region Movement
    private void FixedUpdate()
    {
        if (!takingDamage && IsTouchingGround() && !attacking)
        {
            if (playerInProximity && !PlayerOutOfSight() && ThereIsAFloor())
            {
                Move(npcMovementSpeed * PlayerDirection(), rb.velocity.y);
            }
            if (!ThereIsAFloor())
            { 
                Move(0, rb.velocityY);
            }

            if (!OutOfReachX() && !OutOfReachY() && IsTouchingGround())
            {
                Move(0, rb.velocityY);
                if (attackCount > 0)
                {
                    AttackStage1();
                    attackCount--;
                }
            }

            if (!IsTouchingGround())
            {
                StartCoroutine(jumpCD.Timer(2)); 
            }
        
            if (ForwardObjDetection())
            {
                if (!ForwardObjTooHigh() && jumpCD.TimerDone)
                {
                    if(rb.velocityX != 0 && !resetJump)
                    {
                        Move(rb.velocityX, jumpHeight);
                        StartCoroutine(jumpCD.Timer(3));   
                    } else
                    {
                        resetJump = true;
                        StartCoroutine(SituateJump());
                    }   
                }
                else if(jumpCD.TimerDone)
                {
                    Move(0, rb.velocityY);
                }
            }   
        }
    }

    private IEnumerator SituateJump()
    {
        Move(npcMovementSpeed * -Line.LeftOrRight(transform.position.x, ForwardObjDetection().collider.GetComponent<Transform>().position.x), rb.velocityY);
        yield return new WaitUntil(() => ForwardObjDetection().collider == null);
        resetJump = false;
        Move(npcMovementSpeed * PlayerDirection(), rb.velocityY);
    }
    private IEnumerator ReturnToOrigin()
    {
        yield return new WaitUntil(() => !takingDamage);
        yield return new WaitUntil(() => IsTouchingGround());
        rb.velocity = new Vector2(npcMovementSpeed * Line.LeftOrRight(transform.position.x, startingPos.x), rb.velocity.y);
        if (Line.LeftOrRight(transform.position.x, startingPos.x) == 1)
        {
            yield return new WaitUntil(() => transform.position.x > startingPos.x);
            rb.velocity = new Vector2(0, rb.velocity.y);
            returning = false;
            yield break;
        }
        if (Line.LeftOrRight(transform.position.x, startingPos.x) == -1)
        {
            yield return new WaitUntil(() => transform.position.x < startingPos.x);
            rb.velocity = new Vector2(0, rb.velocity.y);
            returning = false;
        }
        
    }
    
    private void Move(float x, float y)
    { 
        rb.velocity = new Vector2(x, y);
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

        if (!boss)
        {
            StartCoroutine(DoKnockBack(knockback));
            StartCoroutine(stunLength.Timer(stun, () => takingDamage = false));
        }
        else
        {
            takingDamage = false;
        }

        StartCoroutine(DoDamageColor());
        StartCoroutine(jumpCD.Timer(4));
        Debug.Log(name + ", " + description+ ", took " + d + " damage and now has " + health + " hp.");
    }
    private IEnumerator DoKnockBack(float playerKnockBack)
    {
        rb.velocity = new Vector2(playerKnockBack * -Line.LeftOrRight(transform.position.x, player.transform.position.x), playerKnockBack);
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => IsTouchingGround());
        rb.velocity = new Vector2(0, 0);
        
    }

    private IEnumerator DoDamageColor()
    {
        //GetComponent<SpriteRenderer>().color = new Color(224, 138, 138, 255);
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }




    [Header("Dealing Damage")] int DEALING;
    
    
    private void AttackStage1()
    {
        attacking = true;
        StartCoroutine(primaryCD.Timer(primaryCooldown, () => { attackCount = 1;})); 
        warningSign.SetActive(true);
        StartCoroutine(new UniversalTimer().Timer(0.2f, AttackStage2));
    }

    private void AttackStage2()
    {
        StartCoroutine(new UniversalTimer().Timer(0.4f, AttackStage3));
    }
    private void AttackStage3()
    {
        RaycastHit2D a = BoxCastDrawer.BoxCastAndDraw(new Vector2(transform.position.x +(reach * PlayerDirection()), transform.position.y),new Vector2(transform.localScale.x/2,transform.localScale.y), 0, new Vector2(PlayerDirection(), 0),0, LayerMask.GetMask("Player"));
        if (a.collider != null)
            a.collider.GetComponent<PlayerController>().DamageDelt(damage, knockBack, stun, gameObject);
        warningSign.SetActive(false);
        attacking = false;
    }
    
    #endregion

    #region Raycast

    

    
    [Header("Raycast")] int RAYCAST;
    private RaycastHit2D PlayerOutOfSight() => Line.CreateAndDraw(transform.position, player.transform.position - transform.position, Line.Length(transform.position,player.transform.position),LayerMask.GetMask("WorldObj"), Color.black);
    //private RaycastHit2D ForwardObjDetection() => Line.CreateAndDraw(transform.position, new Vector2(1,0), transform.localScale.x * reach * 0.5f * PlayerDirection(), LayerMask.GetMask("WorldObj"), Color.green);
    private RaycastHit2D ForwardObjDetection() => BoxCastDrawer.BoxCastAndDraw(new Vector2(transform.position.x +(GetComponent<BoxCollider2D>().size.x * transform.localScale.x * PlayerDirection()), transform.position.y), new Vector2(transform.localScale.x * .5f, transform.localScale.y * .94f), 0, Vector2.right, 0, LayerMask.GetMask("WorldObj"));
    private RaycastHit2D ForwardObjTooHigh() => Line.CreateAndDraw(transform.position + new Vector3(0,transform.localScale.y * .6f, 0), Vector2.right, transform.localScale.x * PlayerDirection() * .45f, LayerMask.GetMask("WorldObj"), Color.green);
    
    private bool IsTouchingGround1() => Line.CreateAndDraw(transform.position, Vector2.down, transform.localScale.y * 0.55f, LayerMask.GetMask("WorldObj"), Color.cyan);
    private bool IsTouchingGround2() => Line.CreateAndDraw(new Vector2(transform.position.x - (GetComponent<BoxCollider2D>().size.x * 0.5f), transform.position.y), Vector2.down, transform.localScale.y * 0.55f, LayerMask.GetMask("WorldObj"), Color.cyan);
    private bool IsTouchingGround3() => Line.CreateAndDraw(new Vector2(transform.position.x + (GetComponent<BoxCollider2D>().size.x * 0.5f), transform.position.y), Vector2.down, transform.localScale.y * 0.55f, LayerMask.GetMask("WorldObj"), Color.cyan);

    private bool IsTouchingGround()
    {
        if (IsTouchingGround1())
        {
            return true;
        } else if (IsTouchingGround2())
        {
            return true;
        } else if (IsTouchingGround3())
        {
            return true;
        }

        return false;
    }

    private RaycastHit2D ThereIsAFloor()
    {
        if (rb.velocity.x != 0)
        {
            return Line.CreateAndDraw(new Vector2(transform.position.x + (reach * PlayerDirection()), transform.position.y), Vector2.down, transform.localScale.y * 1.3f, LayerMask.GetMask("WorldObj"), Color.red);
        }
        else
        {
            return Line.CreateAndDraw(new Vector2(transform.position.x + (reach * MovementDirection()), transform.position.y), Vector2.down, transform.localScale.y * 1.3f, LayerMask.GetMask("WorldObj"), Color.red);
        }
    }
    #endregion

    #region Misc

    

   
    [Header("Miscelaneous")] int MISCELANEOUS;

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

    private bool OutOfReachX() => Mathf.Abs(Mathf.Abs(transform.position.x) - Mathf.Abs(player.transform.position.x)) > reach;
    private bool OutOfReachY() => Mathf.Abs( Mathf.Abs(transform.position.y) - Mathf.Abs(player.transform.position.y)) > transform.localScale.y;
    
    private bool Walking() => rb.velocity.x != 0 && !attacking;
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
       if(!attacking)
            MovementDirection();
       GetComponent<EnemyAnimator>().UpdateAnimator(Walking(), takingDamage, attacking);
       
       
    //    print("Ray Casts: \n" + "PlayerOutOfSight: " + (bool) PlayerOutOfSight() + "\nForwardObjDetection: " + (bool) ForwardObjDetection() + "\nForwardObjTooHigh: " + (bool) ForwardObjTooHigh() + "\nIsTouchingGround: " + (bool) IsTouchingGround() + "\nThereIsAFloor: " + (bool) ThereIsAFloor()
    //    + "\n\nBool Methods: \n" + "OutOfReachX: " + OutOfReachX() + "\nOutOfReachY: " + OutOfReachY() + "\nWalking: "+ Walking() + "\n\nInt Methods: \n" + "MovementDirection: " + MovementDirection() + "PlayerDirection: " + PlayerDirection());
    }

    #endregion
}
