using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Input_Manager inputManager;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float targetDistance = 15f;

    [SerializeField]
    private float cameraLerp;

    private float rotationX;
    private float rotationY;

    private void Awake()
    {
        inputManager = Input_Manager._INPUT_MANAGER;
    }
    private void LateUpdate()
    {
        rotationX += inputManager.GetRightAxis().y;
        rotationY += inputManager.GetRightAxis().x;

        rotationX = Mathf.Clamp(rotationX, -50f, 50f);

        transform.eulerAngles = new Vector3(rotationX, rotationY, 0);

        //Vector3 finalPosition = Vector3.Lerp(transform.position, target.position - transform.forward * targetDistance, cameraLerp * Time.deltaTime);
        Vector3 finalPosition = Vector3.Lerp(transform.position, target.position - transform.forward * targetDistance, 1);


        transform.position = target.transform.position - transform.forward * targetDistance;

        RaycastHit hit;
        if(Physics.Linecast(target.transform.position, finalPosition, out hit)){
            finalPosition = hit.point;
        }

        transform.position = finalPosition;

    }

}
