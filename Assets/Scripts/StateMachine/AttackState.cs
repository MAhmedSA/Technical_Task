using UnityEngine;

public class AttackState :IState
{
    EnemyMovement enemy;
    float attackCooldown = 1f;
    float timer = 0;

    public AttackState(EnemyMovement enemy)
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

        // if player moves away , chase again
        if (!enemy.IsPlayerClose())
        {
            enemy.stateMachine.TransitionTo(new WalkState(enemy));
            return;
        }

        // Attack every second here
        if (timer >= attackCooldown)
        {
           
            timer = 0;
        }
    }

    public void Exit() { }
}
