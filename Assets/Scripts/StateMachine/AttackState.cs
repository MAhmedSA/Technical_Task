using UnityEngine;

public class AttackState :IState
{
    EnemyMovement enemy;
    float attackCooldown = 1f;
    float timer = 0;
    Animator animator;
    public AttackState(EnemyMovement enemy)
    {
        this.enemy = enemy;
        animator = enemy.gameObject.GetComponent<Animator>();
    }

    public void Enter()
    {
        animator.SetBool("IsAttacking", true);
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

    public void Exit() {
        animator.SetBool("IsAttacking", false);
    }
}
