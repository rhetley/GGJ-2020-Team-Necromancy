using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundWhileInRegion : MonoBehaviour
{
    public SoundId SoundToPlay;
    public string SoundManagerName = "SoundManager";
    public GameObject GameObject;

    private SoundController soundController;


    private void Awake()
    {
        GameObject SoundManager = GameObject.Find(SoundManagerName);
        soundController = SoundManager.GetComponent<SoundController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject  == GameObject)
        {
            soundController.Play(SoundToPlay);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == GameObject)
        {
            soundController.Stop(SoundToPlay);
        }
    }
}
