using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public int playerHealth = 100;
    public Slider healthBar;

    [Header("Damage Audio")]
    public AudioSource damageAudio;
    public AudioClip[] damageSounds;

    void Start()
    {
        if (healthBar != null)
        {
            healthBar.maxValue = playerHealth;
            healthBar.value = playerHealth;
        }
    }

    public void GetHurt(int damage)
    {
        playerHealth -= damage;

        PlayDamageSound();

        if (healthBar != null)
        {
            healthBar.value = playerHealth;
        }

        if (playerHealth <= 0)
        {
            Die();
        }
    }

    void PlayDamageSound()
    {
        if (damageAudio == null || damageSounds == null || damageSounds.Length == 0)
            return;

        AudioClip clip = damageSounds[Random.Range(0, damageSounds.Length)];

        damageAudio.pitch = Random.Range(0.95f, 1.05f);
        damageAudio.PlayOneShot(clip);
    }

    public void Die()
    {
        Debug.Log("You Died!");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public bool TryToHeal()
    {
        if (playerHealth < 5)
        {
            playerHealth++;

            if (healthBar != null)
                healthBar.value = playerHealth;

            return true;
        }

        return false;
    }
}