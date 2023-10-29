using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorPlayer : MonoBehaviour
{
    private PlayerMovement playerMovement;
    
    private Animator animator;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetFloat("velocity", playerMovement.GetCurrentSpeed());
    }
}
