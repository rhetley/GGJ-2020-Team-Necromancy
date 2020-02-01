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
    private float maxAirAcceleration = 2500;
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
                rb.velocity = ClampX(rb.velocity, maxGroundSpeed);
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
                rb.velocity = ClampX(rb.velocity, maxAirSpeed);

                break;
            //Movement while swinging
            case State.Swinging:

                //Swing in circular motion by keeping velocity tangent to the teather
                Vector2 toTeather = tPoints.Peek() - (Vector2)transform.position;
                Vector2 teatherPerp = Vector2.Perpendicular(toTeather).normalized;
                rb.velocity = Vector2.Dot(teatherPerp, rb.velocity) * teatherPerp;
                transform.position = tPoints.Peek() - toTeather.normalized * distanceToTeather;

                //Check for object wrapping
                playerCollider.enabled = !playerCollider.enabled;
                RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, toTeather, toTeather.magnitude - .001f);
                playerCollider.enabled = !playerCollider.enabled;
                if (hitInfo.collider != null)
                {
                    Teather(hitInfo.point);
                }

                //check for object unwrapping
                if (tPointPerps.Count != 0)
                {
                    Debug.DrawLine(tPoints.Peek(), tPointPerps.Peek(), Color.white);
                    if (Vector2.Dot(toTeather, tPointPerps.Peek()) > 0)
                    {
                        //Unteather();
                    }
                }

                break;
        }

        //TEMP use mouse to teather anywhere
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (playerState == State.Swinging)
            {
                playerState = State.Falling;
            }
            else
            {
                //TEMP teather at the mouse position
                Teather(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
        }
    }

    void Teather(Vector2 location)
    {
        if (tPoints.Count > 0)
        {
            tPointPerps.Push(Vector2.Perpendicular(location - tPoints.Peek()));
        }
        tPoints.Push(location);
        distanceToTeather = (location - (Vector2)transform.position).magnitude;
        playerState = State.Swinging;
    }

    void Unteather()
    {
        tPoints.Pop();
        tPointPerps.Pop();
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
