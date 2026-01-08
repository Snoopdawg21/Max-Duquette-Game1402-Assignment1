using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpForce;
    [SerializeField] private float speed;
    [SerializeField] private float acceleration;
    
    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Enable();
    }

    void OnEnable()
    {
        inputActions.Player.Jump.performed += Jump;
        inputActions.Player.Left.performed += MoveLeft;
        inputActions.Player.Right.performed += MoveRight;
    }

    void OnDisable()
    {
        inputActions.Player.Jump.performed -= Jump;
        inputActions.Player.Left.performed -= MoveLeft;
        inputActions.Player.Right.performed -= MoveRight;
    }

    void Jump(InputAction.CallbackContext ctx)
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y + jumpForce);
    }
    
    void MoveLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x - (speed * acceleration), rb.linearVelocity.y);
            acceleration += (acceleration/2);
        }

        if (context.canceled)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x + (speed * acceleration), rb.linearVelocity.y);
            acceleration -= (acceleration/2);
        }
    }
    
    void MoveRight(InputAction.CallbackContext context)
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x + (speed * acceleration), rb.linearVelocity.y);
        acceleration += (acceleration/2);
    }
}
