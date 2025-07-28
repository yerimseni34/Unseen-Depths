using UnityEngine;

public class Movement : MonoBehaviour
{

    [Header("References")]
    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;


    [Header("Ground Check")]
    public float groundCheckDistance = 0.4f;
    private bool isGrounded;


    [Header("Movement")]
    public float speed = 6f;
    public float runSpeed = 10f;
    private float originalSpeed;


    [Header("Jumping & Gravity")]
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    private Vector3 velocity;


    [Header("Crouching")]
    public float crouchHeight = 1f;
    private float originalHeight;
    public float crouchSpeed = 3f;
    private bool isCrouching;


    private void Start()
    {
        originalSpeed = speed;
        originalHeight = controller.height;
    }

    private void Update()
    {
        HandleGroundCheck();
        HandleMovement();
        HandleJump();
        HandleRun();
        HandleCrouchToggle();
        ApplyGravity();

        if (!isCrouching)
        {
            controller.height = Mathf.Lerp(controller.height, originalHeight, Time.deltaTime * 10f);
        }

    }

    private void HandleGroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    private void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private void HandleRun()
    {
        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching)
        {
            speed = runSpeed;
        }
        else if (isCrouching)
        {
            speed = crouchSpeed;
        }
        else
        {
            speed = originalSpeed;
        }
    }

    private void HandleCrouchToggle()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isCrouching)
                StandUp();
            else
                Crouch();
        }
    }

    private void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Crouch()
    {
        controller.height = crouchHeight;
        isCrouching = true;
    }

    private void StandUp()
    {
        isCrouching = false;
    }
}