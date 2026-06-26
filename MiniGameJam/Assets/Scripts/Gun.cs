using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("References")]
    public Transform muzzle;
    public Camera cam;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public float fireRate = 10f;
    public float bulletSpeed = 120f;
    public float spread = 0.01f;

    [Header("Ammo")]
    public bool infiniteAmmo = true;
    public int magSize = 30;
    public int ammo;

    float nextFire;

    void Start()
    {
        ammo = magSize;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= nextFire)
        {
            nextFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
       
        if (!infiniteAmmo)
        {
            if (ammo <= 0) return;
            ammo--;
        }


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