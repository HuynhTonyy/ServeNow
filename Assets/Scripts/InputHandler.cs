using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private InputSystem_Actions inputActions;
    private Vector2 moveInput = Vector2.zero;
    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        inputActions.Player.Interact.started += Interact;

    }
    private void Update()
    {
        EventManager.Instance.InputMove(moveInput);
    }
    private void OnDisable()
    {
        inputActions.Player.Disable();
    }
    private void Interact(InputAction.CallbackContext context)
    {
        Debug.Log("Press E");
        EventManager.Instance.Interact();
    }
}
