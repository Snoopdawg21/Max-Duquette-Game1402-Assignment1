using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private Vector2 startPointOffset;
    [SerializeField] private float checkDistance;
    [SerializeField] private LayerMask player;
    [SerializeField] private Transform playerPos;

    private int direction;

    private bool isCharging;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        isCharging = false;

        checkDistance = startPointOffset.x * 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isCharging == false)
        {
            LookForPlayer();
        }
        else
        {
            ChargeAtPlayer();
        }
        
        Debug.Log(isCharging);
    }

    void Update()
    {
        if (playerPos.position.x > transform.position.x)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
    }

    void LookForPlayer()
    {
        isCharging = Physics2D.Raycast((Vector2)transform.position + startPointOffset, Vector2.left, checkDistance, player);
    }

    void ChargeAtPlayer()
    {
        if (rb == null) return;
        
        rb.linearVelocityX += direction * speed;
    }

    void OnDrawGizmos()
    {
        Debug.DrawLine((Vector2)transform.position + startPointOffset, (Vector2)transform.position + startPointOffset + Vector2.left * checkDistance, isCharging ? Color.green:Color.red);
    }
}
