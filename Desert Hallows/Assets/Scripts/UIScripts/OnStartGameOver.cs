using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OnStartGameOver : MonoBehaviour
{
    public PlayerInput input;

    private void Start()
    {
        input = GameObject.Find("Player").GetComponent<PlayerInput>();
        input.SwitchCurrentActionMap("End Game");
    }

    private void OnDisable()
    {
        input.SwitchCurrentActionMap("Movement");
        Time.timeScale = 1;
    }
}
