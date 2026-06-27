using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    public float cooldown = 1.5f;

    public GameObject player;
    public GameObject bullet;
    public Transform muzzle;
    public float speed = 10f;

    [Header("Detection")]
    public float activationDistance = 10f;

    bool isActive = false;
    float nextShootTime = 0f;

    Health health;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        health = GetComponent<Health>();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (!isActive && (distance <= activationDistance || (health != null && health.alerted)))
        {
            isActive = true;
        }

        if (!isActive) return;

        if (Time.time >= nextShootTime)
        {
            Shoot();
            nextShootTime = Time.time + cooldown;
        }
    }

    void Shoot()
    {
        Vector3 direction = (player.transform.position - muzzle.position).normalized;

        GameObject b = Instantiate(bullet, muzzle.position, Quaternion.LookRotation(direction));

        Rigidbody rb = b.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = direction * speed;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, activationDistance);
    }
}