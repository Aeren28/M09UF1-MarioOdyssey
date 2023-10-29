using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input_Manager : MonoBehaviour
{
    public static Input_Manager _INPUT_MANAGER;

    private PlayerInputActions playerInputs;

    private float timeSinceJumpPressed = 0f;
    private float timeSinceCappyPressed = 0f;

    private bool crouch;

    private Vector2 leftAxisValue = Vector2.zero;
    private Vector2 rightAxisValue = Vector2.zero;

    private void Awake()
    {
        // Compruebo existencia de instancias al input manager

        if (_INPUT_MANAGER != null && _INPUT_MANAGER != this) // Si existe un input manager y no soy yo
        {
            Destroy(this.gameObject); // Destruyelo
        }
        else
        {
            // Genero instancia y activo character sheme
            playerInputs = new PlayerInputActions();
            playerInputs.Character.Enable();

            //Delegates
            playerInputs.Character.Jump.performed += JumpButtonPressed;

            playerInputs.Character.Crouch.started += CrouchButtonPressed;
            playerInputs.Character.Crouch.canceled += CrouchButtonPressedReleased;

            playerInputs.Character.Cappy.performed += CappyButtonPressed;
            playerInputs.Character.Move.performed += LeftAxisUpdate;
            playerInputs.Character.Camera.performed += RightAxisUpdate;
            
            _INPUT_MANAGER = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Update()
    {

        timeSinceCappyPressed += Time.deltaTime;
        timeSinceJumpPressed += Time.deltaTime;

        InputSystem.Update();
    }

    private void RightAxisUpdate(InputAction.CallbackContext context)
    {
        rightAxisValue = context.ReadValue<Vector2>();
    }

    private void LeftAxisUpdate(InputAction.CallbackContext context)
    {
        leftAxisValue = context.ReadValue<Vector2>();

        //Debug.Log("Magnitude: " + leftAxisValue.magnitude);
        //Debug.Log("Normalize: " + leftAxisValue.normalized);
    }

    private void JumpButtonPressed(InputAction.CallbackContext context)
    {
      
        timeSinceJumpPressed = 0f;
    }

    private void CrouchButtonPressed(InputAction.CallbackContext context)
    {
        crouch = true;
    }

    private void CrouchButtonPressedReleased(InputAction.CallbackContext context)
    {
        crouch = false;
    }

    private void CappyButtonPressed(InputAction.CallbackContext context)
    {
       timeSinceCappyPressed = 0f;
    }

    public Vector2 GetRightAxis() { return rightAxisValue; }
    public Vector2 GetLeftAxis() { return leftAxisValue.normalized; }

    public bool GetJumpButtonPressed() { return timeSinceJumpPressed <= 0.2f; }


    public bool GetCappyButtonPresed() { return timeSinceCappyPressed <= 0.2f;  }


    public bool GetCrouchButtonPressed() { return crouch; }
    

}
