using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSoundManager : MonoBehaviour
{
    public SoundController soundController;
    public BackgroundSoundArea[] soundAreas;

    private Rigidbody2D playerRigidbody;
    private GroundCheck groundChecker;

    private void Awake()
    {
        playerRigidbody = GetComponentInParent<Rigidbody2D>();
        groundChecker = GetComponentInParent<GroundCheck>();
    }

    private void Start()
    {
        checkAreas();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        checkAreas();
    }

    private void checkAreas()
    {
        foreach (BackgroundSoundArea area in soundAreas)
        {
            if (playerRigidbody.position.x > area.FromX && playerRigidbody.position.x < area.ToX)
            {
                soundController.FadeIn(area.SoundId, area.FadeInSeconds);
            }
            else
            {
                soundController.FadeOut(area.SoundId, area.FadeInSeconds);
            }
        }
    }
}
