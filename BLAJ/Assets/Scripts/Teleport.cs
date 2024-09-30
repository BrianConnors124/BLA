using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private bool _once;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (_once)
        {
            if (other.CompareTag("Player"))
            {

                other.transform.position = transform.position;

            }
        }
    }
}
