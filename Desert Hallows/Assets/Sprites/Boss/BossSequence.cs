using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossSequence : MonoBehaviour
{

    public GameObject[] enemies;
    public Animator boss_ac;
    public List<GameObject> aliveEnemies;
    private int sequence;
    private UniversalTimer timer;
    private float time;
    public GameObject beam;
    private bool attacking;

    private void Start()
    {
        aliveEnemies = new List<GameObject>();
        timer = GetComponent<UniversalTimer>();
        timer.SetActionTimer("Get New Attack ", 2, RandomAttack);
    }
    

    private void RandomAttack()
    {
        beam.SetActive(false);
        var a = Random.Range(0, 2);

        print(a);
        if (a == 0)
        {
            BlastBeam();
        }
        else if(a == 1)
        {
            SummonEnemies();
        }
        
    }
    private void BlastBeam()
    {
        sequence++;
        beam.SetActive(true);
        boss_ac.Play("attack");
        timer.SetActionTimer("Get New Attack " + sequence, 5, RandomAttack);
    }

    private void SummonEnemies()
    {
        sequence++;
        boss_ac.Play("attack");
        
        foreach (var enemy in enemies)
        {
            Instantiate(enemy);
        }
        timer.SetActionTimer("Get New Attack " + sequence, 10, RandomAttack);
    }
    
    
}
