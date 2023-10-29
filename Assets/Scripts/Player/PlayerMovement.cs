using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    Camera camera;

    [SerializeField]
    private Animator animator;

    private CharacterController controller;
    private Input_Manager inputManager;

    [SerializeField]
    private UiGameOver gameover;

    [SerializeField]
    private UiYouWin youWin;

    private Vector3 finalVelocity = Vector3.zero;
    private Vector3 followDirector = Vector3.zero;
    private Vector3 direction = Vector3.zero;

    public LayerMask wallLayer;

    [SerializeField]
    private float velocity = 8f;

    [SerializeField]
    private float acceleration = 5f;

    [SerializeField]
    private float desacceleration = 7f;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float gravity = 20f;

    [SerializeField]
    private float jumpForce = 20f;

    [SerializeField]
    private float wallJumpForce = 10f;

    [SerializeField]
    private bool isTouchingWall = false;

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

    private void Update()
    {
        BasicMovement();

        MarioJump();

        Cruch();

        WallJump();


        controller.Move(finalVelocity * Time.deltaTime);

    }

    void Start()
    {
        gameover.Hide();
        youWin.HideWin();
        jumpForce = inicialJump;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Walljump"))
        {
            isTouchingWall = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Walljump"))
        {
            isTouchingWall = false;
        }
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

    private void WallJump()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.right, out hit, 1f, wallLayer) || Physics.Raycast(transform.position, -transform.right, out hit, 1f, wallLayer))
        {
            isTouchingWall = true;
        }
        else
        {
            isTouchingWall = false;
        }

        if (inputManager.GetJumpButtonPressed() && isTouchingWall)
        {
            finalVelocity.y = jumpForce;
            finalVelocity.x = direction.x * wallJumpForce;
            jumpCounter++;
            jumpTimer = 0.5f;
            jumpForce *= 1.5f;
            if (jumpCounter >= 3)
            {
                jumpCounter = 0;
                jumpForce = inicialJump;
            }
        }
    }

    public void PlatformJump()
    {
        finalVelocity.y = 50f;
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

        if (speed == 0f)
        {
         
            animator.SetBool("walking", false);
            animator.SetBool("running", false) ;

        }
        else if(speed < velocity / 2 && velocity != 0f)
        {

            animator.SetBool("walking", true);
            animator.SetBool("running", false);

        }
        else if(speed > velocity / 2)
        {

            animator.SetBool("walking", false );
            animator.SetBool("running", true) ;
            
        }


    }


}
