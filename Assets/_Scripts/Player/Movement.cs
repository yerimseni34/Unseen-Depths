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
    public float walkSpeed = 6f;
    public float runSpeed = 10f;
    private float currentSpeed;
    private Vector3 moveDirection;
    private Vector3 smoothVelocity;

    [Header("Jumping & Gravity")]
    public float gravity = -20f;
    public float jumpHeight = 2f;
    private float verticalVelocity;
    public float airControlMultiplier = 0.5f;
    public float groundAcceleration = 10f;
    public float airAcceleration = 3f;

    //[Header("Crouching")]
    //public float crouchHeight = 1f;
    //private float originalHeight;
    //public float crouchSpeed = 3f;
    //private bool isCrouching;

    private void Start()
    {
        currentSpeed = walkSpeed;
        //originalHeight = controller.height;
    }

    private void Update()
    {
        HandleGroundCheck();
        HandleRun();
        HandleMovement();
        HandleJump();
        ApplyGravity();

        //if (!isCrouching)
        //{
        //    controller.height = Mathf.Lerp(controller.height, originalHeight, Time.deltaTime * 10f);
        //}
    }

    private void HandleGroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);
        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f; // yere çakılma hissini yumuşatır
        }
    }

    private void HandleRun()
    {
        if (Input.GetKey(KeyCode.LeftShift)) // && !isCrouching
        {
            currentSpeed = runSpeed;
        }
        //else if (isCrouching)
        //{
        //    currentSpeed = crouchSpeed;
        //}
        else
        {
            currentSpeed = walkSpeed;
        }
    }

    private void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 inputDirection = (transform.right * x + transform.forward * z).normalized;

        float acceleration = isGrounded ? groundAcceleration : airAcceleration;
        float controlMultiplier = isGrounded ? 1f : airControlMultiplier;

        moveDirection = Vector3.SmoothDamp(
            moveDirection,
            inputDirection * currentSpeed * controlMultiplier,
            ref smoothVelocity,
            1f / acceleration
        );

        Vector3 move = moveDirection * Time.deltaTime;
        move.y = 0f; // yer hareketi
        controller.Move(move);
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private void ApplyGravity()
    {
        verticalVelocity += gravity * Time.deltaTime;
        Vector3 verticalMove = new Vector3(0f, verticalVelocity, 0f) * Time.deltaTime;
        controller.Move(verticalMove);
    }

    //private void HandleCrouchToggle()
    //{
    //    if (Input.GetKeyDown(KeyCode.C))
    //    {
    //        if (isCrouching)
    //            StandUp();
    //        else
    //            Crouch();
    //    }
    //}

    //private void Crouch()
    //{
    //    controller.height = crouchHeight;
    //    isCrouching = true;
    //}

    //private void StandUp()
    //{
    //    isCrouching = false;
    //}
}