using System;
using System.Collections;
using UnityEngine;

public abstract class InteractiveObject<T> : MonoBehaviour, IActivatable<T> where T : MonoBehaviour
{
    [SerializeField] protected float _minimumTimeLifeInSeconds;
    [SerializeField] protected float _maximumTimeLifeInSeconds;

    protected float TimeLife;

    private WaitForSeconds _delay;

    public event Action<T> OnDeactivated;

    private void Awake()
    {
        TimeLife = UnityEngine.Random.Range(_minimumTimeLifeInSeconds, _maximumTimeLifeInSeconds);
        _delay = new WaitForSeconds(TimeLife);
    }

    protected virtual IEnumerator DeactivatingRutine()
    {
        yield return _delay;

        NotifyCompletion();
    }

    protected virtual void NotifyCompletion() =>
        OnDeactivated?.Invoke(this as T);

    public abstract void Activate();

    public abstract void ResetState();
}