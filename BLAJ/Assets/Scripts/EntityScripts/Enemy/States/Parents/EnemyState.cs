using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : State<EnemyStateMachine.EEnemyState>
{
    public Enemy enemy;
    public Rigidbody2D rb;
    public UniversalTimer Timer;
    public string jumpKey;
    public EnemyState(EnemyStateMachine.EEnemyState key, Enemy entity) : base(key)
    {
        enemy = entity;
        rb = enemy.GetComponent<Rigidbody2D>();
        Timer = enemy.GetComponent<UniversalTimer>();
        jumpKey = "jumpCD";
        //InitiateTimer();
    }

    private void InitiateTimer()
    {
        Timer.SetTimer(jumpKey, .1f);
    }
    

    public override void EnterState()
    {
        base.EnterState();
        //Debug.Log(StateKey);
        enemy.Anim.Play(StateKey.ToString());
    }
    public override EnemyStateMachine.EEnemyState GetNextState()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        enemy.Anim.Play(StateKey.ToString());
    }
}
