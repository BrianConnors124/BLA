using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDone : MonoBehaviour
{
    public GameObject[] setActiveOnAnimDone;

    public bool animDone;

    private void Start()
    {
        foreach (var a in setActiveOnAnimDone)
        {
            a.SetActive(false);
        }
        gameObject.SetActive(false);
    }

    public void OnEnable()
    {
        animDone = false;
        StartCoroutine(OnAnimFinished());
    }

    public void OnDisable()
    {
        foreach (var a in setActiveOnAnimDone)
        {
            a.SetActive(false);
        }
    }

    private IEnumerator OnAnimFinished()
    {
        yield return new WaitUntil(() => animDone);
        foreach (var a in setActiveOnAnimDone)
        {
            a.SetActive(true);
        }
    }

    public void AnimFinished()
    {
        animDone = true;
    }
}