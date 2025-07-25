using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public Transform groundCheck;
    public float groundCheckDistance = 0.4f;
    public LayerMask groundMask;

    public float speed;
    public float runSpeed;
    private float originalSpeed;
    public float gravity;
    public float jumpHeight;

    public float crouchHeight = 1f;
    private float originalHeight;
    public float crouchSpeed;
    private bool isCrouching;

    private Vector3 velocity;
    private bool isGrounded;

    private void Start()
    {
        originalSpeed = speed;
        originalHeight = controller.height;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (Input.GetKey(KeyCode.LeftShift) && isCrouching == false)
        {
            speed = runSpeed;
        }
        else if (isCrouching == true)
        {
            speed = crouchSpeed;
        }
        else
        {
            speed = originalSpeed;
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.C) && isCrouching == false)
        {
            Crouch();
        }
        else if(Input.GetKeyDown(KeyCode.C) && isCrouching == true)
        {
            StandUp();
        }
    }

    void Crouch()
    {
        controller.height = crouchHeight;
        isCrouching = true;
    }

    void StandUp()
    {
        controller.height = originalHeight;
        isCrouching = false;
    }

}
