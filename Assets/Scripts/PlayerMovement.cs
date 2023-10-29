using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    Camera camera;

    private CharacterController controller;
    private Input_Manager inputManager;

    private Vector3 finalVelocity = Vector3.zero;
    private Vector3 followDirector = Vector3.zero;

    [SerializeField]
    private float velocity = 5f;

    [SerializeField]
    private float acceleration = 5f;

    [SerializeField]
    private float desacceleration = 5f;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float gravity = 20f;

    [SerializeField]
    private float jumpForce = 20f;

    [SerializeField]
    //private int maxJump = 2;
    private float inicialJump = 8f;

    private int jumpCounter = 0;

    //public float jumpDistance = 5;
    public float jumpTimer = 5;

    [SerializeField]
    private float coyoteTime = 1f;

    [SerializeField]
    private bool crouch;

    private void Awake()
    {
        //Bloquea cursor
        Cursor.lockState = CursorLockMode.Locked;


        controller = GetComponent<CharacterController>();
        inputManager = Input_Manager._INPUT_MANAGER;
    }

    public float GetCurrentSpeed()
    {
        return this.speed;
    }

    private void Update()
    {
        BasicMovement();

        MarioJump();

        Cruch();

    }

    void Start()
    {
        jumpForce = inicialJump;
    }

    private void Cruch()
    {

        if (inputManager.GetCrouchButtonPressed() && crouch == false)
        {
            crouch = true;
        }
        else if (!inputManager.GetCrouchButtonPressed() && crouch == true) //<- no hace falta pero como funciona no se toca
        {
            crouch = false;
        }
        if (crouch == true)
        {
            finalVelocity *= 0.5f;
            controller.height = 1f;

        }
        else
        {
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
                //Debug.Log("Salto");
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

    public Vector3 PlatformJump()
    {
        finalVelocity.y = 50f;
        return finalVelocity;
    }

    private void BasicMovement()
    {
        //Calcular dirección XZ
        //Vector3 direction = Quaternion.Euler(0f, camera.transform.eulerAngles.y, 0f) * new Vector3(inputManager.GetLeftAxis().x, 0f, inputManager.GetLeftAxis().y);
        Vector3 direction = Quaternion.Euler(0f, camera.transform.eulerAngles.y, 0f) * new Vector3(inputManager.GetLeftAxis().x, 0f, inputManager.GetLeftAxis().y);
        direction.Normalize();

        if (direction != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(direction, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, 0.5f * Time.deltaTime);
            gameObject.transform.forward = direction;

            speed += acceleration * Time.deltaTime;
            followDirector = direction;
        }
        else
        {
            speed -= desacceleration * Time.deltaTime;
           
            direction.x = followDirector.x;
            direction.y = followDirector.y;
        }

        speed = Mathf.Clamp(speed, 0f, velocity);

        //Calcular velocidad XZ
        finalVelocity.x = direction.x * speed;
        finalVelocity.z = direction.z * speed;

        direction.y = -1f;

        finalVelocity.y += direction.y * gravity * Time.deltaTime;

        controller.Move(finalVelocity * Time.deltaTime);


    }


}
