using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    /*private PlayerWalkJump thePlayer;
    private ContactPoint2D[] contacts = new ContactPoint2D[10];*/

    [SerializeField]
    private LayerMask layerCalledGround;

    private Collider2D playerCollider2D;
    private ContactFilter2D groundContactFilter;
    private ContactFilter2D rightContactFilter;
    private ContactFilter2D leftContactFilter;
    private ContactPoint2D[] contactPoints = new ContactPoint2D[1];

    public bool isGrounded;
    public bool isAgainstRight;
    public bool isAgainstLeft;

    /*To use raycasting, always set distanceFromCenter equal to 1 pixel above halfway down the sprite.
    For instance, if a tile is 16 tall, set it to 9/16.
    Tile size is determined in Sprite Import Settings, which is found when selecting something in the Sprites folder.
    To make textures into sprites, click Sprite (2D and UI) in the Texture Type*/
    /*[SerializeField]
    private Transform thisObjectTransform;
    [SerializeField]
    private float distanceFromCenter;*/

    // Start is called before the first frame update
    void Start()
    {
        playerCollider2D = GetComponent<Collider2D>();
        //thePlayer = gameObject.GetComponent<PlayerWalkJump>();
        isGrounded = false;
        isAgainstRight = false;
        isAgainstLeft = false;

        //Ground contact filter
        groundContactFilter = new ContactFilter2D();
        groundContactFilter.layerMask = layerCalledGround;
        groundContactFilter.useLayerMask = true;
        groundContactFilter.SetNormalAngle(89f, 91f);
        groundContactFilter.useNormalAngle = true;
        //Right contact filter
        rightContactFilter = new ContactFilter2D();
        rightContactFilter.layerMask = layerCalledGround;
        rightContactFilter.useLayerMask = true;
        rightContactFilter.SetNormalAngle(179f, 181f);
        rightContactFilter.useNormalAngle = true;
        //Left contact filter
        leftContactFilter = new ContactFilter2D();
        leftContactFilter.layerMask = layerCalledGround;
        leftContactFilter.useLayerMask = true;
        leftContactFilter.SetNormalAngle(-1f, 1f);
        leftContactFilter.useNormalAngle = true;
    }

    private void FixedUpdate()
    {
        isGrounded = playerCollider2D.GetContacts(groundContactFilter, contactPoints) > 0;
        isAgainstRight = playerCollider2D.GetContacts(rightContactFilter, contactPoints) > 0;
        isAgainstLeft = playerCollider2D.GetContacts(leftContactFilter, contactPoints) > 0;
    }

    /*public bool CheckIfGrounded()
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
    }*/

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
