using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCrystal : MonoBehaviour
{
    public enum FollowCrystalState
    {
        idle,
        saving,
        following
    }
    public GameObject crystalGO;
    public Queue<Vector2> crystalPositions;
    public FollowCrystalState state = FollowCrystalState.idle;
    public GameObject playerGO;
    public PlayerWalkJump playerWalkJump;
    public Vector2 prevPosition;
    public Vector2 nextPosition;
    public float percent = -1f;
    public Rigidbody2D rigidbody;

    public void Start()
    {
        crystalPositions = new Queue<Vector2>();
        rigidbody = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if(state == FollowCrystalState.saving)
        {
            EnqueuePosition(new Vector2(crystalGO.transform.position.x, crystalGO.transform.position.y));
        }
        else if(state == FollowCrystalState.following)
        {
            if (percent < 0)
            {
                percent += 1;
                playerWalkJump.enabled = false;
            
                prevPosition = transform.position;
                nextPosition = DequeuePosition();
                rigidbody.isKinematic = true;
            }

            Vector2 newPosition = Vector2.Lerp(prevPosition, nextPosition, percent);
            if(nextPosition == Vector2.zero)
            {
                playerWalkJump.enabled = true;
                crystalGO.SetActive(false);
                state = FollowCrystalState.idle;
                percent = -1f;
                rigidbody.isKinematic = false;
            }
            else
            {
                playerGO.transform.position = new Vector3(newPosition.x, newPosition.y, 0);
                percent += Time.deltaTime * 15f;
            }

            if(percent >= 1)
            {
                percent -= 2;
            }

        }
    }

    public void EnqueuePosition(Vector2 position)
    {
        crystalPositions.Enqueue(position);
    }

    public Vector2 DequeuePosition()
    {
        if(crystalPositions.Count > 0)
        {
            return crystalPositions.Dequeue();
        }
        return Vector2.zero;
    }
}
