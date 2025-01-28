using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxBackground : MonoBehaviour
{
    public GameObject[] spawnLocations;
    public GameObject[] background;

    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Background"))
        {
            other.gameObject.SetActive(false);
            print("delete");
            NeedNewBackground();
        }
        
    }

    private void NeedNewBackground()
    {
        foreach (var a in spawnLocations)
        {
            ObjectPuller.PullObject(background, a.transform.position);   
        }
        
    }
}
