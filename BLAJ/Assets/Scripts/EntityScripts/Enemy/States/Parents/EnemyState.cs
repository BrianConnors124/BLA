using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class EnemyState : State<EnemyStateMachine.EEnemyState>
{
    protected Enemy enemy;
    protected Rigidbody2D rb;
    protected UniversalTimer Timer;
    protected string jumpKey;
    protected string code = "Player Lost";
    
    
    protected EnemyStateMachine enemyStateMachine;
    public EnemyState(EnemyStateMachine.EEnemyState key, Enemy entity) : base(key)
    {
        enemy = entity;
        rb = enemy.GetComponent<Rigidbody2D>();
        Timer = enemy.GetComponent<UniversalTimer>();
        jumpKey = "jumpCD";
        enemyStateMachine = enemy.GetComponent<EnemyStateMachine>();
    }
    

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log(StateKey);
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

    public override void DoAttack()
    {
        
        var a = BoxCastDrawer.BoxCastAndDraw(new Vector2(enemy.transform.position.x +( enemy.reach * enemy.MovementDirection()), enemy.transform.position.y),new Vector2(enemy.transform.localScale.x,enemy.transform.localScale.y), 0, new Vector2(enemy.MovementDirection(), 0),0, LayerMask.GetMask("Player"), 0.3f);
        if(a) a.collider.GetComponent<Player>().ReceiveDamage(enemy.damage, enemy.knockBack,enemy.stun, enemy.MovementDirection());
    }

    public override void FacePlayer()
    {
        enemy.PlayerDirection();
    }
    
    
}
