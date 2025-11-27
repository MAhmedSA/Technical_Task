using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("movement Section")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeed = 3f;

    [Header("Reference to player")]
    [SerializeField] Transform playerTransform;


    public StateMachine stateMachine;
    bool isMove;

    float angleView = 15;
    float sqrAttackDistance = 9f;
    public bool IsMove { get => isMove; set => isMove = value; }
    private void Awake()
    {
        playerTransform = FindObjectOfType<Camera>().transform;
        stateMachine = new StateMachine();
    }
    private void Start()
    {
        stateMachine.Initialize(new IdleState(this));  // start in idle
        StartCoroutine(EnableMove());
    }
    private void FixedUpdate()
    {
        stateMachine.Execute();
        if (!IsPlayerClose())
        {
                RotateTowardPlayer();
                Move();
            
        }
    }
    

    public void Move() {
      
        transform.Translate(Vector3.forward * moveSpeed * Time.fixedDeltaTime);
    }

    public void RotateTowardPlayer()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;

        
        direction.y = 0;

        // If enemy already facing target , no need to rotate
        if (direction == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Smooth Rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
    }
    public IEnumerator EnableMove()
    {
        yield return new WaitForSeconds(2f);
        isMove = true;
    }

    public bool IsPlayerClose()
    {
        return Vector3.SqrMagnitude(transform.position- playerTransform.position) < sqrAttackDistance;
    }

    public void Dead() {
        stateMachine.Initialize(new DeathState(this));  
    }
    private void OnDisable()
    {
        stateMachine.Initialize(new IdleState(this));  
    }

}
