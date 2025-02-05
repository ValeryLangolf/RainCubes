using UnityEngine;

public class Cube : InteractiveObject<Cube>
{
    [SerializeField] private CubeVisuals _visuals;
    [SerializeField] private bool _hasCollided;

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasCollided)
            return;

        if (collision.gameObject.TryGetComponent<GroundMarker>(out _))
        {
            _hasCollided = true;
            _visuals.SetRandomColor();

            StartCoroutine(DeactivatingRutine());
        }
    }

    public override void Activate() =>      
        _hasCollided = false;

    public override void ResetState() =>
        _visuals.UpdateColorToDefault();
}