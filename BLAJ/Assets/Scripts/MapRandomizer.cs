using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRandomizer : MonoBehaviour
{
    public GameObject[] room;
    [SerializeField] private int rand;
    
    // Start is called before the first frame update
    void Start()
    {
        rand = Random.Range(0, room.Length);
        InstantiateLevel();
    }

    private void InstantiateLevel()
    {
        for (var i = 1; i <= 4; i++)
        {
            var mod = 4 / i;
            
            Instantiate(room[rand], new Vector2(transform.position.x + mod * 2, transform.position.y + mod * 2), Quaternion.identity);
            
            rand = Random.Range(0, room.Length);
        }
    }
}
