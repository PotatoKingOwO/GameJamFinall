using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10;
    public GameObject hitEffect; 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Health health = collision.gameObject.GetComponent<Health>();

            if (health != null)
            {
                health.TakeDamage(damage);
                if (hitEffect != null)
                {
                    ContactPoint contact = collision.contacts[0];

                    Instantiate(hitEffect, contact.point, Quaternion.LookRotation(contact.normal));
                }
            }
        }

        Destroy(gameObject);
    }
}