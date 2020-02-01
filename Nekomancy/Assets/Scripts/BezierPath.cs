using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BezierPath
{
    [SerializeField]
    Vector2[] path;
    public BezierPath(params Vector2[] path)
    {
        this.path = path;
        if (path.Length < 1)
        {
            throw new ArgumentException("Bezier Curve needs Points to function");
        }
    }
    public Vector2 GetPosition(float t)
    {
        if (path.Length == 1)
        {
            return path[0];
        }
        Vector2 BezierPosition = Vector2.zero;
        for (int i = 0; i < path.Length; i++)
        {
            BezierPosition += Combinations(path.Length - 1, i) * Mathf.Pow(1 - t, path.Length - 1 - i) * Mathf.Pow(t, i) * path[i];
        }
        return BezierPosition;
    }

    public int Combinations(int n, int k)
    {
        if (k == 0 || n == k)
        {
            return 0;
        }
        else
        {
            int topEnd = 1;
            for (int i = 0; i < k; i++)
            {
                topEnd *= (n - i);
            }
            int bottomEnd = 1;
            for (int j = 1; j <= k; j++)
            {
                bottomEnd *= j;
            }
            return topEnd / bottomEnd;
        }
    }
}
