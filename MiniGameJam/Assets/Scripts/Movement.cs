using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] Transform playerCamera;
    [SerializeField] Camera cam;
    [SerializeField] float normalFOV = 60f;
    [SerializeField] float sprintFOV = 75f;
    [SerializeField] float fovSpeed = 8f;

    [SerializeField, Range(0f, 0.5f)] float mouseSmoothTime = 0.03f;
    [SerializeField] bool cursorLock = true;
    [SerializeField] float mouseSensitivity = 3.5f;

    [Header("Movement")]
    [SerializeField] float Speed = 6f;
    [SerializeField] float SprintSpeed = 10f;
    [SerializeField, Range(0f, 0.5f)] float moveSmoothTime = 0.15f;

    [Header("Gravity")]
    [SerializeField] float gravity = -30f;
    [SerializeField] float jumpHeight = 2f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;

    [Header("Stamina")]
    [SerializeField] Slider sprintBar;
    [SerializeField] float maxStamina = 100f;
    [SerializeField] float staminaDrain = 25f;
    [SerializeField] float staminaRegen = 20f;

    private float currentStamina;
    private bool canSprint = true;

    float velocityY;
    bool isGrounded;

    float cameraCap;
    Vector2 currentMouseDelta;
    Vector2 currentMouseDeltaVelocity;

    CharacterController controller;
    Vector2 currentDir;
    Vector2 currentDirVelocity;

    float startSpeed;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        startSpeed = Speed;

        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        currentStamina = maxStamina;

        if (sprintBar != null)
        {
            sprintBar.maxValue = maxStamina;
            sprintBar.value = currentStamina;
        }

        if (cam == null)
            cam = Camera.main;

        cam.fieldOfView = normalFOV;
    }

    void Update()
    {
        HandleSprint();

        UpdateMouse();
        UpdateMove();

        if (sprintBar != null)
            sprintBar.value = currentStamina;
    }

    void HandleSprint()
    {
        if (currentStamina <= 0)
            canSprint = false;

        if (currentStamina >= maxStamina * 0.25f)
            canSprint = true;

        bool isMoving = Input.GetAxisRaw("Horizontal") != 0 ||
                        Input.GetAxisRaw("Vertical") != 0;

        bool sprinting = Input.GetKey(KeyCode.LeftShift) && canSprint && isMoving;

        if (sprinting)
        {
            Speed = SprintSpeed;
            currentStamina -= staminaDrain * Time.deltaTime;
        }
        else
        {
            Speed = startSpeed;
            currentStamina += staminaRegen * Time.deltaTime;
        }

        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

        float targetFOV = sprinting ? sprintFOV : normalFOV;
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, fovSpeed * Time.deltaTime);
    }

    void UpdateMouse()
    {
        Vector2 targetMouseDelta = new Vector2(
            Input.GetAxis("Mouse X"),
            Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(
            currentMouseDelta,
            targetMouseDelta,
            ref currentMouseDeltaVelocity,
            mouseSmoothTime);

        cameraCap -= currentMouseDelta.y * mouseSensitivity;
        cameraCap = Mathf.Clamp(cameraCap, -90f, 90f);

        playerCamera.localEulerAngles = Vector3.right * cameraCap;
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    void UpdateMove()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, ground);

        if (isGrounded && velocityY < 0)
        {
            velocityY = -2f;
        }

        Vector2 targetDir = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"));

        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(
            currentDir,
            targetDir,
            ref currentDirVelocity,
            moveSmoothTime);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocityY = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocityY += gravity * Time.deltaTime;

        Vector3 velocity =
            (transform.forward * currentDir.y +
             transform.right * currentDir.x) * Speed +
            Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);
    }
}