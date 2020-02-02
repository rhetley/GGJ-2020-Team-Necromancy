using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnCollisionStuff : MonoBehaviour
{
    [SerializeField]
    Hook refHook;
    public bool enabled;
    public FollowCrystal followCrystal;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (enabled)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            followCrystal.state = FollowCrystal.FollowCrystalState.following;
            //Debug.Log("YEEEEEEEEEEEEEEEEEEEEEEEEYEEEEEEEEEEEEEEEEEEEEEEEEYEEEEEEEEEEEEEEEEEEEEEEEEYEEEEEEEEEEEEEEEEEEEEEEEEYEEEEEEEEEEEEEEEEEEEEEEEEYEEEEEEEEEEEEEEEEEEEEEEEEYEEEEEEEEEEEEEEEEEEEEEEEEYEEEEEEEEEEEEEEEEEEEEEEEEYEEEEEEEEEEEEEEEEEEEEEEEEYEEEEEEEEEEEEEEEEEEEEEEEE");
            
            //refHook.Clear();
            //refHook.Teather(this.transform.position);
            
            enabled = false;
        }
    }
}
