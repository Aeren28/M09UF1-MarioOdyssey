using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float targetDistance;

    [SerializeField]
    private float cameraLerp;

    private float rotationX;
    private float rotationY;

    private void LateUpdate()
    {
        rotationX += Input.GetAxis("Mouse Y");
        rotationY += Input.GetAxis("Mouse X");

        rotationX = Mathf.Clamp(rotationX, -50f, 50f);

        transform.eulerAngles = new Vector3(rotationX, rotationY, 0);

        //Vector3 finalPosition = Vector3.Lerp(transform.position, target.position - transform.forward * targetDistance, cameraLerp * Time.deltaTime);
        Vector3 finalPosition = Vector3.Lerp(transform.position, target.position - transform.forward * targetDistance, 1);


        transform.eulerAngles = new Vector3(rotationX, rotationY, 0);

        transform.position = target.transform.position - transform.forward * targetDistance;
    }

}
