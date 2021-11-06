using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;



    void Update()
    {
        //Running
        if ((playerController.GetMovementInput() > 0 || playerController.GetMovementInput() < 0) && playerController.IsGrounded())
        {
            animator.SetBool("Running", true);
        } else {
            animator.SetBool("Running", false);
        }

        //Jumping
        animator.SetBool("Jumping", playerController.GetIsJumping());

        //Falling
        animator.SetBool("Falling", (rb.velocity.y < 0 || !playerController.IsGrounded()));

        //Check if grounded
        animator.SetBool("Grounded", playerController.IsGrounded());

        //Attacking


        //Damaged


        //Dead
        if (!animator.GetBool("Dead"))
        {
            if (playerController.GetIsDead() && !animator.GetBool("Hurt"))
            {
                animator.SetTrigger("Hurt");
                animator.SetBool("Dead", true);
            }
            else if (playerController.GetIsDead())
            {
                animator.Play("Death");
            }
        }
    }
}
