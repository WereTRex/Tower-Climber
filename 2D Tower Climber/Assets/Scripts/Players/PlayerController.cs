using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed, jumpSpeed;

    [SerializeField] Transform graphics;

    //Ground check varaibles
    [SerializeField] LayerMask groundLayers;
    [SerializeField] Transform groundCheck;
    float groundCheckRadius = 0.2f;

    //Rigidbody & velocity variables
    Rigidbody2D rb;
    [SerializeField] [Range(0, 0.3f)] float movementSmoothing = 0.05f;

    //Flip Variables
    bool facingRight;

    //New input system variables
    float movementInput;

    //Animation Variables
    bool isJumping;
    [SerializeField] bool dead = false;

    #region "New Input System stuff"
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<float>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (dead) { return; }
        Jump();
    }
    #endregion


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (dead) {
            if (IsGrounded()) {
                rb.velocity = Vector2.zero;
            }
            return;
        }

        Vector3 m_Velocity = Vector3.zero;

        // Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector2(movementInput * speed, rb.velocity.y);
        // And then smoothing it out and applying it to the character
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, movementSmoothing);

        //Flip the player
        if (movementInput > 0 && facingRight)
        {
            Flip();
        } else if (movementInput < 0 && !facingRight)
        {
            Flip();
        }
    }

    void Jump()
    {
        if (IsGrounded())
        {
            rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);

            StartCoroutine(SetIsJumping());
        }
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayers);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    private void Flip()
    {
        //Flip the player facing variable
        facingRight = !facingRight;

        //Flip the player by multiply their local x scale by -1
        Vector3 playerScale = graphics.localScale;
        playerScale.x *= -1;
        graphics.localScale = playerScale;
    }

    IEnumerator SetIsJumping()
    {
        isJumping = true;

        //Triggered when they get off the ground
        yield return new WaitUntil(() => (rb.velocity.y > 0 || !IsGrounded()));
        //Triggered when they land
        yield return new WaitUntil(() => (IsGrounded() == true));

        isJumping = false;
    }

    public void Die()
    {
        dead = true;
        
    }


    public bool GetIsJumping()
    {
        return isJumping;
    }

    public bool GetIsDead()
    {
        return dead;
    }

    public float GetMovementInput()
    {
        return movementInput;
    }
}
