using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10;

    [Header("Lifetime")]
    public float lifeTime = 5f;

    [Header("Effects")]
    public GameObject hitEffect;
    public GameObject hitEffectNotEnemy;

    [Header("Hit Audio")]
    public AudioSource hitAudio;
    public AudioClip[] hitSounds;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayHitSound();

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Health health = collision.gameObject.GetComponent<Health>();

            if (health != null)
            {
                health.TakeDamage(damage);

                if (hitEffect != null)
                {
                    ContactPoint contact = collision.contacts[0];
                    Instantiate(hitEffect, contact.point, Quaternion.LookRotation(contact.normal));
                }
            }
        }
        else
        {
            if (hitEffectNotEnemy != null)
            {
                ContactPoint contact = collision.contacts[0];
                Instantiate(hitEffectNotEnemy, contact.point, Quaternion.LookRotation(contact.normal));
            }
        }

        Destroy(gameObject);
    }

    void PlayHitSound()
    {
        if (hitSounds == null || hitSounds.Length == 0)
            return;

        AudioClip clip = hitSounds[Random.Range(0, hitSounds.Length)];

        GameObject soundObj = new GameObject("3D_HitSound");
        soundObj.transform.position = transform.position;

        AudioSource audio = soundObj.AddComponent<AudioSource>();

        audio.clip = clip;
        audio.spatialBlend = 1f; // ?? FULL 3D
        audio.rolloffMode = AudioRolloffMode.Linear;
        audio.minDistance = 2f;
        audio.maxDistance = 25f;

        audio.pitch = Random.Range(0.95f, 1.05f);
        audio.volume = 1f;

        audio.Play();

        Destroy(soundObj, clip.length);
    }
}