using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public float GetTimerValue(string code) => timer[code];

    // public bool TimerDone(string a) => previousKey.Any(t => t == a);
    public bool TimerDone(string a) => !key.Contains(a);
    
   

    public void SetActionTimer(string code, float length, Action commit)
    {
        if(!key.Contains(code)) key.Add(code);
        action.TryAdd(code, commit);
        timer.TryAdd(code, length);
        timer[code] = length;
    }

    public bool TimerActive(string code)
    {
        return key.Contains(code);
    }

    public void RemoveActionTimer(string code)
    {
        timer.Remove(code);
        action.Remove(code);
        key.Remove(code);
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
        { timer[key[i]] -= Time.deltaTime;
            if(timer[key[i]] <= 0){
                if(action.ContainsKey(key[i]))
                {
                   action[key[i]].Invoke();
                    action.Remove(key[i]);
                }  
                timer.Remove(key[i]);  
                key.RemoveAt(i);
                i--;                   
            }    
        }   
    }
    
 
}
