using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject _player;
    private Vector2 _origPos;
    private Rigidbody2D rb;
    private bool checkingPos = false;
    private bool greaterThan;
    private Vector2 jumpLimit;
    private Vector2 obstacleSensor;
    private bool similarX;
    [SerializeField] private bool hostile;
    private bool left;
    [SerializeField] private bool cautious;
    [SerializeField] private bool passive = true;
    [SerializeField] private bool pathBlocked = false;
    
    
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
    
    
    
    //return to beginning position ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    
    private void FixedUpdate()
    {
        if (cautious)
        {
            var playerDist = (float)Math.Sqrt((Math.Pow(_player.transform.localPosition.x - transform.position.x, 2) + Math.Pow(_player.transform.localPosition.y - transform.position.y, 2)));
            hostile = !Physics2D.Raycast(transform.position,  (_player.transform.localPosition - transform.localPosition).normalized , playerDist, LayerMask.GetMask("WorldObj"));
            passive = false;
            GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        
        if (hostile)
        {
            // cautious = false;
            GetComponent<SpriteRenderer>().color = Color.red;
            Debug.DrawLine(transform.position, _player.transform.localPosition, Color.green, .1f);
            var distance = new Vector2(_player.transform.localPosition.x - transform.position.x, transform.position.y); /*  this is finding the distance that the enemy should take to reach the player     EX: x2 - x1 = D
                                                                                                            while keeping the same Y value and so the difference of the two is the distance the enemy must travel    */
            if (!pathBlocked)
            {
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
        else
        {
            Debug.DrawLine(transform.localPosition, _player.transform.localPosition, Color.red, .1f);
        }
        

        if (passive)
        {
            StartCoroutine(Return());
            passive = false;
        }
        
        
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cautious = false;
            hostile = false;
            passive = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _player = other.gameObject;
            cautious = true; 
        }
    }
    

    private IEnumerator Return()//this is used to return the enemy back to its original position once the player leaves the trigger collider
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        if (!pathBlocked)
        {
            if (transform.position.x < _origPos.x)
            {
                //if the enemy x position is less than the original x position then it will apply a positive velocity and wait until the position is greater than original x position
                rb.velocity = new Vector2( npcMovementSpeed * Time.timeScale,  rb.velocity.y);
                EnemyFaceRight(true);
                yield return new WaitUntil(() => transform.position.x > _origPos.x);
                rb.velocity = new Vector2(0, rb.velocity.y);
            } else if(transform.position.x > _origPos.x)
            {
                //if the enemy x position is greater than the original position then it will apply a negative velocity and wait until the position is less than original position
                rb.velocity = new Vector2(-1 * npcMovementSpeed * Time.timeScale,  rb.velocity.y);
                EnemyFaceRight(false);
                yield return new WaitUntil(() => transform.position.x < _origPos.x);
                rb.velocity = new Vector2(0, rb.velocity.y);
            }  
        }
    }
    
    //jumping ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    private void Update()
    {
        
        jumpLimit = new Vector2(transform.position.x, transform.position.y + (float) maxJumpHeight );//jump height limit
        obstacleSensor = new Vector2(transform.position.x, transform.position.y);//testing to see if there is a collider in the direction of the enemy's movement
        
        
        var limitTest = Physics2D.Raycast(jumpLimit,Vector2.right, lengthOfRay, LayerMask.GetMask("WorldObj"));
        var sensor = Physics2D.Raycast(obstacleSensor,Vector2.right, lengthOfRay, LayerMask.GetMask("WorldObj"));
        
        
        if (!limitTest && sensor && !similarX && isTouchingGround())
        {
            Jump();
        }

        if (limitTest && sensor && _player.transform.position.x - transform.position.x < lengthOfRay)
        {
            pathBlocked = true;
            rb.velocity = new Vector2(0, rb.velocityY);
        }
        else
        {
            pathBlocked = false;
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
        //Gizmos.DrawLine(transform.position, playerDirection);
    }
}
