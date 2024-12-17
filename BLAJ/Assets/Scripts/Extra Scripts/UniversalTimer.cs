using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class UniversalTimer : MonoBehaviour
{
    public List<string> key;
    private Dictionary<string, float> timer;
    private Dictionary<string, Action> action;
    
    private float timerLength;

    public void Start()
    {
        key = new List<string>();
        timer = new Dictionary<string, float>();
        action = new Dictionary<string, Action>();
    }

    public bool TimerDone(string a) => timer[a] < 0;
    
   

    public void SetActionTimer(string code, float length,Action commit)
    {
        if(!key.Contains(code)) key.Add(code);
        action.TryAdd(code, commit);
        timer.TryAdd(code, length);
        timer[code] = length;
    }
    
    public void SetTimer(string code, float length)
    {
        if(!key.Contains(code)) key.Add(code);
        timer.TryAdd(code, length);
        timer[code] = length;
    }
    
    
    private void Update()
    {
        for (int i = 0; i < timer.Count; i++)
        {
        if(timer[key[i]] > 0){
            timer[key[i]] -= Time.deltaTime;
        } else if (action.ContainsKey(key[i]))
            {
                action[key[i]].Invoke();
            }
            
        }
        
    }
    
 
}
