using UnityEngine;

public class OrbitAndFloat : MonoBehaviour
{
    [Header("Orbit (rotace)")]
    public float rotateSpeed = 50f;

    [Header("Rotation Axis")]
    public bool rotateX = false;
    public bool rotateY = true;
    public bool rotateZ = false;

    [Header("Float (houpání)")]
    public float floatHeight = 0.5f;
    public float floatSpeed = 2f;

    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        Vector3 rotation = Vector3.zero;

        if (rotateX) rotation.x = 1f;
        if (rotateY) rotation.y = 1f;
        if (rotateZ) rotation.z = 1f;

        transform.Rotate(rotation * rotateSpeed * Time.deltaTime);

        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;

        transform.position = new Vector3(
            startPos.x,
            newY,
            startPos.z
        );
    }
}