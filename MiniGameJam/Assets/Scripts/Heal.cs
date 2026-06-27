using UnityEngine;

public class Heal : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();

            if (health != null)
            {
                if (health.TryToHeal())
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}