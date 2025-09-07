using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private float accelerateSpeed;
    [SerializeField] private float maxSpeed;
    [Header("Rotation")]
    [SerializeField] private float rotateSpeed;
    [Header("Animation")]
    [SerializeField] private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody.maxLinearVelocity = maxSpeed;
    }
    private void OnEnable() {
        EventManager.Instance.onInputMove += MoveControl;
    }
    private void OnDisable() {
        EventManager.Instance.onInputMove -= MoveControl;
    }
    
    private void MoveControl(Vector2 inputMoveDir)
    {
        animator.SetFloat("velocity", rigidbody.linearVelocity.magnitude);
        Vector3 moveDir = new Vector3(inputMoveDir.x, 0, inputMoveDir.y);
        if (moveDir == Vector3.zero)
        {
            rigidbody.linearVelocity = Vector3.zero;
        }
        else
        {
            float rotateDir = moveDir.magnitude;
            if (rotateDir > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDir, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
            }
            Vector3 force = accelerateSpeed * Time.deltaTime * moveDir;
            rigidbody.AddForce(force, ForceMode.Impulse);
        }
    } 
}
