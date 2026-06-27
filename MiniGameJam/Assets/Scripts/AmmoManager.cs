using TMPro;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    public int ammo = 90;
    public TMP_Text ammoText;

    void Start()
    {
        UpdateUI();
    }

    public void AddAmmo(int amount)
    {
        ammo += amount;
        UpdateUI();
    }

    public bool UseAmmo(int amount)
    {
        if (ammo < amount) return false;

        ammo -= amount;
        UpdateUI();
        return true;
    }

    void UpdateUI()
    {
        if (ammoText != null)
        {
            ammoText.text = ammo.ToString("D3");
        }
    }
}