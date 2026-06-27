using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealth;

    public Slider healthBar;
    void Start()
    {
        if (healthBar != null)
        {
            healthBar.value = playerHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetHurt(int damage)
    {
        playerHealth =- damage;
        healthBar.value = playerHealth;
    }
}
