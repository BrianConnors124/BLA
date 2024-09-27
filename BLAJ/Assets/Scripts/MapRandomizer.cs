
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapRandomizer : MonoBehaviour
{
    [Header("Location And Direction")] 
    [SerializeField] private Vector2 _direction;

    [Header("GameObject Inst")] 
    [SerializeField] private GameObject[] platform;
    private Action redoInst;
    private Action _instantiate;
    private BoxCollider2D thisCol;
    private Transform thisTransform;
    private bool canPlace = false;
    private UniversalTimer timer = new UniversalTimer();
    private bool placed = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!timer.TimerDone)
        {
            GameManager.instance.Failed(other);
        }
    }
    
    private void Start()
    {
        thisCol = GetComponent<BoxCollider2D>(); 
        _direction = new Vector2(0, 0);
        thisTransform = transform;
        redoInst += ActionInstantiate;

        StartCoroutine(timer.Timer(.1f));
        StartCoroutine(new UniversalTimer().Timer(1, redoInst));
        
    }

    public void ActionInstantiate()
    {
        GameManager.instance.SetValues(_direction, platform, thisCol, thisTransform);
        _instantiate += GameManager.instance.InstantiateObj;
        _instantiate.Invoke();
    }
}
