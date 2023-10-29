using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnimation : MonoBehaviour
{
    public float distance = 0.36f; // La distancia en la que se moverá el objeto hacia arriba y hacia abajo
    public float velocity= 2f;
    private Vector3 InitialPos;

    void Start()
    {
        InitialPos = transform.position;
    }

    private void Update()
    {
        transform.rotation *= Quaternion.Euler(0, 0.5f, 0);

        float newY = distance * Mathf.Sin(Time.time * velocity);
        transform.position = new Vector3(InitialPos.x, InitialPos.y + newY * 2, InitialPos.z);
    }
}
