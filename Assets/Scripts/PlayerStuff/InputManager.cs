using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInputActions inputActions;
    
    public System.Action OnJump;
    public System.Action<float> OnHorizontal;
    public System.Action OnInteract;
    public System.Action OnPauseGame;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Enable();
    }

    void OnEnable()
    {
        inputActions.Player.Jump.performed += Jump;
        inputActions.Player.Interact.performed += Interact;
        inputActions.Player.PauseGame.performed += Pause;
    }

    void OnDisable()
    {
        inputActions.Player.Jump.performed -= Jump;
        inputActions.Player.Interact.performed -= Interact;
        inputActions.Player.PauseGame.performed -= Pause;
    }

    void Jump(InputAction.CallbackContext ctx)
    {
        OnJump?.Invoke();
    }

    void Interact(InputAction.CallbackContext ctx)
    {
        OnInteract?.Invoke();
    }

    void Pause(InputAction.CallbackContext ctx)
    {
        OnPauseGame?.Invoke();
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