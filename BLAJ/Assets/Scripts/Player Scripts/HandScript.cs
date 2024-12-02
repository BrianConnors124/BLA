using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    // Update is called once per frame
    
    public Vector3 dir;
    
    public static HandScript instance;

    private void Start()
    {
        instance = this;
        var input = InputSystemController.MovementInput(); 
        dir = Vector3.up * input.x + Vector3.left * input.y; 
        transform.rotation = Quaternion.LookRotation(Vector3.forward, new Vector3(0, PlayerController.instance.direction, 0));
         
    }


    void FixedUpdate()
    {
        if (!InputSystemController.MovementInput().Equals(new Vector3(0, 0, 0)))
        { 
            var input = InputSystemController.MovementInput(); 
            dir = Vector3.up * input.x + Vector3.left * input.y; 
            transform.rotation = Quaternion.LookRotation(Vector3.forward, new Vector3(0, PlayerController.instance.direction, 0));  
        }
        
    }
}
