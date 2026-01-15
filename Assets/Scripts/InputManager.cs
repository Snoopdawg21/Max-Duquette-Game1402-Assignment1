using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInputActions inputActions;
    
    public System.Action OnJump;
    public System.Action<float> OnHorizontal;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Enable();
    }

    void OnEnable()
    {
        inputActions.Player.Jump.performed += Jump;
    }

    void OnDisable()
    {
        inputActions.Player.Jump.performed -= Jump;
    }

    void Jump(InputAction.CallbackContext ctx)
    {
        OnJump?.Invoke();
    }

    void HorizontalMovement()
    {
        OnHorizontal?.Invoke(inputActions.Player.Horizontal.ReadValue<float>());
    }

    void Update()
    {
        HorizontalMovement();
    }
}