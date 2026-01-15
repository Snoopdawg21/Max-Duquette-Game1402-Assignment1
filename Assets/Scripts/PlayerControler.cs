using System;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private Vector2 minMaxSpeed;
    [SerializeField] private float acceleration = 0.05f;
    [SerializeField] private float jumpForce = 15f;
    
    private Rigidbody2D rb;

    [SerializeField] private InputManager inputManager;

    private bool isGrounded;
    [SerializeField] private Vector2 startPointOffset;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance;

    private float _horizontalInput;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        inputManager.OnJump += HandleJump;
        inputManager.OnHorizontal += HandleMove;
    }

    void OnDisable()
    {
        inputManager.OnJump -= HandleJump;
        inputManager.OnHorizontal -= HandleMove;
    }

    void HandleJump()
    {
        if (rb == null) return;

        if (isGrounded)
            rb.AddForceY(jumpForce, ForceMode2D.Impulse);
        
    }

    void HandleMove(float direction)
    {
        _horizontalInput = direction;
    }

    void FixedUpdate()
    {
        HandleMovement();
        GroundCheck();
    }

    void Update()
    {
        if (transform.position.y < -25f)
        {
            transform.position = new Vector3(0, 0, 0);
        }
    }

    void HandleMovement()
    {
        if (rb == null) return;
        
        rb.linearVelocityX = _horizontalInput * moveSpeed;

        if (_horizontalInput != 0 && moveSpeed < minMaxSpeed.y && isGrounded)
        {
            moveSpeed += acceleration;
        } else if (moveSpeed > minMaxSpeed.x)
        {
            moveSpeed -= acceleration;
        }
        
        Debug.Log(moveSpeed);
    }
    
    void GroundCheck()
    {
        isGrounded = Physics2D.Raycast((Vector2)transform.position + startPointOffset, Vector2.down, groundCheckDistance,
            groundLayer);
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine((Vector2)transform.position + startPointOffset, (Vector2)transform.position + startPointOffset + Vector2.down * groundCheckDistance, isGrounded? Color.green:Color.red);
    }
}
