using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class AIController : MonoBehaviour
{
    [Header("Navigation Settings")]
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float chaseSpeed = 5f;
    [SerializeField] private float patrolSpeed = 2f;
    [SerializeField] private float patrolWaitTime = 2f;
    [SerializeField] private float patrolRadius = 10f;

    [Header("References")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private Animator animator; // Optional: for animations

    private NavMeshAgent agent;
    private Vector3 startPosition;
    private AIState currentState;
    private float nextPatrolTime;

    private AudioSource audioSource;

    private enum AIState
    {
        Patrolling,
        Chasing
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        startPosition = transform.position;
        currentState = AIState.Patrolling;
        
        // Set initial speed
        agent.speed = patrolSpeed;

        audioSource = GetComponentInChildren<AudioSource>();

        // Start the patrol behavior
        StartCoroutine(PatrolBehavior());
    }

    private void Update()
    {
        if (playerTransform == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        bool canSeePlayer = CanSeeTarget(playerTransform.position);

        // State machine logic
        switch (currentState)
        {
            case AIState.Patrolling:
                if (distanceToPlayer <= detectionRange && canSeePlayer)
                {
                    StartChasing();
                }
                break;

            case AIState.Chasing:
                if (distanceToPlayer > detectionRange || !canSeePlayer)
                {
                    StartPatrolling();
                }
                else
                {
                    ChasePlayer();
                }
                break;
        }

        // Update animation parameters if animator is assigned
        // if (animator != null)
        // {
        //     animator.SetFloat("Speed", agent.velocity.magnitude / chaseSpeed);
        // }
    }

    private bool CanSeeTarget(Vector3 targetPosition)
    {
        Vector3 directionToTarget = targetPosition - transform.position;
        float distanceToTarget = directionToTarget.magnitude;
        
        // Check if there's any obstacle between AI and target
        if (Physics.Raycast(transform.position, directionToTarget, out RaycastHit hit, distanceToTarget, obstacleLayer))
        {
            return false;
        }
        
        return true;
    }

    private void StartChasing()
    {
        currentState = AIState.Chasing;
        agent.speed = chaseSpeed;
        audioSource.Play();
        StopAllCoroutines();
    }

    private void StartPatrolling()
    {
        currentState = AIState.Patrolling;
        agent.speed = patrolSpeed;
        StartCoroutine(PatrolBehavior());
    }

    private void ChasePlayer()
    {
        agent.SetDestination(playerTransform.position);
    }

    private IEnumerator PatrolBehavior()
    {
        while (currentState == AIState.Patrolling)
        {
            // If we've reached our destination or don't have one
            if (!agent.hasPath || agent.remainingDistance < 0.5f)
            {
                // Wait at the current point
                yield return new WaitForSeconds(patrolWaitTime);

                // Get a new random point to patrol to
                Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
                randomDirection += startPosition;

                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, 1))
                {
                    agent.SetDestination(hit.position);
                }
            }

            yield return null;
        }
    }

    // Optional: Visualize the detection range in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(startPosition, patrolRadius);
    }
    private float nextDamageTime = 0.5f;
    private float damageInterval = 3f;
    private int damageAmount = 35;
    // private void OnCollisionEnter(Collision collision)
    // {
    //     // Initial damage on first contact
    //     DealDamage(collision);
    // }

    private void OnCollisionStay(Collision collision)
    {
        // Continuous damage while in contact
        DealDamage(collision);
    }

    private void DealDamage(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && Time.time >= nextDamageTime)
        {

            EnvManager.Instance.setHealth(-damageAmount);
            nextDamageTime = Time.time + damageInterval;
            Debug.Log($"Damaged player for {damageAmount}");
        }
    }
}