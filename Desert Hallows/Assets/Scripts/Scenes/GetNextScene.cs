using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetNextScene : MonoBehaviour
{
    public string sceneName;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))GetScene();
    }

    public void GetScene()
    {
        SceneManager.LoadScene(sceneName);
        gameObject.SetActive(false);
    }
}
