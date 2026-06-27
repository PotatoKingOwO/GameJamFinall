using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("References")]
    public Transform muzzle;
    public Camera cam;
    public AmmoManager ammoManager;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public float fireRate = 10f;
    public float bulletSpeed = 120f;
    public float spread = 0.01f;
    public int ammoCost = 1;

    [Header("Ammo")]
    public bool infiniteAmmo = true;

    float nextFire;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= nextFire)
        {
            if (!infiniteAmmo)
            {
                if (ammoManager == null) return;
                if (!ammoManager.UseAmmo(ammoCost)) return;
            }

            nextFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out RaycastHit hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(1000f);

        Vector3 direction = (targetPoint - muzzle.position).normalized;

        direction += Random.insideUnitSphere * spread;
        direction.Normalize();

        GameObject bullet = Instantiate(bulletPrefab, muzzle.position, Quaternion.LookRotation(direction));

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = direction * bulletSpeed;
    }
}