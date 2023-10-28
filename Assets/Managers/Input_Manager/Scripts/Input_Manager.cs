using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input_Manager : MonoBehaviour
{
    public static Input_Manager _INPUT_MANAGER;

    private PlayerInputActions playerInputs;

    private float timeSinceJumpPressed = 0f;
    private float timeSinceCrouchPressed = 0f;

    private bool jumpButtonPressed = false;
    private bool crouchButtonPressed = false;

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
            playerInputs.Character.Crouch.performed += CrouchButtonPressed;

            playerInputs.Character.Move.performed += LeftAxisUpdate;
            playerInputs.Character.Camera.performed += RightAxisUpdate;
            
            _INPUT_MANAGER = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Update()
    {
        jumpButtonPressed = false;
        crouchButtonPressed = false;

        timeSinceJumpPressed += Time.deltaTime;
        timeSinceCrouchPressed += Time.deltaTime;

        InputSystem.Update();
    }

    private void RightAxisUpdate(InputAction.CallbackContext context)
    {
        rightAxisValue = context.ReadValue<Vector2>();
    }

    private void LeftAxisUpdate(InputAction.CallbackContext context)
    {
        leftAxisValue = context.ReadValue<Vector2>();

        Debug.Log("Magnitude: " + leftAxisValue.magnitude);
        Debug.Log("Normalize: " + leftAxisValue.normalized);
    }

    private void JumpButtonPressed(InputAction.CallbackContext context)
    {
        jumpButtonPressed = true;
        timeSinceJumpPressed = 0f;
    }

    private void CrouchButtonPressed(InputAction.CallbackContext context)
    {
        crouchButtonPressed = true;
    }

    public Vector2 GetRightAxis() { return rightAxisValue; }
    public Vector2 GetLeftAxis() { return leftAxisValue; }

    public bool GetJumpButtonPressed() { return jumpButtonPressed; }
    public float GetJumpButtonPressedTime() {  return timeSinceJumpPressed; }

    public bool GetCrouchButtonPressed() { return crouchButtonPressed; }
    public float GetCrouchButtonPressedTime() {  return timeSinceCrouchPressed; }

}
