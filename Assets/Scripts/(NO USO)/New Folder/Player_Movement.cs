using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    //public float horizontalMove;
    //public float verticalMove;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float rotationSpeed;

    public CharacterController controller;
    public Animator anim;

    private Vector3 moveDirection;
    private Vector3 Velocity;

    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isJumping;
    [SerializeField] private bool isMoving;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       Move();
    }

    private void Move()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        if(isGrounded && Velocity.y < 0)
        {
            Velocity.y = -2f;
        }

        float moveZ = Input.GetAxis("Vertical");
        float moveX = Input.GetAxis("Horizontal");

        moveDirection = new Vector3(moveX, 0, moveZ);
        moveDirection.Normalize();

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

        if (isGrounded)
        {
            anim.SetBool("IsGrounded", true);

            isGrounded = true;

            anim.SetBool("IsFalling", false);

            anim.SetBool("IsJumping", false);

            isJumping = false;

            if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("IsMoving", true);
                isMoving = true;

                Walk();

                Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
            else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("IsMoving", true);
                isMoving = true;

                Run();

                Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
            else if (moveDirection == Vector3.zero)
            {
                anim.SetBool("IsMoving", false);
                isMoving = false;

                Idle();
            }

            moveDirection *= moveSpeed;

            if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
               Velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            }

        } else
        {
            controller.stepOffset = 0;
            anim.SetBool("IsGrounded", false);
            isGrounded = false;

            anim.SetBool("IsJumping", true);
            isJumping = true;

            if (isJumping)
            {
                anim.SetBool("IsFalling", true);
            }
        }

        controller.Move(moveDirection * Time.deltaTime);

        Velocity.y += gravity * Time.deltaTime;

        controller.Move(Velocity * Time.deltaTime);

        
    }

    private void Idle()
    {
        anim.SetFloat("IdleSpeed", 0, 0.1f, Time.deltaTime);
    }

    private void Walk()
    {
        moveSpeed = walkSpeed;
        anim.SetFloat("IdleMove", 0, 0.1f, Time.deltaTime);
    }

    private void Run()
    {
        moveSpeed = runSpeed;
        anim.SetBool("IsMoving", true);
        isMoving = true;
        anim.SetFloat("IdleMove", 1, 0.1f, Time.deltaTime);
    }

    private void Jump()
    {
        Velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
    }
}
