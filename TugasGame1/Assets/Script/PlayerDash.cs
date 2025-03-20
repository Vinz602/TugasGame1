using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private float horizontal;
    private float speed = 50f;
    private bool isFacingRight = true;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 200f;
    private float dashingTime = 0.5f;
    private float dashingCooldown = 1f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;

    private void Update()
    {
        if (isDashing)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        // if (Input.GetButtonDown("Jump") && IsGrounded())
        // {
        //     rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        // }

        // if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        // {
        //     rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        // }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        Flip();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        // rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    // private bool IsGrounded()
    // {
    //     return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    // }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        // rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        // rb.AddForce(new Vector2(speed * dashingPower, 0), ForceMode2D.Impulse);
        // rb.velocity = new Vector2(transform.localScale.x * speed, rb.velocity.y);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}