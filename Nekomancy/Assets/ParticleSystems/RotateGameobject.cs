using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGameobject : MonoBehaviour
{
    [SerializeField]
    float radius;
    [SerializeField]
    float speed;
    void Update()
    {
        this.transform.position = new Vector3(radius * Mathf.Cos(speed*Time.time), radius * Mathf.Sin(speed*Time.time),0);
    }
}
