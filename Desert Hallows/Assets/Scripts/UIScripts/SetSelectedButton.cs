using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SetSelectedButton : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject setObj;

    private void Start()
    {
        SceneManager.sceneLoaded += LoadedScene;
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }

    private void OnEnable()
    {
        eventSystem.SetSelectedGameObject(setObj);
    }

    private void LoadedScene(Scene scene, LoadSceneMode sceneMode)
    {
        Time.timeScale = 1;
    }
}
