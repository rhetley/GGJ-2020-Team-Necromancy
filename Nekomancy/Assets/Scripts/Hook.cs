using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//holds information needed for string wrapping around objects

public class Hook : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D playerCollider;

    private float distanceToTeather;
    private Vector2 teatherBase;
    private Stack<Vector2> tPoints;
    private Stack<Vector2> tPointPerps;

    private bool swinging = false;

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

            Debug.DrawLine(transform.position, tPoints.Peek());
            //Swing in circular motion by keeping velocity tangent to the teather
            Vector2 toTeather = tPoints.Peek() - (Vector2)transform.position;
            Vector2 teatherPerp = Vector2.Perpendicular(toTeather).normalized;
            rb.velocity = Vector2.Dot(teatherPerp, rb.velocity) * teatherPerp;            
            transform.position = tPoints.Peek() - toTeather.normalized * distanceToTeather;

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
                if (Vector2.Dot(toTeather, tPointPerps.Peek()) >  0)
                {
                    Unteather();
                }
            }
        }

        //TEMP use mouse to teather anywhere
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (swinging)
            {
                swinging = false;
                Clear();
            }
            else
            {
                //TEMP teather at the mouse position
                Teather(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                swinging = true;
            }
        }
    }

    void Teather(Vector2 location)
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
        else
        {
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
        tPointPerps.Pop();
    }

    void Clear()
    {
        tPoints = new Stack<Vector2>();
        tPointPerps = new Stack<Vector2>();
    }
}
