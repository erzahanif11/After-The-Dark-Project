using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private Transform player;
    public float chaseRange = 10f;
    public float roamingRange = 20f;
    public float speed = 3.5f;
    public float waitTime = 2f;

    private NavMeshAgent agent;
    private Vector3 startPoint;
    private Vector3 roamingPoint;
    private float distanceToPlayer;
    private bool isChasing = false;
    private bool isRoaming = false;
    private bool hasTriggered = false;

    // ========== Added Variables for Freezing ==========
    private bool isFrozen = false; // Status whether the enemy is frozen
    private float originalSpeed; // Stores the original speed before freezing
    // ===================================================

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        startPoint = transform.position;
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        StartCoroutine(Roam());

        // Save original speed
        originalSpeed = speed;
    }

    void Update()
    {
        if (isFrozen) return; // Stop all movement when frozen

        distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseRange)
        {
            isChasing = true;
            isRoaming = false;
            ChasePlayer();
        }
        else if (!isRoaming)
        {
            isChasing = false;
            StartCoroutine(Roam());
        }
    }

    void ChasePlayer()
    {
        agent.speed = speed;
        agent.SetDestination(player.position);
    }

    IEnumerator Roam()
    {
        isRoaming = true;

        while (!isChasing)
        {
            roamingPoint = GetRandomPoint();
            agent.SetDestination(roamingPoint);

            while (Vector3.Distance(transform.position, roamingPoint) > 1f && !isChasing)
            {
                yield return null;
            }

            yield return new WaitForSeconds(waitTime);
        }

        isRoaming = false;
    }

    Vector3 GetRandomPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * roamingRange;
        randomDirection += startPoint;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, roamingRange, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            TimeManager.Instance.AddTime();
            Destroy(gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(startPoint, roamingRange);
    }

    // ========== Added Methods for Freezing ==========

    public void Freeze()
    {
        if (!isFrozen)
        {
            isFrozen = true;
            agent.isStopped = true; // Stop the NavMeshAgent
            StartCoroutine(Unfreeze());
        }
    }

    private IEnumerator Unfreeze()
    {
        yield return new WaitForSeconds(2f); // Freeze for 2 seconds
        isFrozen = false;
        agent.isStopped = false; // Resume movement
        agent.speed = originalSpeed;
    }

    // ===============================================
}
