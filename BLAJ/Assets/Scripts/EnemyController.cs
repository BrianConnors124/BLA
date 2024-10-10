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
    public float lengthOfRay;
    private Vector2 startingPoint;
    private Vector2 obstaclePos;
    public float jumpHeight;
    public float groundRayLength;
    private bool movingRight;
    
    [Header("Movement Speed")]
    [SerializeField] private float npcMovementSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _origPos = transform.position;
    }
    private void Update()
    {
        if (checkingPos)
        {
            if (greaterThan)
            {
                if (transform.position.x <= _origPos.x)
                {
                    rb.velocity = new Vector2(0, 0);
                    transform.position = new Vector3(_origPos.x, transform.position.y, transform.position.z);
                    checkingPos = false;
                }
            } else
            {
                if (transform.position.x >= _origPos.x)
                {
                    rb.velocity = new Vector2(0, 0);
                    transform.position = new Vector3(_origPos.x, transform.position.y, transform.position.z);
                    checkingPos = false;
                }
            }
        }
        else if(transform.position.x != _origPos.x)
        {
            greaterThan = transform.position.x > _origPos.x;
        }
        //jumping
        if (movingRight)
        {
            lengthOfRay = Math.Abs(lengthOfRay);
        } else if (!movingRight)
        {
            lengthOfRay = Math.Abs(lengthOfRay) * -1;
        }
        
        startingPoint = new Vector2(transform.position.x, transform.position.y + transform.localScale.x + 1 );//jump height limit
        obstaclePos = new Vector2(transform.position.x, transform.position.y);//testing to see if there is a collider in front
        bool isJump = Physics2D.Raycast(startingPoint,Vector2.right, lengthOfRay, LayerMask.GetMask("WorldObj"));
        bool isObstacle = Physics2D.Raycast(obstaclePos,Vector2.right, lengthOfRay, LayerMask.GetMask("WorldObj"));
        if (!isJump && isObstacle)
        {
            print(isTouchingGround());
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(startingPoint, new Vector3(startingPoint.x + lengthOfRay, startingPoint.y));
        Gizmos.DrawLine(obstaclePos, new Vector3(obstaclePos.x + lengthOfRay, obstaclePos.y));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundRayLength));
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            Invoke("Return", .5f);
            //print(Direction);
        }
    }

    private void Return()
    {
        var Direction = new Vector2(_origPos.x - transform.position.x, transform.position.y).normalized;
        if (Direction.x > 0)
        {
            rb.velocity = new Vector2( npcMovementSpeed * Time.timeScale,  rb.velocity.y);
            movingRight = true;
        } else if (Direction.x < 0)
        {
            rb.velocity = new Vector2(-1 * npcMovementSpeed * Time.timeScale,  rb.velocity.y);
            movingRight = false;
        }
        
        checkingPos = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            checkingPos = false;
            //print("Attack");
            _player = other.gameObject;
            playerDirection = _player.transform.position;
            var Direction = new Vector2(playerDirection.x - transform.position.x, transform.position.y);
            
            if (Direction.x >= 1.08)
            {
                rb.velocity = new Vector2( npcMovementSpeed * Time.timeScale, rb.velocity.y);
                movingRight = true;
            } else if (Direction.x <= -1.08)
            {
                rb.velocity = new Vector2(npcMovementSpeed * Time.timeScale * -1, rb.velocity.y);
                movingRight = false;
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
                
            //print(Direction);
        }
        
    }

    private bool isTouchingGround() => Physics2D.Raycast(transform.position, Vector2.down, groundRayLength, LayerMask.GetMask("WorldObj"));

    private void SetVelocity(Vector2 newVelocity) => rb.velocity = newVelocity;
    private void SetVelocityX(float X) => rb.velocity = new Vector2(X, rb.velocity.y);
    private void SetVelocityY(float Y) => rb.velocity = new Vector2(rb.velocity.x, Y);
}
