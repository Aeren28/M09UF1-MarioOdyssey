using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input_Manager : MonoBehaviour
{
    public static Input_Manager _INPUT_MANAGER;

    private PlayerInputActions playerInputs;

    private float timeSinceJumpPressed = 0f;
    private Vector2 leftAxisValue = Vector2.zero;

    private bool jumpButtonPressed = false;
    private bool cruchButtonPressed = false;

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
            playerInputs.Character.Move.performed += leftAxisUpdate;
            playerInputs.Character.Crouch.performed += CruchButtonPressed;


            _INPUT_MANAGER = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Update()
    {
        timeSinceJumpPressed += Time.deltaTime;

        jumpButtonPressed = false;
        cruchButtonPressed = false;

        InputSystem.Update();
    }

    private void JumpButtonPressed(InputAction.CallbackContext context)
    {
        jumpButtonPressed = true;
        timeSinceJumpPressed = 0f;
    }

    private void leftAxisUpdate(InputAction.CallbackContext context)
    {
        leftAxisValue = context.ReadValue<Vector2>();

        Debug.Log("Magnitude: " + leftAxisValue.magnitude);
        Debug.Log("Normalize: " + leftAxisValue.normalized);
    }

    private void CruchButtonPressed(InputAction.CallbackContext context)
    {
        cruchButtonPressed = true;
    }
    public bool GetJumpButtonPressed()
    {
        return jumpButtonPressed;
    }

    public float GetJumpButtonPressedTime()
    {
        return timeSinceJumpPressed;
    }

    public bool GetCruchButtonPressed()
    {
        return cruchButtonPressed;
    }
    public Vector2 GetLeftAxisValue()
    {
        return leftAxisValue;
    }

}
