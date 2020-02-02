using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    [SerializeField] private SoundController soundController;

    private Rigidbody2D playerRigidbody;
    private GroundCheck groundChecker;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        groundChecker = GetComponent<GroundCheck>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            soundController.Play(SoundId.FishingStrike);
        }
    }

    void LateUpdate()
    {
        if(groundChecker.isGrounded)
        {
            if(playerRigidbody.velocity.x > 0.1)
            {
                soundController.Play(SoundId.WalkRight);
            }
            else if (playerRigidbody.velocity.x < -0.1)
            {
                soundController.Play(SoundId.WalkLeft);
            }
            else
            {
                soundController.Stop(SoundId.WalkLeft);
                soundController.Stop(SoundId.WalkRight);
            }

            if (playerRigidbody.velocity.y > 0.1)
            {
                soundController.Play(SoundId.JumpUp);
            }
        }
        else
        {
            soundController.Stop(SoundId.WalkLeft);
            soundController.Stop(SoundId.WalkRight);
            //soundController.Stop(SoundId.JumpUp);
        }
    }
}
