using UnityEngine;
using UnityEngine.AI;

public class GoToPlayer : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject player;

    [Header("Detection")]
    public float activationDistance = 10f;

    bool isActive = false;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        agent.isStopped = true; 
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (!isActive && distance <= activationDistance)
        {
            isActive = true;
            agent.isStopped = false;
        }

        if (!isActive) return;

        agent.SetDestination(player.transform.position);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, activationDistance);
    }
}