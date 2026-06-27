using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealth = 100;
    public Slider healthBar;

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

        if (healthBar != null)
        {
            healthBar.value = playerHealth;
        }

        if (playerHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("You Died!");
    }
}