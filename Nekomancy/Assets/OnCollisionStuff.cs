using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnCollisionStuff : MonoBehaviour
{
    [SerializeField]
    Hook refHook;

    private void OnCollisionEnter2D(Collision collision)
    {
        Debug.Log("hi");
        refHook.Teather(this.transform.position);
    }
}
