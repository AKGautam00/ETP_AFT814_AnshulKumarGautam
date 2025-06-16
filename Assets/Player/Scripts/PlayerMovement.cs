using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 12f;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask GroundLayer;

    private Rigidbody2D rb;
    private BoxCollider2D playerCollider;
    private InputSystem_Actions controls;


    private void Awake()
    {
        controls = new InputSystem_Actions();

        
    }




}

