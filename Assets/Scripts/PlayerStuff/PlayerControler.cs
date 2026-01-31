using Unity.VisualScripting;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{

    public int health;
    [SerializeField] private float immunityFrames;

    public float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private bool canDoubleJump;

    private Rigidbody2D rb;

    [SerializeField] private InputManager inputManager;

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform deathZone;

    private float bounceForce = 5f;
    private int bounceDirection;
    
    [Header("Accelerating")] 
    public float acceleration;
    public float deceleration;
    public float airAcceleration;
    public float airDeceleration;
    private float newAcceleration;

    [Header("WallJumping")] 
    [SerializeField] private float wallCheckDistance;

    private bool onRightWall;
    private bool onLeftWall;

    [Header("GroundCheck")] 
    [SerializeField] private Vector2 startPointOffset;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance;

    private float cFrames;

    private bool isGrounded;

    private float _horizontalInput;
    
    [Header("Game Manager")]
    [SerializeField] private GameObject gameManager;

    [SerializeField] private GameObject shopManager;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        inputManager.OnJump += JumpPressed;
        inputManager.OnHorizontal += HandleMove;
        inputManager.OnInteract += Interaction;
    }

    void OnDisable()
    {
        inputManager.OnJump -= JumpPressed;
        inputManager.OnHorizontal -= HandleMove;
        inputManager.OnInteract -= Interaction;

        Shop shop = shopManager.GetComponent<Shop>();

        if (shop == null) return;

        shop.ButtonUp();
    }

    void Interaction()
    {
        Shop shop = shopManager.GetComponent<Shop>();

        if (shop == null) return;
        
        shop.ButtonPress();
    }

    void JumpPressed()
    {
        if (rb == null) return;

        if (!isGrounded && !onRightWall && !onLeftWall)
            cFrames = 0;
        
        HandleJump();
    }

    void HandleJump()
    {
        if (isGrounded)
        {
            rb.AddForceY(jumpForce, ForceMode2D.Impulse);
            cFrames += 12;
        }
        else if (onRightWall)
        {
            rb.AddForce(new Vector2(-jumpForce / 2,  jumpForce), ForceMode2D.Impulse);
            cFrames += 12;
        }
        else if (onLeftWall)
        {
            rb.AddForce(new Vector2(jumpForce / 2,  jumpForce), ForceMode2D.Impulse);
            cFrames += 12;
        }
        else if (canDoubleJump)
        {
            rb.AddForceY(jumpForce, ForceMode2D.Impulse);
            cFrames += 12;
            canDoubleJump = false;
        }
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
            Death();
    }

    void Update()
    {
        if (cFrames <= 10)
            HandleJump();
        
        cFrames++;
        immunityFrames++;

        if (isGrounded || onLeftWall || onRightWall)
            canDoubleJump = true;
        
        Debug.Log(canDoubleJump);
    }

    void TakeDamage()
    {
        health--;
        immunityFrames = 0;
    }

    public void Heal(int regainedHealth)
    {
        health += regainedHealth;
    }

    void Death()
    {
        health = 3;
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
