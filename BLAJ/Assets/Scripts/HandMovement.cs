using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMovement : MonoBehaviour
{
    // Update is called once per frame
    public bool armMovesWithMovement = false;
    
    
    
    void FixedUpdate()
    {
        if (!armMovesWithMovement)
        {
            var input = InputSystemController.AimInput();
            var dir = Vector3.up * input.x + Vector3.left * input.y;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, dir);   
        }
        else
        {
            var input = InputSystemController.MovementInput();
            var dir = Vector3.up * input.x + Vector3.left * input.y;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, dir);  
        }
    }
}
