using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    Camera camera;

    private CharacterController controller;

    private Vector3 finalVelocity = Vector3.zero;

    [SerializeField]
    private float velocity = 5f;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float gravity = 20f;

    [SerializeField] 
    private float jumpForce;

    [SerializeField]
    private float inicialJump;

    private int jumpCounter = 0;

    public float jumpTimer = 0;

    [SerializeField]
    private float coyoteTime = 1f;

    private void Awake()
    {
        //Bloquea cursor
        Cursor.lockState = CursorLockMode.Locked;


        controller = GetComponent<CharacterController>();

    }

    void Start()
    {
        jumpForce = inicialJump;
    }

    private void Update()
    {
        BasicMovement();

        MarioJump();     

    }

    private void MarioJump()
    {
        //Calcular gravedad
        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                finalVelocity.y = jumpForce;

                jumpCounter++;

                jumpTimer = 0.5f;
                jumpForce *= 1.5f;

                if (jumpCounter >= 3)
                {
                    jumpCounter = 0;
                    jumpForce = inicialJump;
                }

            }
            else
            {
                finalVelocity.y = -gravity * Time.deltaTime;
                coyoteTime = 1f;

                if (jumpTimer <= 0)
                {
                    jumpForce = inicialJump;
                    jumpCounter = 0;
                }
            }

        }
        else
        {
            finalVelocity.y += -gravity * Time.deltaTime;

            coyoteTime -= Time.deltaTime;
        }
    }

    private void BasicMovement()
    {
        //Calcular dirección XZ
        Vector3 direction = Quaternion.Euler(0f, camera.transform.eulerAngles.y, 0f) * new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        direction.Normalize();

        if (direction != Vector3.zero && speed <= velocity)
        {

            speed += velocity * Time.deltaTime;
        }
        else if (direction == Vector3.zero && speed > 0)
        {
            speed -= velocity * Time.deltaTime;
        }

        //Calcular velocidad XZ
        finalVelocity.x = direction.x * speed;
        finalVelocity.z = direction.z * speed;

        controller.Move(finalVelocity * Time.deltaTime);

        if (direction != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(direction, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, 0.5f * Time.deltaTime);
            gameObject.transform.forward = direction;
        }


    }


}
