using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapSelect : MonoBehaviour
{
    public GameObject[] level;
    public bool _once;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!_once)
        {
            if (other.CompareTag("Player"))
            {
                var mapNum = Random.Range(0, level.Length);
                MakeNewLevel(mapNum);
                _once = true;
            }
        }
    }

    private void MakeNewLevel(int mapNum)
    {
        Instantiate(level[mapNum], new Vector3(transform.position.x + transform.localScale.x -0.01f, transform.position.y), Quaternion.identity);
    }
    
    
}
