
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRandomizer : MonoBehaviour
{
    [Header("Location And Direction")] 
    [SerializeField] private Vector3 _location;
    [SerializeField] private Vector2 _direction;

    [Header("GameObject Inst")] 
    [SerializeField] private GameObject[] platform;

    private bool a = true;

    private BoxCollider2D thisCol;
    


    private void Start()
    {
        StartCoroutine(InstantiateObj());
    }

    private IEnumerator InstantiateObj()
    {
        yield return new WaitForSeconds(0.5f);
        if (GameManager.instance.numberOfRooms < GameManager.instance.maxRooms)
        { 
            thisCol = GetComponent<BoxCollider2D>(); 
            _direction = new Vector2(0, 0);
            var rand = Random.Range(0, platform.Length);
            _location = transform.position;
            RandomXorY();
            BoxCollider2D platformCol = platform[rand].GetComponent<BoxCollider2D>();
            var thisScale = transform.localScale;
            var thisColSize = thisCol.size;
            var nextColSize = platformCol.size;
            var scaleSize = platform[rand].transform.localScale;
            if (a)
            {
                Instantiate(platform[rand], new Vector2 ((_location.x + _direction.x * nextColSize.x * scaleSize.x / 2) + (thisColSize.x * thisScale.x * _direction.x /2),(_location.y + _direction.y * nextColSize.y * scaleSize.y / 2) + (thisColSize.y * thisScale.y * _direction.y /2)), Quaternion.identity);
            }//instantiating obj
            GameManager.instance.AddRoom();
        }
    }

    private void RandomXorY()
    {
        while (_direction.x == 0 && _direction.y == 0 && GameManager.instance.previousDirection.x == _direction.x || GameManager.instance.previousDirection.y == _direction.y)
        {
           
            var rand = Random.Range(0, 2);

            if (rand == 0)
            {
                var x = Random.Range(-1, 2); 
                _direction = new Vector2(x, 0); 
                Debug.Log("rand = 0");   
            }

            if (rand == 1)
            {
                var y = Random.Range(-1, 2);
                _direction = new Vector2(0, y); 
                Debug.Log("rand = 1");
            }
        }
        GameManager.instance.previousDirection = new Vector2(_direction.x * -1, _direction.y * -1);
    }
}
