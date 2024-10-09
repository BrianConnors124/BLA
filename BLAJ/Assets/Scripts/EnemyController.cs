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
                    transform.position = _origPos;
                    checkingPos = false;
                }
            } else
            {
                if (transform.position.x >= _origPos.x)
                {
                    rb.velocity = new Vector2(0, 0);
                    transform.position = _origPos;
                    checkingPos = false;
                }
            }
        }
        else if(transform.position.x != _origPos.x)
        {
            greaterThan = transform.position.x > _origPos.x;
        }
        startingPoint = new Vector2(transform.position.x, transform.position.y + transform.localScale.x + 1 );
        obstaclePos = new Vector2(transform.position.x, transform.position.y + transform.localScale.x);
        bool isJump = Physics2D.Raycast(startingPoint,Vector2.right, lengthOfRay, LayerMask.GetMask("WorldObj"));
        bool isObstacle = Physics2D.Raycast(obstaclePos,Vector2.right, lengthOfRay, LayerMask.GetMask("WorldObj"));
        if (!isJump && isObstacle)
        {
            print(isJump);
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(startingPoint, new Vector3(startingPoint.x + lengthOfRay, startingPoint.y));
        Gizmos.DrawLine(obstaclePos, new Vector3(obstaclePos.x + lengthOfRay, obstaclePos.y));
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            rb.velocity = new Vector2(0, 0);
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
        } else if (Direction.x < 0)
        {
            rb.velocity = new Vector2(-1 * npcMovementSpeed * Time.timeScale,  rb.velocity.y);
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
            } else if (Direction.x <= -1.08)
            {
                rb.velocity = new Vector2(npcMovementSpeed * Time.timeScale * -1, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(0, 0);
            }
                
            //print(Direction);
        }
        
    }

    private bool isTouchingGround() => Physics2D.Raycast(transform.position, Vector2.down);

    private void SetVelocity(Vector2 newVelocity) => rb.velocity = newVelocity;
    private void SetVelocityX(float X) => rb.velocity = new Vector2(X, rb.velocity.y);
    private void SetVelocityY(float Y) => rb.velocity = new Vector2(rb.velocity.x, Y);
}
