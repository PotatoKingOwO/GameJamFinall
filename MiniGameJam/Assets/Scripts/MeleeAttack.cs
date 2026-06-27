using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public int Damage = 1;
    public float damageCooldown = 1f;

    private float nextDamageTime = 0f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Time.time >= nextDamageTime)
            {
                PlayerHealth health = other.GetComponent<PlayerHealth>();

                if (health != null)
                {
                    health.GetHurt(Damage);
                    nextDamageTime = Time.time + damageCooldown;
                }
            }
        }
    }
}