using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;  // Horizontal movement speed
    public float jumpForce = 12f; // Jump force

    [Header("Ground Check Settings")]
    public Transform groundCheck;  // Position where we check for the ground
    public float groundCheckRadius = 0.2f;  // Radius of the ground check
    public LayerMask groundLayer; // Which layers are considered as ground

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private Vector2 moveInput; // Holds input for horizontal movement
    private bool isGrounded;   // Whether the player is grounded

    private InputSystem_Actions controls; // Input system actions to handle controls

    private void Awake()
    {
        // Initialize controls system
        controls = new InputSystem_Actions();

        // Set up movement input
        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    private void Start()
    {
        // Get references to necessary components
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Check if the player is on the ground using a small overlap circle
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Flip the sprite depending on movement direction
        if (moveInput.x > 0.01f) // Moving right
        {
            if (transform.localScale.x < 0)  // If flipped, un-flip it
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (moveInput.x < -0.01f) // Moving left
        {
            if (transform.localScale.x > 0)  // If not flipped, flip it
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        // Jump logic: Allow jump only if grounded and jump input is pressed
        if (isGrounded && controls.Player.Jump.ReadValue<float>() > 0.1f) // Check if the jump button is pressed
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);  // Apply jump force
        }
    }

    private void FixedUpdate()
    {
        // Horizontal movement: apply velocity based on input
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);  // Only change horizontal velocity
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the ground check radius in the editor for debugging
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
