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

    Vector2 respawnPoint; // Where the player will respawn

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position; // Setting the player's respawn point
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Death"))
        {
            transform.position = respawnPoint; // Teleport the player back to spawn when they touch the death zone
        }
    }

    bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

}
