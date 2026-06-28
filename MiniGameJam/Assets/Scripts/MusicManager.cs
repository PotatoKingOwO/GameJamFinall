using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    private void Awake()
    {
        // Pokud už existuje jiná instance, tuhle zniè
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Nezniè objekt pøi zmìnì scény
        DontDestroyOnLoad(gameObject);
    }
}