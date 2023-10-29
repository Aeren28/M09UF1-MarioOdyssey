using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformReaction : MonoBehaviour
{
    private GameObject player;

    private PlayerMovement move;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        move = player.GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        move.PlatformJump();
    }

}
