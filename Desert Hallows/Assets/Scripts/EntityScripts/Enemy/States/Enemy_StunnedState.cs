

public class Enemy_StunnedState : EnemyState
{
    public Enemy_StunnedState(EnemyStateMachine.EEnemyState key, Enemy entity) : base(key, entity)
    {
        
    }


    public override void EnterState()
    {
        stateTimer = enemy.recentStun;
        enemy.recentStun = 0;
        base.EnterState();
        enemy.ZeroVelocity();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }
    

    public override EnemyStateMachine.EEnemyState GetNextState()
    {
        if (enemy.dead) return EnemyStateMachine.EEnemyState.dying;
        if (enemy.takingDamage) return EnemyStateMachine.EEnemyState.takingDamage;
        if (StateTimerDone() && enemy.canBeStunned) return EnemyStateMachine.EEnemyState.retreat;
        return StateKey;

    }
    
    public override void ExitState()
    {
        base.ExitState();
        
    }
}
