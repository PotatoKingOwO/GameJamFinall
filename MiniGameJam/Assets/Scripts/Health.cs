using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 100;

    [Header("Materials")]
    public Material normalMaterial;
    public Material hitMaterial;

    public Renderer rend;

    public bool alerted = false;

    [Header("Hit Audio")]
    public AudioClip[] hitSounds;
    public AudioSource audioSource;

    void Start()
    {
        if (normalMaterial != null)
            rend.material = normalMaterial;

        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
            audioSource.loop = false;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        alerted = true;

        PlayHitSound();

        StartCoroutine(FlashMaterial());

        if (health <= 0)
        {
            KillUnit();
        }
    }

    void PlayHitSound()
    {
        if (audioSource == null || hitSounds == null || hitSounds.Length == 0)
            return;

        AudioClip clip = hitSounds[Random.Range(0, hitSounds.Length)];

        audioSource.pitch = Random.Range(0.95f, 1.05f);
        audioSource.PlayOneShot(clip);
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