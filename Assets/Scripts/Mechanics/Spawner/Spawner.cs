using System;
using UnityEngine;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour, IActivatable<T>
{
    [SerializeField] private Pool<T> _pool;

    public event Action Spawned;
    public event Action<Vector3> OnDeactivated;

    public void Spawn(Vector3 position)
    {
        T element = _pool.Get();

        element.OnDeactivated += Deactivate;

        element.transform.SetParent(transform, false);
        element.transform.position = position;
        element.gameObject.SetActive(true);
        element.Activate();

        Spawned?.Invoke();
    }

    private void Deactivate(T element)
    {
        Vector3 position = element.transform.position;

        _pool.Return(element);
        element.OnDeactivated -= Deactivate;

        OnDeactivated?.Invoke(position);
    }
}