using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerFix : MonoBehaviour
{
    public GameObject gameManager;

    private void Start()
    {
        DestroyImmediate(gameManager);
        Instantiate(gameManager);
    }
}
