using System;
using System.Collections;
using UnityEngine;

public abstract class InteractiveObject<T> : MonoBehaviour, IActivatable<T> where T : MonoBehaviour
{
    [SerializeField] protected float _minimumTimeLifeInSeconds;
    [SerializeField] protected float _maximumTimeLifeInSeconds;

    protected float _timeLife;

    public event Action<T> OnDeactivated;

    private void Awake() =>
        _timeLife = UnityEngine.Random.Range(_minimumTimeLifeInSeconds, _maximumTimeLifeInSeconds);

    protected virtual IEnumerator DeactivatingRutine()
    {
        yield return new WaitForSeconds(_timeLife);

        NotifyCompletion();
    }

    protected virtual void NotifyCompletion() =>
        OnDeactivated?.Invoke(this as T);

    public abstract void Activate();

    public abstract void ResetState();
}