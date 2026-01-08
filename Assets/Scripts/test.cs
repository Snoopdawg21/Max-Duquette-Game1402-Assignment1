using UnityEngine;
using UnityEngine.InputSystem;

public class test : MonoBehaviour
{

    private PlayerInputActions testActions;

    void Awake()
    {
        testActions = new PlayerInputActions();
        testActions.Enable();
    }

    void OnEnable()
    {
        testActions.Player.Jump.performed += Jump;
    }

    void OnDisable()
    {
        testActions.Player.Jump.performed -= Jump;
    }

    void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jumping and jumping all about");
    }
}
