using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class SoundTest : MonoBehaviour
{

    public string SoundManagerName = "SoundManager";

    private SoundController soundController;

    private Dictionary<KeyCode, SoundId> soundIdsByKey;

    private void Awake()
    {
        soundIdsByKey = new Dictionary<KeyCode, SoundId>()
        {
            { KeyCode.L, SoundId.AdventureLoop},

            { KeyCode.C, SoundId.CatStrike},
            { KeyCode.X, SoundId.CatMusic},

            { KeyCode.F, SoundId.FishingStrike},
            { KeyCode.H, SoundId.HowlingWind},
            //{ KeyCode.W, SoundId.JumpUp },
            //{ KeyCode.S, SoundId.JumpDown },

            { KeyCode.M, SoundId.NekomancerBoth },
            { KeyCode.N, SoundId.NekomancerClarinet },
            { KeyCode.V, SoundId.NekomancerViolin },

            { KeyCode.B, SoundId.PickUpBone },

            { KeyCode.R, SoundId.RavenStrike },
            //{ KeyCode.T, SoundId.RavenMusic },

            { KeyCode.D, SoundId.WalkRight },
            { KeyCode.A, SoundId.WalkLeft },
        };

        GameObject SoundManager = GameObject.Find(SoundManagerName);
        soundController = SoundManager.GetComponent<SoundController>();

    }

    private void Start()
    {
        //GameObject canvas = GameObject.Find("Canvas");
        //Text text = canvas.AddComponent<Text>();

        //StringBuilder sb = new StringBuilder();
        //foreach (KeyCode keyCode in soundIdsByKey.Keys)
        //{
        //    sb.AppendLine($"{keyCode}: {soundIdsByKey[keyCode].ToString()}");
        //}
        //text.text = sb.ToString();

    }

    void Update()
    {
        foreach (KeyCode keyCode in soundIdsByKey.Keys)
        {
            if(Input.GetKeyDown(keyCode))
            {
                if(soundController.IsPlaying(soundIdsByKey[keyCode]))
                {
                    soundController.Stop(soundIdsByKey[keyCode]);
                }
                else
                {
                    soundController.Play(soundIdsByKey[keyCode]);
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            foreach(SoundId id in soundIdsByKey.Values)
            {
                soundController.Stop(id);
            }
        }
    }
}
