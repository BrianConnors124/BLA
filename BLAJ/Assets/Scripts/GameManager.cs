using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int numberOfRooms;
    private bool collapser = true;

    

    [Header("Random Level Gen")]
    [SerializeField] private Transform thisTransform;
    [SerializeField] private Vector3 _location;
    [SerializeField] private Vector2 _direction;
    [SerializeField] private GameObject[] platform;
    public Action instOBJ;
    
    public int maxRooms;
    public Vector2 previousDirection;
    public Vector2 oppositeDirection;
    private BoxCollider2D thisCol;
    public bool redo;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        instOBJ += InstantiateObj;
        instOBJ += PrintStatement;
    }

    // Update is called once per frame
    public void AddRoom()
    {
        numberOfRooms++;
    }

    public void Failed(Collider2D other, Collider2D thisColl)
    {
        
        var ogGO = other.gameObject;
        var secGO = thisColl.gameObject;
        Destroy(secGO);
        StartCoroutine(new UniversalTimer().Timer(1f, instOBJ));
        Debug.Log(secGO.name + " is intersecting " + ogGO.name);
    }

    private void PrintStatement()
    {
        Debug.Log("retry");
    }

    public void SetValues(Vector2 a, GameObject[] b, BoxCollider2D c, Transform d)
    {
            _direction = a;
            platform = b;
            thisCol = c;
            thisTransform = d;
    }

    public void InstantiateObj()
    {
        _location = thisTransform.position;
        if (numberOfRooms < maxRooms)
        { 
            var randObj = Random.Range(0, platform.Length);
            
            
            while (_direction.x == 0 && _direction.y == 0 && previousDirection.x == _direction.x || _direction.x == 0 && _direction.y == 0 && previousDirection.y == _direction.y || _direction.x == 0 && _direction.y == 0 && oppositeDirection.y == _direction.y)
            {
           
                var rand = Random.Range(0, 2);

                if (rand == 0)
                {
                    var y = Random.Range(-1, 2);
                    _direction = new Vector2(0, y); 
                    //Debug.Log("rand = 0");   
                }

                if (rand == 1)
                {
                    _direction = new Vector2(1, 0); 
                    //Debug.Log("rand = 1");
                }
            }

            previousDirection = new Vector2(_direction.x, _direction.y);
            oppositeDirection = new Vector2(_direction.x * -1, _direction.y * -1);
            
            BoxCollider2D platformCol = platform[randObj].GetComponent<BoxCollider2D>();
            var thisScale = thisTransform.localScale;
            var thisColSize = thisCol.size;
            var nextColSize = platformCol.size;
            var scaleSize = platform[randObj].transform.localScale;
            if (collapser)
            {
                Instantiate(platform[randObj], new Vector2 ((_location.x + _direction.x * (nextColSize.x + 0.01f) * scaleSize.x / 2) + ((thisColSize.x + 0.01f) * thisScale.x * _direction.x /2),(_location.y + _direction.y * (nextColSize.y + 0.01f) * scaleSize.y / 2) + ((thisColSize.y + 0.01f) * thisScale.y * _direction.y /2)), Quaternion.identity);
            }//instantiating obj
            // yield return new WaitWhile();
        }
        
    }
    
}
