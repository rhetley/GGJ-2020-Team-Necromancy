using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempController : MonoBehaviour
{
    public enum State
    {
        Grounded,
        Falling,
        Swinging,
    }

    private State playerState;

    [SerializeField]
    private Vector2 horizInput;

    private Rigidbody2D rb;
    private BoxCollider2D playerCollider;

    private float distanceToTeather;
    private Stack<Vector2> tPoints;
    private Stack<Vector2> tPointPerps;

    //player movement feilds
    [SerializeField]
    private float maxGroundAcceleration = 5000;
    [SerializeField]
    private float maxGroundSpeed = 2;
    [SerializeField]
    private float jumpForce = 200;
    private float maxAirAcceleration = 200;
    private float maxAirSpeed = 2;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        tPoints = new Stack<Vector2>();
        tPointPerps = new Stack<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        horizInput = new Vector2(Input.GetAxisRaw("Horizontal"), 0);

        switch (playerState)
        {
            //Ground movement
            case State.Grounded:
                rb.AddForce(horizInput * maxGroundAcceleration * Time.deltaTime);
                //rb.velocity = ClampX(rb.velocity, maxGroundSpeed);
                if (Input.GetAxis("Jump") > 0.5f)
                {
                    rb.AddForce(new Vector2(0, jumpForce));
                    playerState = State.Falling;
                }
                break;
            //Air movement
            case State.Falling:
                rb.AddForce(horizInput * maxAirAcceleration * Time.deltaTime);
                //have the player fall faster than they jump to make jumping feel less floaty
                if (rb.velocity.y < 0)
                {
                    rb.AddForce(new Vector2(0, -7f * Time.deltaTime));
                }
                //rb.velocity = ClampX(rb.velocity, maxAirSpeed);

                break;
        }
    }
    

    void OnCollisionEnter2D(Collision2D collision)
    {
        //check to see if the collider is a platfrom, and make sure it didn't collide with the side of the platfrom
        if (playerCollider.bounds.min.y >= collision.collider.bounds.max.y - .05f)
        {
            playerState = State.Grounded;
        }
    }

    //clamps only the x axis of a vector
    Vector2 ClampX(Vector2 clampThis, float maxVal)
    {
        if (Mathf.Abs(clampThis.x) > maxVal)
        {
            return new Vector2(maxVal * Mathf.Sign(clampThis.x), clampThis.y);
        }
        else
        {
            return clampThis;
        }
    }

}
