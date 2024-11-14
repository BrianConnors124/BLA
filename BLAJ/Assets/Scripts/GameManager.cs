using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int levelNum;
    private GameObject loadingScreen;
    public bool armAttached = false;
    
    private void Start()
    {
        instance = this;
        
        
        if(loadingScreen == null)
            loadingScreen = GameObject.Find("Loading Screen");
        if (loadingScreen == null)
        {
            Debug.LogError("There is no loading screen rename the prefered loading screen to \"Loading Screen\" ");
        }
        else
        {
            Debug.Log("Loading Screen found");
            DeactivateLoadingScreen();
        }
    }

    public void AddLevel()
    {
        levelNum++;
    }

    public void ActivateLoadingScreen()
    {
        loadingScreen.SetActive(true);
        
    }

    public void DeactivateLoadingScreen()
    {
        loadingScreen.SetActive(false);
        if (armAttached == false)
        {
            GameObject.Find("Shoulder").SetActive(false);
            GameObject.Find("ShoulderFreeMove").SetActive(true);
        }
        else
        {
            GameObject.Find("Shoulder").SetActive(true);
            GameObject.Find("ShoulderFreeMove").SetActive(false);
        }
    }
    
    
}
