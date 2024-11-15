using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    // Update is called once per frame
    
    public Vector3 dir;
    public Vector3 dir2;
    public static HandScript instance;

    private void Start()
    {
        instance = this;
    }


    void FixedUpdate()
    {
        if (!PlayerController.instance.armMovesWithMovement)
        {
            if (!InputSystemController.AimInput().Equals(new Vector3(0, 0, 0)))
            {
                var input = InputSystemController.AimInput();
                dir2 = Vector3.up * input.x + Vector3.left * input.y;
                transform.rotation = Quaternion.LookRotation(Vector3.forward, dir2);     
            }
        }
        else
        {
            if (!InputSystemController.MovementInput().Equals(new Vector3(0, 0, 0)))
            {
                var input = InputSystemController.MovementInput();
                dir = Vector3.up * input.x + Vector3.left * input.y;
                transform.rotation = Quaternion.LookRotation(Vector3.forward, dir);     
            }
        }
    }
}
