using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Player : MonoBehaviour
{
    private Input_Manager inputManager;

    private void Awake()
    {
        inputManager = Input_Manager._INPUT_MANAGER;
    }

    private void Update()
    {

        if (inputManager.GetJumpButtonPressed())
        {
            Debug.Log("Pressed-Jump");
        }


    }

}
