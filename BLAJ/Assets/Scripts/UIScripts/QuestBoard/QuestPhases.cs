using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestPhases : MonoBehaviour
{
    private Action phase1, phase2;
    public bool phase1T, phase2T;
    public TextMeshProUGUI text;
    public Scene currentScene;

    [Header("PhaseOne")]
    //kill all of the mobs in the room
    public List<GameObject> mobs;

    public Player player;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        player = GameObject.Find("Player").GetComponent<Player>();
        foreach (var mob in mobs)
        {
            mob.GetComponent<Enemy>().onDeath += RemoveEnemyOnDeath;
        }
        UpdateQuestBoard();
    }

    private void RemoveEnemyOnDeath()
    {
        for (int i = 0; i < mobs.Count; i++)
        {
            if (!mobs[i])
            {
                mobs.Remove(mobs[i]);
            }
        }
        UpdateQuestBoard();
        
    }

    private void UpdateQuestBoard()
    {
        if (currentScene.name.Equals("Phase1"))
        {
            if (phase1T)
            {
                if (mobs.Count <= 0) phase1T = false;
                text.SetText("Kill x" + mobs.Count + " mummy");
            }

            if (!phase1T)
            {
                player.hasDash = true;
                text.SetText("Quests Completed");
            }
        }
        else
        {
            if (phase2T)
            {
                if (mobs.Count <= 0) phase2T = false;
                text.SetText("Kill x" + mobs.Count + " mummy");
            }
            if (!phase2T)
            {
                player.hasDashAttack = true;
                text.SetText("Quests Completed");
            }   
        }
    }
}
