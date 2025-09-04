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

    // Update is called once per frame
    void Update()
    {
        MoveControl();
    }
    private void MoveControl()
    {
        animator.SetFloat("velocity",rigidbody.linearVelocity.magnitude);
        Vector3 up = Vector3.zero;
        Vector3 down = Vector3.zero;
        Vector3 left = Vector3.zero;
        Vector3 right = Vector3.zero;
        if (Keyboard.current.wKey.isPressed)
        {
            up = Vector3.forward;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            down = Vector3.back;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            left = Vector3.left;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            right = Vector3.right;
        }
        Vector3 moveDir = up + down + left + right;
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
