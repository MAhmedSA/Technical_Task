using UnityEngine;

public class IdleState : IState
{

    EnemyMovement enemy;
    Animator  animator;
    float idleTime = 1.5f;
    float timer = 0;
    public IdleState(EnemyMovement enemy)
    {
        this.enemy = enemy;
        animator = enemy.gameObject.GetComponent<Animator>();
    }
    public void Enter()
    {
        timer = 0;
        animator.SetBool("IsIdle", true);
      

    }

    public void Execute()
    {
        timer += Time.deltaTime;

        if (timer >= idleTime)
            enemy.stateMachine.TransitionTo(new WalkState(enemy));
    }

    public void Exit() {
        animator.SetBool("IsIdle", false);
    }

}
