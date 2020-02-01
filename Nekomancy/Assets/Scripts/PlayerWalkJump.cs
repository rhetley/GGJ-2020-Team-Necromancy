using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkJump : MonoBehaviour
{
    private Vector2 velocity;
    float moveMultiplier = 10.0f;
    float jumpHeight = 5.0f;
    Rigidbody2D playerRigidbody;
    private GroundCheck groundChecker;

    public CameraSwitcher cameraOverlord;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        groundChecker = GetComponent<GroundCheck>();

        Physics2D.gravity = new Vector2(0.0f, -73.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && groundChecker.isGrounded)
        {
            Jump();
        }
        else
        {
            velocity.x = Input.GetAxis("Horizontal");
            if (groundChecker.isAgainstRight && velocity.x > 0)
            {
                velocity.x = 0;
            }
            else if (groundChecker.isAgainstLeft && velocity.x < 0)
            {
                velocity.x = 0;
            }

            /*if (Mathf.Abs(velocity.x) > 0 && cameraOverlord.activeCamera == (int)CameraSwitcher.CameraState.Default)
            {
                cameraOverlord.SwitchToRunCamera();
            }
            else if (Mathf.Abs(velocity.x) == 0 && cameraOverlord.activeCamera == (int)CameraSwitcher.CameraState.Run)
            {
                cameraOverlord.SwitchToDefaultCamera();
            }*/

            /*if (Mathf.Abs(velocity.x) > .1 && cameraOverlord.activeCamera == (int)CameraSwitcher.CameraState.Default)
            {
                // This sets the activeCamera to "TransitionToRun," later to be simply the run camera
                cameraOverlord.SwitchToRunCamera();
            }
            else if (Mathf.Abs(velocity.x) > .9 && cameraOverlord.activeCamera == (int)CameraSwitcher.CameraState.TransitionToRun)
            {
                // Transition is over, so the camera can now change again if the player needs
                cameraOverlord.activeCamera = (int)CameraSwitcher.CameraState.Run;
            }
            else if (Mathf.Abs(velocity.x) < .9 && cameraOverlord.activeCamera == (int)CameraSwitcher.CameraState.Run)
            {
                // This sets the activeCamera to "TransitionToDefault," later to be simply the default camera
                cameraOverlord.SwitchToDefaultCamera();
            }
            else if (Mathf.Abs(velocity.x) < .1 && cameraOverlord.activeCamera == (int)CameraSwitcher.CameraState.TransitionToDefault)
            {
                // Transition is over, so the camera can now change again if the player needs
                cameraOverlord.activeCamera = (int)CameraSwitcher.CameraState.Default;
            }*/


        }
    }

    private void FixedUpdate()
    {
        playerRigidbody.velocity = new Vector2(velocity.x * moveMultiplier, GetComponent<Rigidbody2D>().velocity.y);
    }

    void Jump()
    {
        velocity.y = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y));
        playerRigidbody.velocity = new Vector2(velocity.x, velocity.y);
        velocity.y += Physics2D.gravity.y * Time.deltaTime;
        playerRigidbody.velocity = new Vector2(velocity.x, velocity.y);
    }
}

