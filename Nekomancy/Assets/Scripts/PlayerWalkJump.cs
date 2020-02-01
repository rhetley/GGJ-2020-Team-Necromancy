using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkJump : MonoBehaviour
{
    public Vector2 velocity;
    float moveMultiplier = 10.0f;
    float jumpHeight = 5.0f;
    Rigidbody2D playerRigidbody;
    private GroundCheck groundChecker;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        groundChecker = GetComponent<GroundCheck>();

        Physics2D.gravity = new Vector2(0.0f, -98.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && groundChecker.isGrounded)
        {
            Jump();
        }
        else
        {
            velocity.x = Input.GetAxis("Horizontal");
            if (groundChecker.isAgainstRight && velocity.x > 0)
            {
                velocity.x = 0;
            }
            else if (groundChecker.isAgainstLeft && velocity.x < 0)
            {
                velocity.x = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        playerRigidbody.velocity = new Vector2(velocity.x * moveMultiplier, GetComponent<Rigidbody2D>().velocity.y);
    }

    void Jump()
    {
        velocity.y = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y));
        playerRigidbody.velocity = new Vector2(velocity.x, velocity.y);
        velocity.y += Physics2D.gravity.y * Time.deltaTime;
        playerRigidbody.velocity = new Vector2(velocity.x, velocity.y);
    }
}

