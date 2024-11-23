using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class UniversalTimer
{
    public float timer;
    private float timerLength;

    public bool TimerDone => timer <= 0;


    public void Reset()
    {
        timer = 0;
    }
    public void Reset(float Length)
    {
        timer = Length;
    }
    public IEnumerator Timer(float Length, Action Commit)
    {
        timer = Length; 
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Commit.Invoke();
    }
    
    public IEnumerator Timer(float Length)
    {
        timer = Length; 
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

 
}
