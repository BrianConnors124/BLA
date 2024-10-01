using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    private bool _once = false;
    private UniversalTimer loading;
    private Action endLoading;

    private void Update()
    {
        if(endLoading == null)
            endLoading = GameManager.instance.DeactivateLoadingScreen;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (!_once)
        { 
            if (other.CompareTag("Player"))
            {
                GameManager.instance.ActivateLoadingScreen();
                StartCoroutine(LoadingNewLevel(other));
            }
        }
        
    }

    private IEnumerator LoadingNewLevel(Collider2D other)
    {
        yield return new WaitForSeconds(0.5f);
        other.transform.position = transform.position;
        _once = true;
        yield return new WaitForSeconds(2);
        GameManager.instance.DeactivateLoadingScreen();
    }
}
