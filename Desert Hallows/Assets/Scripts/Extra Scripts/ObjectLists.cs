using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = System.Object;

public class ObjectLists : MonoBehaviour
{
    public GameObject[] dontDestroy;
    public List<GameObject> damageNumbers;
    [SerializeField] private GameObject original;
    public List<GameObject> enemyProjectilesFromAir;

    private void Awake()
    {
        SceneManager.sceneLoaded += LoadedScene;
        
        foreach (var item in dontDestroy)
        {
            DontDestroyOnLoad(item);   
        }

        for (int i = 1; i < enemyProjectilesFromAir.Count; i++)
        {
            enemyProjectilesFromAir.Remove(enemyProjectilesFromAir[i]);
        }
    }

    private void LoadedScene(Scene scene, LoadSceneMode sceneMode)
    {
        if (scene.name.Equals("MainScene") || scene.name.Equals("Thank you"))
        {
            foreach (var item in dontDestroy)
            {
                Destroy(item);
            }
        }


        damageNumbers = new List<GameObject>() {original};
    }
}
