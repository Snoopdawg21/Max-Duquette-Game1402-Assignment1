using UnityEngine;

public class PlayerControler : MonoBehaviour
{

    [SerializeField] private int health;
    [SerializeField] private float immunityFrames;

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 15f;

    private Rigidbody2D rb;

    [SerializeField] private InputManager inputManager;

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform deathZone;
    
    private float bounceForce = 5f;
    private int bounceDirection;

    
    [Header("Accelerating")]
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;
    [SerializeField] private float airAcceleration;
    [SerializeField] private float airDeceleration;
    private float newAcceleration;
    
    [Header("WallJumping")]
    [SerializeField] private float wallCheckDistance;
    
    private bool onRightWall;
    private bool onLeftWall;

    private float wallJumpCFrames;

   [Header("GroundCheck")]
    [SerializeField] private Vector2 startPointOffset;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance;
    
    private bool isGrounded;

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

        if (onRightWall)
            rb.AddForce(new Vector2(-jumpForce / 2,  jumpForce), ForceMode2D.Impulse);
        
        if (onLeftWall)
            rb.AddForce(new Vector2(jumpForce / 2,  jumpForce), ForceMode2D.Impulse);
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
        
        if (transform.position.y < deathZone.position.y)
            Death();
        if (health <= 0)
        {
            Death();

            health = 3;
        }
    }

    void Update()
    {
        if (onRightWall == true)
        {
            wallJumpCFrames = 10;
        }
        else
        {
            wallJumpCFrames--;
        }

        immunityFrames++;
        Debug.Log(health);
    }

    void TakeDamage()
    {
        health--;
        immunityFrames = 0;
    }

    public void Heal(int regainedHealth)
    {
        Debug.Log("healed it");
        //int newHealth = health + regainedHealth;
        //health = newHealth;
        health++;
    }

    void Death()
    {
        transform.position = spawnPoint.position;
    }

    void HandleMovement()
    {
        if (rb == null) return;

        //check if grounded
        //change acceleration to air acceleration if not grounded
        if (isGrounded)
            newAcceleration = Mathf.Abs(_horizontalInput) <= 0.01f ? deceleration : acceleration;
        else
            newAcceleration = Mathf.Abs(_horizontalInput) <= 0.01f ? airDeceleration : airAcceleration;
        
        float targetSpeed = moveSpeed * _horizontalInput;
        float newSpeed = Mathf.MoveTowards(rb.linearVelocityX, targetSpeed, newAcceleration * Time.fixedDeltaTime);
        
        rb.linearVelocityX = newSpeed;

    }
    
    void GroundCheck()
    {
        isGrounded = Physics2D.Raycast((Vector2)transform.position + startPointOffset, Vector2.down, groundCheckDistance,
            groundLayer);
    }
    
    void WallCheck() 
    {
        onRightWall = Physics2D.Raycast((Vector2)transform.position, Vector2.right,  wallCheckDistance,  groundLayer);
        onLeftWall = Physics2D.Raycast((Vector2)transform.position, Vector2.left,  wallCheckDistance,  groundLayer);
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine((Vector2)transform.position + startPointOffset, (Vector2)transform.position + startPointOffset + Vector2.down * groundCheckDistance, isGrounded? Color.green:Color.red);
        Debug.DrawLine((Vector2)transform.position, (Vector2)transform.position + Vector2.right * wallCheckDistance, onRightWall? Color.green:Color.red);
        Debug.DrawLine((Vector2)transform.position, (Vector2)transform.position + Vector2.left * wallCheckDistance, onLeftWall? Color.green:Color.red);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && immunityFrames > 60)
        {
            TakeDamage();

            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            
            enemy.HitPlayer();
            
            if (collision.transform.position.x > transform.position.x)
                bounceDirection = -1;
            else
                bounceDirection = 1;
            
            rb.AddForce(new Vector2(bounceForce * bounceDirection, bounceForce), ForceMode2D.Impulse);
            collision.rigidbody.AddForce(new Vector2(bounceForce * (bounceDirection * -1), bounceForce), ForceMode2D.Impulse);
        }
    }
}
