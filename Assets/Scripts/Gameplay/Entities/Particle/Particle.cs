using System.Collections;
using UnityEngine;

public class Particle : InteractiveObject<Particle>
{
    [SerializeField] private ParticleSystem _particle;

    protected override IEnumerator DeactivatingRutine()
    {
        while (_particle.isPlaying)
            yield return null;

        NotifyCompletion();
    }

    public override void Activate()
    {
        _particle.Play();
        StartCoroutine(DeactivatingRutine());
    }

    public override void ResetState() { }
}