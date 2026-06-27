using UnityEngine;

public class BillBoard : MonoBehaviour
{
    public Transform player;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
    void Update()
    {
        if (player == null) return;

        transform.LookAt(player);

        Vector3 rotation = transform.eulerAngles;
        transform.eulerAngles = new Vector3(0f, rotation.y, 0f);
    }
}