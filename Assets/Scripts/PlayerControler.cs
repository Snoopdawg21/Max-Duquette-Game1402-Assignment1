using System;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 15f;

    private Rigidbody2D rb;

    [SerializeField] private InputManager inputManager;

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform deathZone;

    [Header("Wall Jump")]
    private bool isOnWall;

    private float wallJumpTimer;

    [SerializeField] private float wallCheckDistance;
    [SerializeField] private Vector2 wallCheckOffset;

    [Header("Ground Check")] 
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

        if (isGrounded || isOnWall || wallJumpTimer >= 0)
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
        WallCheck();
    }

    void Update()
    {
        if (transform.position.y < deathZone.position.y)
        {
            transform.position = spawnPoint.position;
        }

        if (isOnWall == true)
        {
            wallJumpTimer = 10;
        }
        else
        {
            wallJumpTimer--;
        }
    }

    void HandleMovement()
    {
        if (rb == null) return;
        
        rb.linearVelocityX = _horizontalInput * moveSpeed;
    }
    
    void GroundCheck()
    {
        isGrounded = Physics2D.Raycast((Vector2)transform.position + startPointOffset, Vector2.down, groundCheckDistance,
            groundLayer);
    }
    
    void WallCheck() 
    {
        isOnWall = Physics2D.Raycast((Vector2)transform.position + wallCheckOffset, Vector2.right,  wallCheckDistance,  groundLayer);
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine((Vector2)transform.position + startPointOffset, (Vector2)transform.position + startPointOffset + Vector2.down * groundCheckDistance, isGrounded? Color.green:Color.red);
        Debug.DrawLine((Vector2)transform.position + wallCheckOffset, (Vector2)transform.position + wallCheckOffset + Vector2.right * wallCheckDistance, isOnWall? Color.green:Color.red);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            transform.position = spawnPoint.position;
        }
    }
}
