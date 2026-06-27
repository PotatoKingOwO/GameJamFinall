using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int ammoAmount = 10;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AmmoManager manager = other.GetComponent<AmmoManager>();

            if (manager != null)
            {
                manager.AddAmmo(ammoAmount);
                Destroy(gameObject);
            }
        }
    }
}