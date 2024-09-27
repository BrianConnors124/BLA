using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int numberOfRooms;
    public Vector2 previousDirection;

    public int maxRooms;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    public void AddRoom()
    {
        numberOfRooms++;
    }
}
