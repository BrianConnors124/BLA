using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Info")] 
    public EnemyInfo info;
    private string name;
    public float health;
    private string description;
    private float damage;
    private float jumpHeight;
    private float npcMovementSpeed;

    public EnemyController instance;
    
    
    
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
    
    [SerializeField] private bool jumped = false;

    [Header("Obj Detection")] 
    [SerializeField] private float lengthOfRay;
    [SerializeField] private float distanceToStopBeforePlayer;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Presets();
        _origPos = transform.position;
        lengthOfRay *= transform.localScale.x;
        groundRayLength  = DistFromGround();
    }

    void Presets()
    {
        health = info.health;
        name = info.name;
        description = info.description;
        damage = info.damage;
        jumpHeight = info.jumpHeight;
        npcMovementSpeed = info.movementSpeed;
    }
    
    //finding the ground from the center of the enemy



    private float DistFromGround()
    {
        
        float dist = transform.localScale.y + .04f; 
        
        return dist;
    }
    
    
    
    //return to beginning position ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private void FixedUpdate()
    {
        obstacleSensor =
            new Vector2(transform.position.x,
                transform.position.y); //testing to see if there is a collider in the direction of the enemy's movement


        var limitTest = Physics2D.Raycast(jumpLimit, Vector2.right, lengthOfRay, LayerMask.GetMask("WorldObj"));
        var sensor = Physics2D.Raycast(obstacleSensor, Vector2.right, lengthOfRay, LayerMask.GetMask("WorldObj"));
        
        if (!limitTest && sensor && !similarX && isTouchingGround())
        {
            Jump();
        }

        if (limitTest && sensor && !similarX)
        {
            pathBlocked = true;
            rb.velocity = new Vector2(0, rb.velocityY);
        }
        else
        {
            pathBlocked = false;
        }

        if (_player != null)
        {
            var distance = new Vector2(_player.transform.localPosition.x - transform.position.x, transform.position.y); /*  this is finding the distance that the enemy should take to reach the player     EX: x2 - x1 = D
                                                                                                                                    while keeping the same Y value and so the difference of the two is the distance the enemy must travel    */
            jumped = !isTouchingGround();
            if (cautious)
            {
                var playerDist = (float)Math.Sqrt((Math.Pow(_player.transform.localPosition.x - transform.position.x, 2) +
                                                   Math.Pow(_player.transform.localPosition.y - transform.position.y, 2)));
                hostile = !Physics2D.Raycast(transform.position,
                    (_player.transform.localPosition - transform.localPosition).normalized, playerDist,
                    LayerMask.GetMask("WorldObj"));
                passive = false;
                GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            if (hostile)
            {
                if (transform.position.x > _player.transform.position.x)
                {
                    EnemyFaceRight(false);
                }
                else
                {
                    EnemyFaceRight(true);
                }
                // cautious = false;
                GetComponent<SpriteRenderer>().color = Color.red;
                Debug.DrawLine(transform.position, _player.transform.localPosition, Color.green, .1f);
                if (distance.x >= distanceToStopBeforePlayer) //if this is true the enemy must move right
                {
                    //the enemy should moving right
                    if (!pathBlocked && !jumped)
                    {
                        rb.velocity = new Vector2(npcMovementSpeed * Time.timeScale, rb.velocity.y); //applying the velocity
                        similarX = false;
                    }
                }
                else if (distance.x <= -1 * distanceToStopBeforePlayer) //however if this is true the enemy must move left
                {
                    //the enemy should moving left
                    if (!pathBlocked && !jumped)
                    {
                        rb.velocity =
                            new Vector2(npcMovementSpeed * Time.timeScale * -1, rb.velocity.y); //applying the velocity
                        similarX = false; /*similarX is used to see if the player and enemy have an "distance" value of:  -1.08 < x < 1.08 , if so then this will be set to true,
                                                  but in this case it isn't true so it is set to false. This is to prohibit the enemy from continuously walking into the player when it is already close enough. */
                    }
                }
                else
                {
                    if (!pathBlocked && !jumped)
                    {
                        similarX = true; //as previously stated this is set to true because "distance" is not greater than 1.08 nor is it less than -1.08 meaning it fits the inequality of -1.08 < x < 1.08 so it shouldn't move anymore
                        rb.velocity = new Vector2(0, rb.velocity.y); //preventing anymore movement along the X axis
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
                if (!jumped)
                {
                    StartCoroutine(Return());
                    passive = false;
                }
            } 
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
        
        if (transform.position.x < _origPos.x)
        {
            EnemyFaceRight(true);
            pathBlocked = false;
            if (!pathBlocked && !jumped)
            {
                //if the enemy x position is less than the original x position then it will apply a positive velocity and wait until the position is greater than original x position
                rb.velocity = new Vector2( npcMovementSpeed * Time.timeScale,  rb.velocity.y);
                yield return new WaitUntil(() => transform.position.x > _origPos.x);
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        } else if(transform.position.x > _origPos.x)
        {
            EnemyFaceRight(false);
            pathBlocked = false;
            if (!pathBlocked && !jumped)
            {
                //if the enemy x position is greater than the original position then it will apply a negative velocity and wait until the position is less than original position
                rb.velocity = new Vector2(-1 * npcMovementSpeed * Time.timeScale,  rb.velocity.y);
                yield return new WaitUntil(() => transform.position.x < _origPos.x); 
                rb.velocity = new Vector2(0, rb.velocity.y); }
        }  
    }
    
    //jumping ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
    }
    
    //jumping ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    public void DamageDelt(float d)
    {
        health -= d;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        Debug.Log(name + ", " + description+ ", took " + d + " damage and now has " + health + " hp.");
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
            