using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxBackground : MonoBehaviour
{
    private GameObject player;
    public List<GameObject> backGroundpt2;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Background"))
        {
            NeedNewBackground(other);
            print("delete");
        }
        
    }

    private void NeedNewBackground(Collider2D other)
    {
        ObjectPuller.PullObject(backGroundpt2, new Vector2(transform.parent.position.x + (player.GetComponent<Player>().MovementDirection() * (backGroundpt2[0].GetComponent<BoxCollider2D>().size.x + 14.8f + backGroundpt2[0].transform.localScale.x)), transform.position.y), other.gameObject);
    }
}
