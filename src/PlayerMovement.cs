using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float rotationSpeed = 10f; 
    [SerializeField] private float gravity = 9.8195f;
    [SerializeField] private float jumpHeight = 2f;

    private Animator animator;
    
    private Vector3 velocity;
    private bool isGrounded;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            animator.SetBool("IsFall", false);
        }
        else
        {
            animator.SetBool("IsFall", true);
        }
        

        float moveX = Input.GetAxis("Horizontal");//A D
        float moveZ = Input.GetAxis("Vertical"); // W S 

        Vector3 camForward = cameraTransform.forward;
        camForward.y = 0; 
        camForward.Normalize();

        Vector3 camRight = cameraTransform.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 moveDirection = camRight * moveX + camForward * moveZ;
        
        
        animator.SetFloat("MoveX", moveX);
        animator.SetFloat("MoveY", moveZ);
        
        moveDirection.Normalize();

       
        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;
        
        animator.SetFloat("Speed", speed);
        
        controller.Move(moveDirection * (speed * Time.deltaTime));
        
        
        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * 2f * gravity);
        }
        
        velocity.y -= gravity * Time.deltaTime;
        
        controller.Move(velocity * Time.deltaTime);
    }
}
