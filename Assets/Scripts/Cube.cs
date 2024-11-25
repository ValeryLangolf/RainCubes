using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private CubeVisualiser _visualizer;
    [SerializeField] private bool _collided;
    [SerializeField] private float _minimumTimeLifeInSeconds;
    [SerializeField] private float _maximumTimeLifeInSeconds;

    public event Action<Cube> Deactivated;

    public float TimeLife => UnityEngine.Random.Range(_minimumTimeLifeInSeconds, _maximumTimeLifeInSeconds);

    private void OnCollisionEnter(Collision collision)
    {
        if (_collided)
            return;

        if(collision.gameObject.TryGetComponent<Platform>(out _))
        {
            _collided = true;
            _visualizer.Repaint();

            Invoke(nameof(Deactivate), TimeLife);
        }
    }

    public void Activate(Vector3 position)
    {
        _collided = false;
        _visualizer.PaintDefault();
        transform.position = position;
        gameObject.SetActive(true);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
        Deactivated?.Invoke(this);
    }
}