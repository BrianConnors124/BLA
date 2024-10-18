using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject _player;
    private Vector3 playerDirection;
    private Vector2 _origPos;
    private Rigidbody2D rb;
    private bool checkingPos = false;
    private bool greaterThan;
    private Vector2 jumpLimit;
    private Vector2 obstacleSensor;
    private bool similarX;
    private bool hostile = false;
    private bool returning;
    
    [Header("Jumping")]
    [SerializeField] private float groundRayLength;
    [SerializeField] private float jumpHeight;

    [Header("Obj Detection")] 
    [SerializeField] private float maxJumpHeight;
    [SerializeField] private float lengthOfRay;
    [SerializeField] private float distanceToStopBeforePlayer;
    
    
    [Header("Movement Speed")]
    [SerializeField] private float npcMovementSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _origPos = transform.position;
        lengthOfRay *= transform.localScale.x;
        groundRayLength *= transform.localScale.x;
        maxJumpHeight *= transform.localScale.x;
        //jumpHeight = (float) Math.Pow(jumpHeight, transform.localScale.x);
    }
    
    
    //follow player ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !returning)//This is used to see if the collider belongs to the player
        {
            hostile = true;
            checkingPos = false;//idk what this is for 
            _player = other.gameObject; //this is assigning the "_player" gameObject to the player in the game
            playerDirection = _player.transform.position; //this updates the player's position while the player is inside the trigger collider of the enemy
            
            var distance = new Vector2(playerDirection.x - transform.position.x, transform.position.y); /*  this is finding the distance that the enemy should take to reach the player     EX: x2 - x1 = D
                                                                                                            while keeping the same Y value and so the difference of the two is the distance the enemy must travel    */
            
            if (distance.x >= distanceToStopBeforePlayer)//if this is true the enemy must move right
            {
                rb.velocity = new Vector2( npcMovementSpeed * Time.timeScale, rb.velocity.y);//applying the velocity
                EnemyFaceRight(true);//the enemy is moving right
                similarX = false; 
            } else if (distance.x <= -1 * distanceToStopBeforePlayer)//however if this is true the enemy must move left
            {
                rb.velocity = new Vector2(npcMovementSpeed * Time.timeScale * -1, rb.velocity.y);//applying the velocity
                EnemyFaceRight(false);//the enemy is moving left so that means the enemy isn't traveling left
                similarX = false;/*similarX is used to see if the player and enemy have an "distance" value of:  -1.08 < x < 1.08 , if so then this will be set to true, 
                                  but in this case it isn't true so it is set to false. This is to prohibit the enemy from continuously walking into the player when it is already close enough. */
            }
            else
            {
                similarX = true; //as previously stated this is set to true because "distance" is not greater than 1.08 nor is it less than -1.08 meaning it fits the inequality of -1.08 < x < 1.08 so it shouldn't move anymore
                rb.velocity = new Vector2(0, rb.velocity.y);//preventing anymore movement along the X axis
                
                //similarX is used to prevent the enemy from jumping when under the player, however I didn't set the Y velocity to zero because if the enemy is falling I want it to fall onto solid ground instead of staying in the air
            }
        }
        
    }
    
    //return to beginning position ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Return());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StopReturn();
        }
    }

    private void StopReturn()
    {
        //print("yo wsg");
        returning = false;
        StopCoroutine(Return());
    }

    private IEnumerator Return()//this is used to return the enemy back to its original position once the player leaves the trigger collider
    {
        returning = true;
        if (transform.position.x < _origPos.x)
        {
            //if the enemy x position is less than the original x position then it will apply a positive velocity and wait until the position is greater than original x position
            rb.velocity = new Vector2( npcMovementSpeed * Time.timeScale,  rb.velocity.y);
            EnemyFaceRight(true);
            yield return new WaitUntil(() => transform.position.x > _origPos.x);
            hostile = false;
            rb.velocity = new Vector2(0, rb.velocity.y);
        } else if(transform.position.x > _origPos.x)
        {
            //if the enemy x position is greater than the original position then it will apply a negative velocity and wait until the position is less than original position
            rb.velocity = new Vector2(-1 * npcMovementSpeed * Time.timeScale,  rb.velocity.y);
            EnemyFaceRight(false);
            yield return new WaitUntil(() => transform.position.x < _origPos.x);
            hostile = false;
            rb.velocity = new Vector2(0, rb.velocity.y);
        } 
        returning = false;
    }
    
    //jumping ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    private void Update()
    {
        
        jumpLimit = new Vector2(transform.position.x, transform.position.y + (float) maxJumpHeight );//jump height limit
        obstacleSensor = new Vector2(transform.position.x, transform.position.y);//testing to see if there is a collider in the direction of the enemy's movement
        
        
        var limitTest = Physics2D.Raycast(jumpLimit,Vector2.right, lengthOfRay, LayerMask.GetMask("WorldObj"));
        var sensor = Physics2D.Raycast(obstacleSensor,Vector2.right, lengthOfRay, LayerMask.GetMask("WorldObj"));
        
        
        if (!limitTest && sensor && !similarX && isTouchingGround() && hostile)
        {
            Jump();
        }

        if (limitTest && sensor && _player.transform.position.x - transform.position.x < lengthOfRay)
            StartCoroutine(Return());
        if (limitTest && sensor && _player.transform.position.x - transform.position.x < lengthOfRay)
        {
            //rb.velocityX = 0; 
            returning = true;
        }

    }
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
    }

    //Extra ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    private bool isTouchingGround() => Physics2D.Raycast(transform.position, Vector2.down, groundRayLength, LayerMask.GetMask("WorldObj"));
    private void EnemyFaceRight(bool movingRight)
    {
        //This is flipping the ray left and right based on the direction of the enemy movement
        if (movingRight)
        {
            lengthOfRay = Math.Abs(lengthOfRay);
        } else if (!movingRight)
        {
            lengthOfRay = Math.Abs(lengthOfRay) * -1;
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine((Vector3) jumpLimit, new Vector3(jumpLimit.x + lengthOfRay, jumpLimit.y));
        Gizmos.DrawLine((Vector3) obstacleSensor, new Vector3(obstacleSensor.x + lengthOfRay, obstacleSensor.y));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundRayLength));
    }
}
