using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestPhases : MonoBehaviour
{
    private int maxCount;
    public List<GameObject> objectives;
    public string currentObjective;
    public TextMeshProUGUI questText;

    private bool questComplete;
    public GameObject[] activateOnCompletion;


    private Player player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        maxCount = objectives.Count;
    }

    private void FixedUpdate()
    {
        if(!questComplete)questText.SetText(currentObjective + ": " + (maxCount - objectives.Count) + "/" + maxCount);
    }

    public void RemoveObjective(GameObject obj)
    {
        if(objectives.Contains(obj))
            objectives.Remove(obj);

        if (objectives.Count == 0)
            ObjectiveCompleted();
    }

    private void ObjectiveCompleted()
    {
        for (int i = 0; i < player.unlockables.Length; i++)
        {
            if (!player.unlockables[i])
            {
                player.unlockables[i] = true;
                break;
            }
        }

        questComplete = true;
        foreach (var obj in activateOnCompletion)
        {
            obj.SetActive(true);
        }
        
        questText.SetText("Quest Completed!");
    }
}
