using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    /*private PlayerWalkJump thePlayer;
    private ContactPoint2D[] contacts = new ContactPoint2D[10];*/

    public bool isGrounded;

    /*To update, always set distanceFromCenter equal to 1 pixel above halfway down the sprite.
    For instance, if a tile is 16 tall, set it to 9/16.
    Tile size is determined in Sprite Import Settings, which is found when selecting something in the Sprites folder.
    To make textures into sprites, click Sprite (2D and UI) in the Texture Type*/
    [SerializeField]
    private Transform thisObjectTransform;
    [SerializeField]
    private float distanceFromCenter;
    [SerializeField]
    private LayerMask layerCalledGround;

    // Start is called before the first frame update
    void Start()
    {
        //thePlayer = gameObject.GetComponent<PlayerWalkJump>();
        isGrounded = false;
    }

    public bool CheckIfGrounded()
    {
        RaycastHit2D[] hits;

        //We raycast down 1 pixel from this position to check for a collider
        hits = Physics2D.RaycastAll(new Vector2(thisObjectTransform.position.x, thisObjectTransform.position.y), new Vector2(0, -1), distanceFromCenter, layerCalledGround);
        //Debug.DrawLine(new Vector2(thisObjectTransform.position.x, thisObjectTransform.position.y), new Vector2(thisObjectTransform.position.x, thisObjectTransform.position.y) + new Vector2(0, -distanceFromCenter));

        //if a collider was hit, we are grounded
        if (hits.Length > 0)
        {
            isGrounded = true;
            //Debug.Log("Detected " + isGrounded);
        }
        else
        {
            isGrounded = false;
            //Debug.Log("Detected " + isGrounded);
        }

        return isGrounded;
    }

    // Update is called once per frame
    /*void Update()
    {
        

    }*/

    /*void OnCollisionEnter2D(Collision2D theCollision)
    {
        //ContactPoint2D contact = incidentCollision.GetContacts(ContactPoint2D[] contacts);

        *//*theCollision.GetContacts(contacts);
        foreach (ContactPoint2D item in contacts)
        {
            Debug.Log("Detected " + contacts[0].normal);

        }*/

    /*ContactFilter2D filter = new ContactFilter2D();
    filter.SetLayerMask(Ground);
    filter.SetNormalAngle(MinGroundAngle, MaxGroundAngle);*//*

    thePlayer.isGrounded = true;
}

void OnCollisionExit2D(Collision2D theCollision)
{
    thePlayer.isGrounded = false;
}*/
}
