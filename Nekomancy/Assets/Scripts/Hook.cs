using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//holds information needed for string wrapping around objects

public class Hook : MonoBehaviour
{
    //The Hit Position of the Projectile logged to the hook
    public Vector2 InitialHookTeather;

    private Rigidbody2D rb;
    private BoxCollider2D playerCollider;

    [SerializeField]
    private float distanceToTeather, minPullDistance;
    [SerializeField]
    private float maxSpeed, hookAccel;

    private Vector2 teatherBase;
    private Stack<Vector2> tPoints;
    private Stack<Vector2> tPointPerps;

    [SerializeField]
    private bool swinging = false, pulling = false;

    public bool Swinging
    { get { return swinging; } }

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
        if (swinging)
        {
            //Do Input test if Swining
            if (Input.GetKeyDown(KeyCode.E))
            {
                pulling = !pulling;
            }

            Debug.DrawLine(transform.position, tPoints.Peek());
            Vector2 toTeather = tPoints.Peek() - (Vector2)transform.position;

            float speed = rb.velocity.magnitude;
            rb.AddForce(hookAccel * Time.deltaTime * toTeather);
            //Vector2 teatherPerp = Vector2.Perpendicular(toTeather).normalized;
            ////Swing in circular motion by keeping velocity tangent to the teather
            //rb.velocity = Vector2.Dot(teatherPerp, rb.velocity) * teatherPerp;
            //transform.position = tPoints.Peek() - toTeather.normalized * distanceToTeather;

            //if (pulling)
            //{
            //    distancetoteather = mathf.lerp(distancetoteather, 0, time.deltatime);
            //    if (tpoints.count > 1 &&  minpulldistance > vector2.distance(tpoints.peek(), (vector2)transform.position))
            //    {
            //        unteather();
            //    }
            //}


            //Check for object wrapping
            playerCollider.enabled = !playerCollider.enabled;
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, toTeather, toTeather.magnitude - .05f);
            playerCollider.enabled = !playerCollider.enabled;
            if (hitInfo.collider != null)
            {
                Teather(hitInfo.point);
            }

            //check for object unwrapping
            if (tPointPerps.Count != 0)
            {
                Debug.DrawLine(tPointPerps.Peek(), tPoints.Peek());
                if (Vector2.Dot(toTeather, tPointPerps.Peek()) > 0 || toTeather.sqrMagnitude < 5.25f)
                {
                    Unteather();
                }
            }
            
            if (toTeather.sqrMagnitude < 5.25f)
            {
                Unteather();
            }
            
        }
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            Clear();
        }
    }

    public void Teather(Vector2 location)
    {
        //before teathering check to see if a previous teather point is close to the new teather point
        if (tPoints.Count > 1)
        {
            Vector2 current = tPoints.Pop();
            if ((tPoints.Peek() - location).magnitude < .3f)
            {
                tPoints.Push(current);
                Unteather();
                return;
            }
            tPoints.Push(current);
        }
        //for the first teather continually ray cast and adjust the position of teather points unitl
        //the ray reaches the player
        else
        {
            swinging = true;
            List<Vector2> points = new List<Vector2>
            {
                new Vector2(1.1f,1.1f),
                new Vector2(1.1f,-1.1f)
            };
            RaycastHit2D hit = Physics2D.Raycast(location, transform.position, (location - (Vector2)transform.position).magnitude);
            if (hit.collider != null)
            {
                Collider2D boxCol = hit.collider;
                Vector2 inbetweenPoint = (Vector2)boxCol.transform.position + new Vector2(boxCol.bounds.extents.x * Mathf.Sign(location.x) + 1, boxCol.bounds.extents.y + 1);
                tPoints.Push(inbetweenPoint);
            }
            tPoints.Push(location);
            teatherBase = location;
        }

        if (tPoints.Count > 0)
        {
            Vector2 perp = Vector2.Perpendicular(location - tPoints.Peek());
            tPointPerps.Push(Vector2.Dot(perp, rb.velocity) * perp);
        }
        tPoints.Push(location);
        distanceToTeather = (location - (Vector2)transform.position).magnitude;
    }

    void Unteather()
    {
        tPoints.Pop();
        distanceToTeather = (tPoints.Peek() - (Vector2)transform.position).magnitude;
        if (tPointPerps.Count > 0)
        {
            tPointPerps.Pop();
        }
        if(tPoints.Count == 0)
        {
            swinging = false;
        }
    }

    public void Clear()
    {
        tPoints = new Stack<Vector2>();
        tPointPerps = new Stack<Vector2>();
        swinging = false;
    }
}
