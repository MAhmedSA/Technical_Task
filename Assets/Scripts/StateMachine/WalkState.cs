using UnityEngine;

public class WalkState : IState
{
    EnemyMovement enemy;

    public WalkState(EnemyMovement enemy)
    {
        this.enemy = enemy;
    }
    public void Enter()
    {
      
    }

    public void Execute()
    {
        enemy.RotateTowardPlayer();

        if (enemy.IsPlayerClose())
        {
            enemy.stateMachine.TransitionTo(new AttackState(enemy));
            return;
        }

        enemy.Move();
    }

    public void Exit() { }

}
