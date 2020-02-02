using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class FoVByVel : MonoBehaviour
{
    private CinemachineVirtualCamera camera;
    public Rigidbody2D CharacterWithVelocity;
    private float percent;
    public float MinFOV;
    public float MaxFOX;
    
    
    // Start is called before the first frame update
    void Start()
    {
        camera = this.GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {

        percent += Mathf.Min(Time.deltaTime * (-1f + Mathf.Abs(CharacterWithVelocity.velocity.x)/2), 0.5f);
        percent = Mathf.Clamp(percent, 0, 1) * .9f;
        
        camera.m_Lens.FieldOfView = Mathf.Lerp(MinFOV, MaxFOX, percent);
    }
}
