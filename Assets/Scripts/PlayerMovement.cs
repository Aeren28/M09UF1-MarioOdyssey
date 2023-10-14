using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    private Vector3 finalVelocity = Vector3.zero;

    [SerializeField]
    private float velocity = 5f;

    [SerializeField]
    private float gravity = 20f;

    [SerializeField] 
    private float jumpForce = 8f;

    [SerializeField]
    private float coyoteTime = 1f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        //Calcular dirección XZ
        Vector3 direction = Input.GetAxis("Vertical") * transform.forward + Input.GetAxis("Horizontal") * transform.right;
        direction.Normalize();

        //Calcular velocidad XZ
        finalVelocity.x = direction.x * velocity;
        finalVelocity.z = direction.z * velocity;

        //Asignar dirección Y
        direction.y = -1f;

        //Calcular gravedad
        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                finalVelocity.y = jumpForce;
            }
            else
            {
                finalVelocity.y = direction.y * gravity * Time.deltaTime;
                coyoteTime = 1f;
            }

        }
        else
        {
            finalVelocity.y += direction.y * gravity * Time.deltaTime;

            coyoteTime -= Time.deltaTime;
            if (Input.GetKey(KeyCode.Space) && coyoteTime >= 0f)
            {
                finalVelocity.y = jumpForce;
                coyoteTime = 0f;

            }

        }

        controller.Move(finalVelocity * Time.deltaTime);

    }

}
