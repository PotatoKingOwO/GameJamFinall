using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public GameObject[] enemies;
    }

    [Header("Arena")]
    public GameObject doors;
    public GameObject key;

    [Header("Waves")]
    public Wave[] waves;

    private int currentWave = -1;
    private bool arenaStarted = false;

    void Start()
    {
        // Vypnutí všech nepøátel
        foreach (Wave wave in waves)
        {
            foreach (GameObject enemy in wave.enemies)
            {
                if (enemy != null)
                    enemy.SetActive(false);
            }
        }

        // Klíè je schovaný
        if (key != null)
            key.SetActive(false);

        // Dveøe otevøené
        if (doors != null)
            doors.SetActive(false);
    }

    void Update()
    {
        if (!arenaStarted || currentWave == -1)
            return;

        if (IsCurrentWaveCleared())
        {
            StartNextWave();
        }
    }

    private void StartNextWave()
    {
        currentWave++;

        if (currentWave >= waves.Length)
        {
            // Konec arény
            if (doors != null)
                doors.SetActive(false);

            if (key != null)
                key.SetActive(true);

            enabled = false;
            return;
        }

        foreach (GameObject enemy in waves[currentWave].enemies)
        {
            if (enemy != null)
                enemy.SetActive(true);
        }
    }

    private bool IsCurrentWaveCleared()
    {
        foreach (GameObject enemy in waves[currentWave].enemies)
        {
            if (enemy != null)
                return false;
        }

        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (arenaStarted)
            return;

        if (other.CompareTag("Player"))
        {
            arenaStarted = true;

            if (doors != null)
                doors.SetActive(true);

            StartNextWave();
        }
    }
}