using System;
using UnityEngine;

public interface IActivatable<T> where T : MonoBehaviour
{
    public void Activate();

    public void ResetState();

    public event Action<T> OnDeactivated;
}