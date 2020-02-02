using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaves : MonoBehaviour
{
    [SerializeField]
    ParticleSystem LeafSystem;
    ParticleSystem.Particle[] LeavesParticles;

    [SerializeField]
    bool xy = true;

    private void Update()
    {
        InitParticlesIfNeeded();

        int numParticles = LeafSystem.GetParticles(LeavesParticles);

        if (xy)
        {
            for (int i = 0; i < numParticles; i++)
            {
                LeavesParticles[i].velocity = new Vector3((LeafSystem.main.startLifetime.constantMax - LeavesParticles[i].remainingLifetime) * Mathf.Sin(2 * Time.time + 5 * LeavesParticles[i].remainingLifetime), -2 + Mathf.Sin(LeavesParticles[i].position.y), 0);
            }

            LeafSystem.SetParticles(LeavesParticles, numParticles);
        }
    }

    private void InitParticlesIfNeeded()
    {
        if (LeafSystem == null)
            LeafSystem = GetComponent<ParticleSystem>();

        if (LeavesParticles == null || LeavesParticles.Length < LeafSystem.main.maxParticles)
            LeavesParticles = new ParticleSystem.Particle[LeafSystem.main.maxParticles];
    }
}
