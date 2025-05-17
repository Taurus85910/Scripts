using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f; 
    [SerializeField] private float attackRange = 2f; 
    [SerializeField] private float chaseRange = 10f; 
    [SerializeField] private float wanderRadius = 5f; 
    [SerializeField] private float wanderInterval = 3f; 
    [SerializeField] private float idleTime = 2f;
    [SerializeField] private LayerMask groundLayer; 
    [SerializeField] private EnemyAttackHandler attackHandler;
    [SerializeField] private  float attackCooldown = 2f;
    
    private Transform player; 
    private Vector3 spawnPosition;
    private Vector3 wanderTarget; 
    private float lastWanderTime; 
    private bool isIdle = false; 
    private Animator animator; 
    private float lastAttackTime = 0f;

    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spawnPosition = GetGroundPosition(transform.position); 
        animator = GetComponent<Animator>();
        SetNewWanderTarget(); 
        
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        float distanceToSpawn = Vector3.Distance(transform.position, spawnPosition);

        if (distanceToPlayer <= chaseRange)
        {
            if (distanceToPlayer <= attackRange)
            {
                AttackPlayer();
            }
            else
            {
                ChasePlayer();
            }
        }
        else if (distanceToSpawn > wanderRadius)
        {
            ReturnToSpawn();
        }
        else
        {
            Wander();
        }
        
        UpdateAnimations();
    }

    void ChasePlayer()
    {
        isIdle = false;
        Vector3 targetPosition = GetGroundPosition(player.position); 
        MoveTo(targetPosition);
        LookAtTarget(targetPosition); 
    }

    void AttackPlayer()
    {
       
        if (Time.time - lastAttackTime < attackCooldown)
            return;
        
        isIdle = true;
        StartCoroutine(attackHandler.PerformAttack());
        lastAttackTime = Time.time;
        LookAtTarget(player.position); 
    }

    void ReturnToSpawn()
    {
        isIdle = false;
        MoveTo(spawnPosition);
        LookAtTarget(spawnPosition); 
        
        if (Vector3.Distance(transform.position, spawnPosition) < 0.5f)
        {
            SetNewWanderTarget();
        }
    }

    void Wander()
    {
        if (Vector3.Distance(transform.position, wanderTarget) < 0.5f || 
            Time.time - lastWanderTime > wanderInterval)
        {
            if (!isIdle) 
            {
                isIdle = true;
                lastWanderTime = Time.time;
            }
            else if (Time.time - lastWanderTime > idleTime)
            {
                isIdle = false;
                SetNewWanderTarget();
            }
        }

        if (!isIdle)
        {
            MoveTo(wanderTarget);
            LookAtTarget(wanderTarget); 
        }
    }

    void SetNewWanderTarget()
    {
        Vector3 randomPoint = spawnPosition + Random.insideUnitSphere
            * wanderRadius;
        randomPoint.y = spawnPosition.y; 
        wanderTarget = GetGroundPosition(randomPoint);
        lastWanderTime = Time.time; 
    }

    void MoveTo(Vector3 targetPosition)
    {
        
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.y = 0; 
        transform.position += direction * (moveSpeed * Time.deltaTime);
        
        transform.position = GetGroundPosition(transform.position);
    }

    void LookAtTarget(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        direction.y = 0; 
        
        direction = -direction;

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
        
        
    }

    Vector3 GetGroundPosition(Vector3 position)
    {
        RaycastHit hit;
        if (Physics.Raycast(position + Vector3.up * 10, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            return hit.point;
        }
        return position; 
    }

    void UpdateAnimations()
    {
        animator.SetBool("IsWalking", !isIdle); 
    }
}