using System.Collections.Generic;
using UnityEngine;

public class Detonator : MonoBehaviour
{
    public void Detonate(Vector3 position, float force, float radius)
    {
        foreach (Rigidbody rigidbody in FindRigidbodiesInRadius(position, radius))
            rigidbody.AddExplosionForce(force, position, radius);
    }

    private List<Rigidbody> FindRigidbodiesInRadius(Vector3 position, float radius)
    {
        Collider[] hits = Physics.OverlapSphere(position, radius);
        List<Rigidbody> rigidbodies = new();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
                rigidbodies.Add(hit.attachedRigidbody);
        }

        return rigidbodies;
    }
}