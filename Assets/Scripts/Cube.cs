using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private CubeVisualiser _visualizer;
    [SerializeField] private bool _isCollision;
    [SerializeField] private float _minimumTimeLifeInSeconds;
    [SerializeField] private float _maximumTimeLifeInSeconds;

    private const string TagName = "Platform";

    public event Action<Cube> Deactivated;

    public float TimeLife => UnityEngine.Random.Range(_minimumTimeLifeInSeconds, _maximumTimeLifeInSeconds);

    private void OnCollisionEnter(Collision collision)
    {
        if (_isCollision)
            return;

        if(collision.gameObject.CompareTag(TagName))
        {
            _isCollision = true;
            _visualizer.Repaint();

            Invoke(nameof(Deactivate), TimeLife);
        }
    }

    public void Activate()
    {
        _isCollision = false;
        _visualizer.PaintDefault();
        gameObject.SetActive(true);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
        Deactivated?.Invoke(this);
    }
}