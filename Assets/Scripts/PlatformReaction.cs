using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformReaction : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement move;


    private void OnTriggerEnter(Collider other)
    {
        move.PlatformJump();
    }

}
