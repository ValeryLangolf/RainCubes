using System.Collections.Generic;
using UnityEngine;

public class Detonator : MonoBehaviour
{
    public void Detonate(Vector3 position, float force, float radius)
    {
        foreach (Rigidbody rb in FindRigidbodiesInRadius(position, radius))
            rb.AddExplosionForce(force, position, radius);
    }

    private List<Rigidbody> FindRigidbodiesInRadius(Vector3 position, float radius)
    {
        Collider[] hits = Physics.OverlapSphere(position, radius);
        List<Rigidbody> rbList = new();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
                rbList.Add(hit.attachedRigidbody);
        }

        return rbList;
    }
}