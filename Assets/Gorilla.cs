using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gorilla : MonoBehaviour
{
    Animator Anicontroller;
    InputAction moveAction;
    InputAction attackAction;
    Rigidbody rb;
    [SerializeField] float MoveSpeed = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Anicontroller = GetComponent<Animator>();
        moveAction = InputSystem.actions.FindAction("Move");
        attackAction = InputSystem.actions.FindAction("Jump");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        Vector3 movement = new Vector3(moveValue.x, 0f, moveValue.y) * MoveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        if(moveAction.IsPressed()){
            transform.forward = new Vector3 (moveValue.x, 0f, moveValue.y);
            Anicontroller.SetBool("IsWalking", true);
        }
        else{
            Anicontroller.SetBool("IsWalking", false);
        }
        if(attackAction.IsPressed()){
            Spinattack();
        }
    }
    void Spinattack()
    {
        Debug.Log("Spin!");
        return;
    }
}
