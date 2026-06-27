using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage = 10;

    [Header("Lifetime")]
    public float lifeTime = 5f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();

            if (health != null)
            {
                health.GetHurt(damage);
            }
        }

        Destroy(gameObject);
    }
}