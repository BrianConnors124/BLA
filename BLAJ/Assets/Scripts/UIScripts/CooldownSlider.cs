using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CooldownSlider : MonoBehaviour
{
    private UniversalTimer playerTimer;

    public GameObject player;
    private Player playerScript;
    
    public List<Image> image;
    private List<string> key;
    private float currentTimerValue;
    private Dictionary<string, float> playerTimerValues;
    private Dictionary<string, Image> images;

    private void Start()
    {
        playerScript = player.GetComponent<Player>();
        images = new Dictionary<string, Image>();
        
        while(!playerScript.doneLoading){}
        
        playerTimer = playerScript.GetUniversalTimer();
        playerTimerValues = playerScript.GetCoolDowns();
        key = playerScript.cooldownKey;
        
        for (var i = 0; i < image.Count; i++)
        {
            images.Add(key[i], image[i]);
        } 
    }

    private void Update()
    {
        if(playerScript.doneLoading && key != null) UpdateSlider();
    }


    private void UpdateSlider()
    {
        foreach (var a in key)
        {
            if (playerTimer.TimerActive(a))
            {
                currentTimerValue = playerTimer.GetTimerValue(a);
                
                images[a].fillAmount = currentTimerValue / playerTimerValues[a];
            }
        }
    }
}
