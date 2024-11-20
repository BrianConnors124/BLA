using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCon : MonoBehaviour
{
    [Header("Info")] 
    public EnemyInfo info;
    private string name;
    public float health;
    private string description;
    private float damage;
    private float jumpHeight;
    private float npcMovementSpeed;


    [Header("Mechanics")] 
    private Vector2 startingPos;
    private GameObject player;
    private bool returning;
    private bool playerInProximity;
    
    
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
    }

    
    //This will be used for movement
    private void FixedUpdate()
    {
        
        Move(npcMovementSpeed * LeftOrRight(transform.position.x, player.transform.position.x), rb.velocity.y);
        if (ForwardObjDetection() && !ForwardObjTooHigh() && isTouchingGround())
        {
            Jump();   
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
    }


    private IEnumerator Return()
    {
        returning = true;
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
            StartCoroutine(Return());
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

    private RaycastHit2D PlayerOutOfSight() => Line.CreateAndDraw(transform.position, player.transform.position, Line.Length(transform.position,player.transform.position),LayerMask.GetMask("WorldObj"), Color.black);
    private RaycastHit2D ForwardObjDetection() => Line.CreateAndDraw(transform.position, new Vector2(1,0), transform.localScale.x * -1.43f * Direction(), Color.green);
    private RaycastHit2D ForwardObjTooHigh() => Line.CreateAndDraw(transform.position + new Vector3(0,transform.localScale.y, 0), new Vector2(1,0), transform.localScale.x * -1.43f * Direction(), Color.green);
    
    private RaycastHit2D isTouchingGround() => Line.CreateAndDraw(transform.position, Vector2.down, transform.localScale.y * -1.14f, LayerMask.GetMask("WorldObj"), Color.cyan);
    
    private void Move(float x, float y)
    {
        if (playerInProximity && !PlayerOutOfSight())
            rb.velocity = new Vector2(x, y);
    }

    [Header("Miscelaneous")] int MISCELANEOUS;
    private int LeftOrRight(float n, float f)
    {
        if (n < f)
        {
            return 1;
        } else if (n > f)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    private int Direction()
    {
        int i = 1;

        if (rb.velocityX < 0)
        {
            i = -1;
        }

        return i;
    }
}
