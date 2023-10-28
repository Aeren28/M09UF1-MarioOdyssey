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
    private Vector3 followDirecction = Vector3.zero;

    private Input_Manager inputManager;

    [SerializeField]
    private float velocity = 0f;

    [SerializeField] 
    private float maxVelocity = 5f;

    [SerializeField] 
    private float acceleration = 2.5f;

    [SerializeField] 
    private float decceleration = 2.5f;

    [SerializeField]
    private float gravity = 20f;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private float inicialJump;

    private int jumpCounter = 0;

    public float jumpTimer = 0;

    [SerializeField]
    private bool crouch;


    private void Awake()
    {
        //Bloquea cursor
        Cursor.lockState = CursorLockMode.Locked;

        controller = GetComponent<CharacterController>();
        inputManager = Input_Manager._INPUT_MANAGER;
    }

    void Start()
    {
        jumpForce = inicialJump;
    }

    private void Update()
    {
        BasicMovement();

        MarioJump();

        Cruch();

    }

    private void Cruch()
    {
        if (inputManager.GetCruchButtonPressed())
        {

            finalVelocity *= 0.5f;
            controller.height = 1f;

            crouch = true;
        }
        else
        {
            crouch = false;
            controller.height = 2f;
        }

    }
    private void MarioJump()
    {
        //Calcular gravedad
        if (controller.isGrounded)
        {
            if (inputManager.GetJumpButtonPressed())
            {
                finalVelocity.y = jumpForce;

                jumpCounter++;

                jumpTimer = 0.5f;
                jumpForce *= 2f;

                if (jumpCounter >= 3)
                {
                    jumpCounter = 0;
                    jumpForce = inicialJump;
                }

            }
            else
            {
                finalVelocity.y = -gravity * Time.deltaTime;
        

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


        }
    }

    private void BasicMovement()
    {

        //Calcular dirección XZ
        Vector3 direction = Quaternion.Euler(0f, camera.transform.eulerAngles.y, 0f) * new Vector3(inputManager.GetLeftAxisValue().x, 0f, inputManager.GetLeftAxisValue().y);
        direction.Normalize();

        if (direction != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(direction, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, 0.5f * Time.deltaTime);
            gameObject.transform.forward = direction;

            velocity += acceleration * Time.deltaTime;
            followDirecction = direction;

        }
        else
        {
            velocity -= decceleration * Time.deltaTime;
            direction.x = followDirecction.x;
            direction.z = followDirecction.z;
        }

        //Calcular velocidad XZ
        finalVelocity.x = direction.x * velocity;
        finalVelocity.z = direction.z * velocity;

        controller.Move(finalVelocity * Time.deltaTime);


    }
}

