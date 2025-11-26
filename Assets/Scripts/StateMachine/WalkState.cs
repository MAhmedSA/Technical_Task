using UnityEngine;

public class WalkState : IState
{
    EnemyMovement enemy;
    Animator animator;
    public WalkState(EnemyMovement enemy)
    {
        this.enemy = enemy;
      animator = enemy.gameObject.GetComponent<Animator>(); 

    }
    public void Enter()
    {
        animator.SetBool("IsWalking", true);
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

    public void Exit() {
        animator.SetBool("IsWalking", false);
    }

}
