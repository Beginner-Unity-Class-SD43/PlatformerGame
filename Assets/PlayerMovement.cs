using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float horizontal; // Horizontal input variable

    [SerializeField] float speed = 8f;
    [SerializeField] float jumpPower = 20f;

    bool isFacingRight = true;

    Rigidbody2D rb;

    [SerializeField] Transform groundCheck; // Ground check
    [SerializeField] LayerMask groundLayer; // Ground layer

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal"); // This gets AD or arrow key input (left and right)

        if (Input.GetButtonDown("Jump") && CheckIfGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }



        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    void Flip() // Flips the player
    {
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f) // || means "or" 
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

}
