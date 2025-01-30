using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class DamageNumberList : MonoBehaviour
{
    public GameObject[] dontDestroy;
    public GameObject[] damageNumbers;
    public List<GameObject> enemyProjectilesFromAir;

    private void Awake()
    {
        foreach (var item in dontDestroy)
        {
            DontDestroyOnLoad(item);   
        }
    }
}
