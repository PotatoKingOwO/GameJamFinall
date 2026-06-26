using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 100;

    [Header("Materials")]
    public Material normalMaterial;
    public Material hitMaterial;

    public Renderer rend;

    void Start()
    {


        if (normalMaterial != null)
            rend.material = normalMaterial;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        StartCoroutine(FlashMaterial());

        if (health <= 0)
        {
            KillUnit();
        }
    }

    IEnumerator FlashMaterial()
    {
        if (hitMaterial != null)
            rend.material = hitMaterial;

        yield return new WaitForSeconds(0.1f);

        if (normalMaterial != null)
            rend.material = normalMaterial;
    }

    public void KillUnit()
    {
        Destroy(gameObject);
    }
}