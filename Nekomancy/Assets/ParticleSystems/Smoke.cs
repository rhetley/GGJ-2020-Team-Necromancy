using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Smoke : MonoBehaviour
{
    [SerializeField]
    ParticleSystem smokeSystem;
    ParticleSystem.Particle[] smokeParticles;

    [SerializeField]
    Vector3 WindVelocity;


    private void Start()
    {
        
    }
    private void LateUpdate()
    {
        InitParticlesIfNeeded();

        int numParticles = smokeSystem.GetParticles(smokeParticles);

        for(int  i = 0; i < numParticles; i++)
        {
            smokeParticles[i].velocity = new Vector3(smokeParticles[i].remainingLifetime * WindVelocity.x * Mathf.Sin(Time.time + 5*this.transform.position.x), 0 , smokeParticles[i].remainingLifetime * WindVelocity.z);
        }

        smokeSystem.SetParticles(smokeParticles, numParticles);

    }
    private void InitParticlesIfNeeded()
    {
        if (smokeSystem == null)
            smokeSystem = GetComponent<ParticleSystem>();

        if (smokeParticles == null || smokeParticles.Length < smokeSystem.main.maxParticles)
            smokeParticles = new ParticleSystem.Particle[smokeSystem.main.maxParticles];
    }
}
