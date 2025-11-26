using UnityEngine;

public class IdleState : IState
{

    EnemyMovement enemy;

    float idleTime = 1.5f;
    float timer = 0;
    public IdleState(EnemyMovement enemy)
    {
        this.enemy = enemy;
    }
    public void Enter()
    {
        timer = 0;
        
    }

    public void Execute()
    {
        timer += Time.deltaTime;

        if (timer >= idleTime)
            enemy.stateMachine.TransitionTo(new WalkState(enemy));
    }

    public void Exit() { }

}
