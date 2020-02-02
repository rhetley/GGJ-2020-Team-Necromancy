using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatedCatController : MonoBehaviour
{
    [SerializeField]
    private GameObject locationToSeek;
    [SerializeField]
    private int stopRadius;
    [SerializeField]
    private SoundController theAwesomeSound;

    private Vector2 distanceVector;
    private float percentToRadius;
    private Vector2 locationOnRadius;
    private bool inStopZone;
    private Rigidbody2D catRigidbody;
    private SpriteRenderer catSprite;
    private int currentSoundState;
    private enum soundState {  };

    private bool facingRight;

    // Start is called before the first frame update
    void Start()
    {
        catRigidbody = GetComponent<Rigidbody2D>();
        catSprite = GetComponent<SpriteRenderer>();
        inStopZone = false;
        facingRight = true;
        currentSoundState = 0;
    }

    void FixedUpdate()
    {
        if (!inStopZone)
        {
            distanceVector = locationToSeek.transform.position - transform.position;
            if (distanceVector.magnitude <= stopRadius * 1.001)
            {
                inStopZone = true;
                if (currentSoundState == 0)
                {
                    currentSoundState = 1;
                }
                StartCoroutine(Hover());
            }
            else
            {
                percentToRadius = distanceVector.magnitude - stopRadius;
                locationOnRadius = Vector2.Lerp(transform.position, locationToSeek.transform.position, percentToRadius);
                catRigidbody.MovePosition(Vector2.Lerp(transform.position, locationOnRadius, 0.025f));
                //Debug.Log("Distance vector " + distanceVector + " Distance " + distanceVector.magnitude + " radius " + stopRadius + " percent " + percentToRadius);

                if (distanceVector[0] > 0)
                {
                    catSprite.flipX = false;
                }
                else if (distanceVector[0] < 0)
                {
                    catSprite.flipX = true;
                }
            }
        }
        else
        {
            distanceVector = locationToSeek.transform.position - transform.position;
            if (distanceVector.magnitude > stopRadius * 1.5)
            {
                inStopZone = false;
                StopCoroutine(Hover());
                currentSoundState = 0;
            }
        }
    }

    private IEnumerator Hover()
    {
        WaitForFixedUpdate wait = new WaitForFixedUpdate();
        float hoverDeltaTime = 0;
        Vector2 shakeVector = Vector2.right * (Random.Range(0, 2) == 1 ? 1 : -1);

        if (currentSoundState == 1)
        {
            currentSoundState = 2;
            theAwesomeSound.Play(SoundId.CatStrike);
            //Debug.Log("Played it!");
        }

        while (true)
        {
            if (distanceVector[0] > 0)
            {
                catSprite.flipX = false;
            }
            else if (distanceVector[0] < 0)
            {
                catSprite.flipX = true;
            }
            catRigidbody.MovePosition(locationOnRadius + (Vector2.up * 0.15f * Mathf.Sin(10*hoverDeltaTime)) + (shakeVector * 0.1f * Mathf.Sin(hoverDeltaTime * 0.37f)));
            yield return wait;
            hoverDeltaTime += Time.deltaTime;
            //Debug.Log("In coroutine");
        }
    }
}
