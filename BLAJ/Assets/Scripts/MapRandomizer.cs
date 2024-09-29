
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private Action addRoomSuccess;
    private BoxCollider2D thisCol;
    private Transform thisTransform;
    private bool canPlace = false;
    private UniversalTimer timer = new UniversalTimer();
    private bool placed = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!placed)
        {
            thisCol = GetComponent<BoxCollider2D>(); 
            GameManager.instance.Failed(other, thisCol);
        }
    }
    
    private void Start()
    {
        thisCol = GetComponent<BoxCollider2D>(); 
        _direction = new Vector2(0, 0);
        thisTransform = transform;
        _instantiate += ActionInstantiate;
        addRoomSuccess += AddRoom;

        StartCoroutine(timer.Timer(.02f, addRoomSuccess));
        StartCoroutine(new UniversalTimer().Timer(1f, _instantiate));
        
    }

    
    public void AddRoom()
    {
        placed = true;
        GameManager.instance.AddRoom();
    }

    
    

    public void ActionInstantiate()
    {
        GameManager.instance.SetValues(_direction, platform, thisCol, thisTransform);
        _instantiate = GameManager.instance.InstantiateObj;
        _instantiate.Invoke();
    }
}
