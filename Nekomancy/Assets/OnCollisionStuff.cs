using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnCollisionStuff : MonoBehaviour
{
    [SerializeField]
    Hook refHook;
    public bool enabled;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (enabled)
        {
            refHook.Clear();
            refHook.Teather(this.transform.position);
            enabled = false;
        }
    }
}
