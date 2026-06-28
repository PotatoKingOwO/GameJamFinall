using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private GameObject doors;

    private static int keysCollected = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        keysCollected++;

        if (keysCollected >= 2 && doors != null)
        {
            doors.SetActive(false); // otevøe dveøe
        }

        Destroy(gameObject);
    }
}