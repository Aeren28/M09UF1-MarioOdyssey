using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CappyMario : MonoBehaviour
{
    public GameObject Cappy;
    public CharacterController Controller;

    public Transform CappySpace;
    public float ThrowTime = 1f;
    public float ReturnSpeed = 1f;
    public float ThrowDistance = 3f;
    public float RotationSpeed = 2f;


    private Vector3 direction;
    private bool Return = true;
    private bool Throw = false;

    private void OnEnable()
    {
        Cappy.transform.position = CappySpace.transform.position;
    }
    /*
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && Return == false)
        {
            StartCoroutine(CapThrow());
        }

        if (Return)
        {
            direction = (CappySpace.position - Cappy.transform.position).normalized;
            Controller.Move(direction * ReturnSpeed * Time.deltaTime);

            if (Vector3.Distance(CappySpace.position, Cappy.transform.position) < 1.25F)
            {
                SetParent();
                Cappy.transform.rotation = this.transform.rotation * Quaternion.Euler(15, 0, 5);
            }
        }
    }
    */
    public IEnumerator CapThrow()
    {
        Throw = true;
        Cappy.transform.SetParent(null);

        Vector3 Forward = transform.forward;
        Vector3 targetPosition = Cappy.transform.position + Forward * ThrowDistance;
        float step = 0;
        while (step < 1)
        {
            step += Time.deltaTime / ThrowTime;
            Cappy.transform.position = Vector3.Lerp(Cappy.transform.position, targetPosition, step);
            Cappy.transform.Rotate(0, RotationSpeed * Time.deltaTime, 0);
            yield return null;
        }
        ReturnCappy();
    }

    public IEnumerator ReturnCappy()
    {
        Return = true;
        direction = (CappySpace.position - Cappy.transform.position).normalized;
        float step = 0;
        while (step < 1)
        {
            step += Time.deltaTime / ThrowTime;
            Cappy.transform.position = Vector3.Lerp(Cappy.transform.position, CappySpace.position, step);
            yield return null;
        }
        SetParent();
    }

    public void SetParent()
    {
        Throw = false;
        Return = false;
        Cappy.transform.position = CappySpace.position;
        Cappy.transform.SetParent(this.transform);
    }

}
