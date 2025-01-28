using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject player;
    public float speed;
    public float previousPosition, newPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        newPosition = player.transform.position.x;
        previousPosition = newPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        newPosition = player.transform.position.x;

        var playerMovement = newPosition - previousPosition;
        rb.velocity = new Vector2(playerMovement * -speed, 0);
        previousPosition = newPosition;
    }
}
