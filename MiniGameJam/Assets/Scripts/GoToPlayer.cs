using UnityEngine;
using UnityEngine.AI;

public class GoToPlayer : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject player;
    public Animator animator;

    [Header("Detection")]
    public float activationDistance = 10f;

    [Header("Offset")]
    public float stopDistance = 1f;

    bool isActive = false;
    Health health;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        health = GetComponent<Health>();

        agent.isStopped = true;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (!isActive && (distance <= activationDistance || (health != null && health.alerted)))
        {
            isActive = true;
            agent.isStopped = false;
        }

        if (!isActive)
        {
            if (animator != null)
                animator.SetBool("isWalking", false);

            return;
        }

        Vector3 direction = (transform.position - player.transform.position).normalized;
        Vector3 targetPosition = player.transform.position + direction * stopDistance;

        agent.SetDestination(targetPosition);

        if (animator != null)
        {
            bool walking = agent.velocity.magnitude > 0.1f && agent.remainingDistance > stopDistance;
            animator.SetBool("isWalking", walking);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, activationDistance);
    }
}