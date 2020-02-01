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
    public RopeSpawn ropeSpawn;

    [Header("Charge")]
    public bool chargeUp = true;
    public float chargeRate = .5f;
    public float chargeMax = 4f;
    [SerializeField]
    private float currentCharge;

    public float CurrentCharge
    {
        get
        {
            return currentCharge;
        }
        set
        {
            if(value < 0)
            {
                currentCharge = 0;
                chargeUp = true;
            }
            else if(value > chargeMax)
            {
                currentCharge = chargeMax;
                chargeUp = false;
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
        if(throwingState == ThrowingState.idle && Input.GetMouseButtonDown(1))
        {
            ropeSpawn.Reset();
        }
        else if (throwingState == ThrowingState.idle && Input.GetMouseButtonDown(0))
        {
            // Charging logic
            firingAngle = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - playerGO.transform.position);
            firingAngle.z = 0;
            firingAngle = firingAngle.normalized;

            throwingState = ThrowingState.charging;
        }
        else if (throwingState == ThrowingState.charging)
        {
            if(chargeUp)
            {
                CurrentCharge += chargeRate * Time.deltaTime;
            }
            else
            {
                CurrentCharge -= chargeRate * Time.deltaTime;
            }

            if (Input.GetMouseButtonUp(0))
            {
                Fire();
            }
        }
    }

    public void Fire()
    {
        Debug.Log("FIRE         Charge:" + currentCharge);

        crystalGO.GetComponent<Rigidbody2D>().AddForce(new Vector2(firingAngle.x * firingPower, firingAngle.y * firingPower) * CurrentCharge);

        CurrentCharge = -1f;
        throwingState = ThrowingState.idle;


    }
}
