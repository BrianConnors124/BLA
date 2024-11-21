using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
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
    private bool takingDamage;
    private float reach;
    private bool canMove;
    
    
    [Header("Misc")]
    private Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startingPos = transform.position;
        SetInfo();
    }

    void SetInfo()
    {
        health = info.health;
        name = info.name;
        description = info.description;
        damage = info.damage;
        jumpHeight = info.jumpHeight;
        npcMovementSpeed = info.movementSpeed;
        reach = info.baseReach;
    }

    
    //This will be used for movement
    private void FixedUpdate()
    {
        if (playerInProximity && !PlayerOutOfSight() && !takingDamage && IsTouchingGround() && Proximity() && ThereIsAFloor())
        {
            Move(npcMovementSpeed * LeftOrRight(transform.position.x, player.transform.position.x), rb.velocity.y);
        }
        
        if (!takingDamage && IsTouchingGround() && (!Proximity() || !ThereIsAFloor()))
        { 
            Move(0,0);
        }   
        

        if (ForwardObjDetection() && !ForwardObjTooHigh() && IsTouchingGround() && rb.velocityX != 0 && !takingDamage)
        {
            Move(rb.velocityX, jumpHeight);
        }
    }

    private void Move(float x, float y)
    { 
        rb.velocity = new Vector2(x, y);
    }

    private IEnumerator ReturnToOrigin()
    {
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
            player = other.gameObject;
            playerInProximity = true;
        }
    }

    [Header("Damage")] int DAMAGE;
    
    public void DamageDelt(float d, float knockback, float stun)
    {
        takingDamage = true;
        health -= d;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        InflictKnockBack(knockback);
        StartCoroutine(new UniversalTimer().Timer(stun, ResetStun));
        Debug.Log(name + ", " + description+ ", took " + d + " damage and now has " + health + " hp.");
    }
    
    private void InflictKnockBack(float a)
    {
        rb.velocity = new Vector2(a * -LeftOrRight(transform.position.x, player.transform.position.x), a);
    }
    void ResetStun()
    {
        takingDamage = false;
    }
    

    [Header("Raycast")] int RAYCAST;
    private RaycastHit2D PlayerOutOfSight() => Line.CreateAndDraw(transform.position, player.transform.position - transform.position, Line.Length(transform.position,player.transform.position),LayerMask.GetMask("WorldObj"), Color.black);
    private RaycastHit2D ForwardObjDetection() => Line.CreateAndDraw(transform.position, new Vector2(1,0), transform.localScale.x * 1.43f * Direction(), LayerMask.GetMask("WorldObj"), Color.green);
    private RaycastHit2D ForwardObjTooHigh() => Line.CreateAndDraw(transform.position + new Vector3(0,transform.localScale.y, 0), new Vector2(1,0), transform.localScale.x * 1.43f * Direction(), LayerMask.GetMask("WorldObj"), Color.green);
    
    private RaycastHit2D IsTouchingGround() => Line.CreateAndDraw(transform.position, Vector2.down, transform.localScale.y * 1.14f, LayerMask.GetMask("WorldObj"), Color.cyan);
    private RaycastHit2D ThereIsAFloor() => Line.CreateAndDraw(new Vector2(transform.position.x + (reach * LeftOrRight(transform.position.x, player.transform.position.x)), transform.position.y), Vector2.down, transform.localScale.y * 3, LayerMask.GetMask("WorldObj"), Color.red);
    

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

    private bool Proximity() => (player.transform.position.x - transform.position.x > transform.localScale.x * reach || player.transform.position.x - transform.position.x < transform.localScale.x * -reach);

    private int Direction()
    {
        int i = 1;

        if (rb.velocity.x < 0)
        {
            i = -1;
        }

        return i;
    }
    
}
