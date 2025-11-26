using UnityEngine;

public class DeathState : IState
{
    EnemyMovement enemy;
    Animator animator;
    public DeathState(EnemyMovement enemy)
    {
        this.enemy = enemy;
        animator = enemy.gameObject.GetComponent<Animator>();
    }
    public void Enter()
    {
        animator.SetBool("IsDeath", true);
    }

    public void Execute()
    {
       
    }

    public void Exit()
    {
        animator.SetBool("IsDeath", false);
    }

}
