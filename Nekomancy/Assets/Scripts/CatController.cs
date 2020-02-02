using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    [SerializeField]
    private GameObject pointToSeek;
    private Vector2 locationWhenInZone;
    private bool moveDefault;
    private bool inStopZone;
    private Rigidbody2D rigidbody;
    private float exitTimeStamp = -1f;
    private float lerpTime = 0.5f;

    private bool facingRight;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        moveDefault = true;
        inStopZone = false;
        facingRight = true;
    }

    private void FixedUpdate()
    {
        if(moveDefault && !inStopZone)
        {
            rigidbody.MovePosition(Vector3.Lerp(transform.position, pointToSeek.transform.position, 0.1f));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "CatStopZone")
        {
            inStopZone = true;
            if(moveDefault)
            {
                locationWhenInZone = rigidbody.transform.position;
                StartCoroutine(Hover());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "CatStopZone")
        {
            inStopZone = false;
            if(moveDefault)
            {
                StopCoroutine(Hover());
            }
        }
    }

    private IEnumerator Hover()
    {
        WaitForFixedUpdate wait = new WaitForFixedUpdate();
        float lerpDeltaTime = 0;
        Vector2 lerpStartPosition = rigidbody.transform.position;
        /*while(lerpDeltaTime < lerpTime)
        {
            rigidbody.MovePosition(Vector2.Lerp(lerpStartPosition, pointToSeek.transform.position, lerpDeltaTime / lerpTime));
            yield return wait;
            lerpDeltaTime += Time.deltaTime;
        }*/
        float hoverDeltaTime = 0;
        Vector2 hoverStartPosition = locationWhenInZone;
        Vector2 shakeVector = Vector2.right * (Random.Range(0, 2) == 1 ? 1 : -1);
        while(true)
        {
            rigidbody.MovePosition(hoverStartPosition + (Vector2.up * 0.15f * Mathf.Sin(hoverDeltaTime)) + (shakeVector * 0.1f * Mathf.Sin(hoverDeltaTime * 0.37f)));
            yield return wait;
            hoverDeltaTime += Time.deltaTime;
        }
    }
}
