using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Cappy : MonoBehaviour
{
    private Input_Manager inputManager;

    [SerializeField] private GameObject player;
    [SerializeField] private float distance = 3f; 

    private float yPosition = 5f;

    private bool throwCappy = false;
    private float timer = 5f;
    private float stopCappy = 0f;

    private void Awake()
    {
        inputManager = Input_Manager._INPUT_MANAGER;

    }

    private void Update()
    {

        if (inputManager.GetCappyButtonPresed())
        {
            throwCappy = true;
        }

        if (throwCappy)
        {
            if (stopCappy <= 1)
            {
                transform.position = transform.position + transform.forward * distance * Time.deltaTime;
            }

            stopCappy += Time.deltaTime;

            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                throwCappy = false;
                timer = 10f;
                stopCappy = 0;

            }
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y + yPosition, player.transform.position.z);
        }

    }
}
