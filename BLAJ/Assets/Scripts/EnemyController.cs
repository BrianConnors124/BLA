using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject _player;
    private Vector3 playerDirection;
    private Vector2 _origPos;
    private Rigidbody2D rb;
    private bool checkingPos = false;
    private bool greaterThan;
    
    [Header("Movement Speed")]
    [SerializeField] private float npcMovementSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _origPos = transform.position;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        rb.velocity = new Vector2(0, 0);
        Invoke("Return", .5f);
        //print(Direction);
    }

    private void Return()
    {
        var Direction = new Vector2(_origPos.x - transform.position.x, transform.position.y).normalized;
        if (Direction.x > 0)
        {
            rb.velocity = new Vector2(Time.deltaTime * npcMovementSpeed * 800, Direction.y);
        } else if (Direction.x < 0)
        {
            rb.velocity = new Vector2(-1 * Time.deltaTime * npcMovementSpeed * 800, Direction.y);
        }
        
        checkingPos = true;
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
            if (Direction.x > 0)
            {
                Direction.x = 1;   
            } else if (Direction.x < 0)
            {
                Direction.x = -1;
            }
            
            if (Direction.x >= 0.08 || Direction.x <= -0.08)
            {
                rb.velocity = new Vector2(Direction.x * Time.deltaTime * npcMovementSpeed * 100, Direction.y);
            }
            else
            {
                rb.velocity = new Vector2(0, 0);
            }
                
            //print(Direction);
        }
        
    }
}
