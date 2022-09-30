using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ParticleSystem))]
public class starfield : MonoBehaviour
{
    ParticleSystem.Particle[] particles;
    new ParticleSystem particleSystem;
    int numAlive;
    bool itRan;
    public float size = 500f;



    private void LateUpdate()
    {
        InitializeIfNeeded();
        particleSystem = GetComponent<ParticleSystem>();
        ParticleSystem.EmitParams emitOverride = new ParticleSystem.EmitParams();
        particleSystem.SetParticles(particles, numAlive);
        particleSystem.Emit(emitOverride, 5000);
        numAlive = particleSystem.GetParticles(particles);
        if (itRan==false)
        {
            CallOnce();
        }
    }
    private void CallOnce()
    {
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].position = new Vector3(Random.Range(-size, size), Random.Range(-size, size), Random.Range(-size, size));
            particles[i].velocity = new Vector3(0, 0, 0);
        }
        itRan = true;
    }
    void InitializeIfNeeded()
    {
        if (particleSystem == null)
            particleSystem = GetComponent<ParticleSystem>();
        if (particles == null || particles.Length < particleSystem.main.maxParticles)
            particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
    }

}
