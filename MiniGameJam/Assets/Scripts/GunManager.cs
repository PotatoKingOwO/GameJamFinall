using UnityEngine;

public class GunManager : MonoBehaviour
{
    public GameObject gun1;
    public GameObject gun2;

    void Start()
    {
        EquipGun1();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipGun1();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipGun2();
        }
    }

    void EquipGun1()
    {
        gun1.SetActive(true);
        gun2.SetActive(false);
    }

    void EquipGun2()
    {
        gun1.SetActive(false);
        gun2.SetActive(true);
    }
}