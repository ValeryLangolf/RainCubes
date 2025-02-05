using System;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> : MonoBehaviour where T : MonoBehaviour, IActivatable<T>
{
    [SerializeField] private T prefab;

    private readonly Stack<T> _availableObjects = new();

    public event Action OnObjectCreated;

    public T Get()
    {
        if (_availableObjects.Count > 0)
            return _availableObjects.Pop();
        else
            return Create();
    }

    public void Return(T element)
    {
        element.ResetState();
        element.gameObject.SetActive(false);
        element.transform.SetParent(transform);
        _availableObjects.Push(element);
    }

    private T Create()
    {
        T obj = Instantiate(prefab);
        obj.gameObject.SetActive(false);

        OnObjectCreated?.Invoke();

        return obj;
    }
}