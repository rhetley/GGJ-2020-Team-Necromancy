using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwing : MonoBehaviour
{
    public enum ThrowingState
    {
        idle,
        charging
    }
    public GameObject playerGO;
    public GameObject crystalGO;
    //public RopeSpawn ropeSpawn;

    [Header("Charge")]
    public float chargeRate = .3f;
    public float chargeMin = .5f;
    public float chargeMax = 1f;
    [SerializeField]
    private float currentCharge;
    [SerializeField]
    private OnCollisionStuff crystalCollision;

    [Header("mouse")]
    public Vector2 intialMousePosition;
    public FollowCrystal followCrystal;

    public float CurrentCharge
    {
        get
        {
            return currentCharge;
        }
        set
        {
            if(value < chargeMin)
            {
                currentCharge = chargeMin;
            }
            else if(value > chargeMax)
            {
                currentCharge = chargeMax;
            }
            else
            {
                currentCharge = value;
            }
        }
    }

    [Header("Firing")]
    public Vector3 firingAngle;
    public float firingPower;

    public ThrowingState throwingState;
    // Start is called before the first frame update
    void Start()
    {
        throwingState = ThrowingState.idle;
    }

    // Update is called once per frame
    void Update()
    {
        if (throwingState == ThrowingState.idle && Input.GetMouseButtonDown(1))
        {
            firingAngle = Vector3.zero;

            crystalGO.SetActive(false);
            //ropeSpawn.Reset();
        }
        else if (throwingState == ThrowingState.idle && Input.GetMouseButtonDown(0))
        {
            intialMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            /*
            // Charging logic
            var stwp = (Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane)));
            firingAngle = (stwp - playerGO.transform.position);
            firingAngle.z = 0;
            firingAngle = firingAngle.normalized;
            firingAngle.x = firingAngle.x * 7;
            */

            throwingState = ThrowingState.charging;
        }
        else if (throwingState == ThrowingState.charging)
        {
            //CurrentCharge += chargeRate * Time.deltaTime;

            if (Input.GetMouseButtonUp(0))
            {
                Fire();
            }
        }
    }

    public void Fire()
    {
        Debug.Log("FIRE         Charge:" + currentCharge);

        Vector2 finalMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 difference = finalMousePosition - intialMousePosition;

        Vector2 differenceNormalized = difference.normalized;
        CurrentCharge = difference.magnitude / 750f;// * 100000000000f;
        crystalGO.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        crystalGO.transform.position = playerGO.transform.position;
        crystalGO.SetActive(true);
        crystalGO.GetComponent<Rigidbody2D>().AddForce(new Vector2(differenceNormalized.x * firingPower, differenceNormalized.y * firingPower) * CurrentCharge);

        followCrystal.state = FollowCrystal.FollowCrystalState.saving;

        //CurrentCharge = -1f;
        throwingState = ThrowingState.idle;
        crystalCollision.enabled = true;


    }
}
