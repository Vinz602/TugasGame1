using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class DashMovement : MonoBehaviour
{
    public float jumpForce = 7f;
    public bool isGrounded;
    public int totalJump = 2;
    private Rigidbody2D rb;
     [HideInInspector] public int currentTotalJump = 0;

    private bool canDash = true;
    private bool isDashing;
    private float dashPower = 2000f;
    private float dashinTime = 0.5f;
    private float dashingCooldown = 0.5f;
    [SerializeField] private TrailRenderer tr;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        

        // if (Input.GetButtonDown("Jump") && isGrounded)
        // {
        //     rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        //     currentTotalJump++;
        //     if (currentTotalJump >= totalJump)
        //     {
        //         isGrounded= false;
        //     }
        // }

        if (isDashing)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
    }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.CompareTag("Ground"))
    //     {
    //         isGrounded = true;
    //         currentTotalJump = 0;
    //     }
    // }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Ground"))
    //    {
    //        isGrounded = false;
    //    }
    //}

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashinTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
