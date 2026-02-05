using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gorilla : MonoBehaviour
{
    public event Action AttackRequested;
    Animator Anicontroller;
    InputAction moveAction;
    InputAction attackAction;
    CapsuleCollider hitbox;
    Rigidbody rb;
    [SerializeField] float MoveSpeed = 10;
    public bool Hashit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hitbox = GetComponentInChildren<CapsuleCollider>();;
        Anicontroller = GetComponent<Animator>();
        moveAction = InputSystem.actions.FindAction("Move");
        attackAction = InputSystem.actions.FindAction("Jump");
        rb = GetComponent<Rigidbody>();

        hitbox.enabled = false;

        attackAction.performed += OnAttackPerformed;

        AttackRequested += Spinattack;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        Vector3 velocity = new Vector3(moveValue.x, 0f, moveValue.y) * MoveSpeed;

        rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);



        if(moveAction.IsPressed()){
            transform.forward = new Vector3 (moveValue.x, 0f, moveValue.y);
            Anicontroller.SetBool("IsWalking", true);
        }
        else{
            Anicontroller.SetBool("IsWalking", false);
        }

    }

    void OnAttackPerformed(InputAction.CallbackContext ctx)
    {
        AttackRequested?.Invoke();
    }

    void Spinattack()
    {
        Debug.Log("Spin!");
        Anicontroller.SetTrigger("Spin");
    }

    void OnEnable()
    {
        moveAction?.Enable();
        attackAction?.Enable();
    }

    void OnDisable()
    {
        // always unsubscribe to avoid duplicates
        if (attackAction != null) attackAction.performed -= OnAttackPerformed;

        moveAction?.Disable();
        attackAction?.Disable();
    }

    public void EnableHitbox()  => hitbox.enabled = true;
    public void DisableHitbox() => hitbox.enabled = false;
    public void SpinStart()  => Hashit = false;
    public void SpinEnd() => Hashit = true;
}
