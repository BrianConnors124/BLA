using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapSelect : MonoBehaviour
{
    public GameObject[] level;
    public bool _once;
    public GameObject background;
    

    
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
        GameManager.instance.AddLevel();
        background = GameObject.Find("Background");
        var backTrans = background.transform.localScale;
        level[mapNum].name = "Level: " + GameManager.instance.levelNum;
        Instantiate(level[mapNum], new Vector3(transform.position.x + (backTrans.x -0.01f) * 2, transform.position.y), Quaternion.identity);
        
    }
    
    
}
