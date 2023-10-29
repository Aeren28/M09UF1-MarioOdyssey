using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorPlayer : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement playerMovement;

    [SerializeField]
    private Animator animator;

    private void Update()
    {
       // animator.SetFloat("velocity", playerMovement.);
    }
}
