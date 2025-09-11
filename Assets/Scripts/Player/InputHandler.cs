using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private InputSystem_Actions inputActions;
    private Vector2 moveInput = Vector2.zero;
    private Coroutine holdCoroutine;
    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        inputActions.Player.Interact.started += Interact;
        inputActions.Player.Operate.started += StartOperate;
        inputActions.Player.Operate.canceled += CancleOperate;

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
        EventManager.Instance.InputInteract();
    }
    private void StartOperate(InputAction.CallbackContext context)
    {
        holdCoroutine = StartCoroutine(Hold());
    }
    private void CancleOperate(InputAction.CallbackContext context)
    {
        StopCoroutine(holdCoroutine);
    }
    private IEnumerator Hold()
    {
        while (true)
        {
            EventManager.Instance.InputOperate();
            yield return new WaitForSeconds(0.1f);
        }
    }
}
