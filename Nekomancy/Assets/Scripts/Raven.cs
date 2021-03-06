﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raven : MonoBehaviour
{
    [SerializeField]
    Vector2[] path;

    BezierPath ravenPath;
    [SerializeField]
    [Range(0, 1)]
    float time;
    [SerializeField]
    float animLength;
    bool AnimStarting;
    private void Start()
    {
        ravenPath = new BezierPath(path);
        time = 0;
        AnimStarting = true;
        Debug.Log(BezierPath.Combinations(5, 2));
        Debug.Log(BezierPath.Combinations(7, 4));
    }

    private void Update()
    {
        if (AnimStarting)
        {
            Vector2 loc = ravenPath.GetPosition(time);
            this.transform.position = new Vector3(loc.x, loc.y, 0);
            //time += Time.deltaTime/animLength;
        }
    }
}
