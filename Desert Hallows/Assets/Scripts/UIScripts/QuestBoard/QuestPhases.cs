using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestPhases : MonoBehaviour
{
    public List<GameObject> objectives;
    
    
    public GameObject[] activateOnCompletion;
    

    

    public void RemoveObjective(GameObject obj)
    {
        if(objectives.Contains(obj))
            objectives.Remove(obj);

        if (objectives.Count == 0)
            ObjectiveCompleted();
    }

    private void ObjectiveCompleted()
    {
        
        foreach (var obj in activateOnCompletion)
        {
            obj.SetActive(true);
        }
        
    }
}
