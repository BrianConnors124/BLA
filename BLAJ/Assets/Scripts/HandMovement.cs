using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMovement : MonoBehaviour
{
    public float change;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var input = InputSystemController.AimInput();
        var dir = Vector3.up * input.x + Vector3.left * input.y;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, dir);
    }
}
