using System;
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
    private float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        newPosition = player.transform.position.x;
        previousPosition = newPosition;
        timer = .3f;
    }

    // Update is called once per frame

    private void OnEnable()
    {
        timer = .3f;
    }

    private void Update()
    {
        if(timer > 0)timer -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        if (!(timer <= 0)) return;
        newPosition = player.transform.position.x;
        //transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        var playerMovement = newPosition - previousPosition;
        //rb.velocity = new Vector2(playerMovement * -speed, 0);
        previousPosition = newPosition;
    }
}
