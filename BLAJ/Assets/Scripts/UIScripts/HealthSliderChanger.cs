using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSliderChanger : MonoBehaviour
{
    public Image main;
    public GameObject player;
    private Player playerController;

    private float previousHp;

    private void Awake()
    {
        playerController = player.GetComponent<Player>();
    }

    private void Update()
    {
        
        main.fillAmount = playerController.health/playerController.maxHealth;
        previousHp = playerController.health;
    }
}
