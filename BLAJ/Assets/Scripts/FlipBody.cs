using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipBody : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector2(Math.Abs(transform.localScale.x) * PlayerController.instance.direction, transform.localScale.y);
    }
}
