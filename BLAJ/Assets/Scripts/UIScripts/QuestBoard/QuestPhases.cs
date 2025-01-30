using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPhases : MonoBehaviour
{
    private Action phase1, phase2;
    public bool phase1T, phase2T;

    [Header("PhaseOne")]
    //kill all of the mobs in the room
    public List<GameObject> mobs;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
}
