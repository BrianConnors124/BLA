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
    private UniversalTimer timer;
    private float time;
    public GameObject beam;

    private void Start()
    {
        aliveEnemies = new List<GameObject>();
        timer = GetComponent<UniversalTimer>();
        timer.SetActionTimer("Waiting for new Attack", 1, RandomAttack);
    }

    private void Update()
    {
        time -= Time.deltaTime;
    }

    private void BlastBeam()
    {
        beam.SetActive(true);
        boss_ac.Play("attack");
        timer.SetActionTimer("Waiting for new Attack", 10, RandomAttack);
    }

    private void RandomAttack()
    {
        beam.SetActive(false);
        var a = Random.Range(0, 3);

        if (a == 0)
        {
            BlastBeam();
        }
        else if(a == 1)
        {
            SummonEnemies();
        }
        else
        {
            Idle();
        }
    }

    private void Idle()
    {
        timer.SetActionTimer("Waiting for new Attack", 20, RandomAttack);
    }

    private void SummonEnemies()
    {
        boss_ac.Play("attack");
        foreach (var enemy in enemies)
        {
            Instantiate(enemy);
        }
        timer.SetActionTimer("Waiting for new Attack", 45, RandomAttack);
    }
    
    
}
