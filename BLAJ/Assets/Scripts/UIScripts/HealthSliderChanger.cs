using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthSliderChanger : MonoBehaviour
{
    public Image[] images;
    public GameObject player;
    public TextMeshProUGUI healthMeter;
    private Player playerController;

    private float previousHp;

    private void Awake()
    {
        playerController = player.GetComponent<Player>();
    }

    private void Update()
    {
        foreach(var a in images)
        {
            a.fillAmount = playerController.health/playerController.maxHealth;   
        }
        previousHp = playerController.health;
    }
}
