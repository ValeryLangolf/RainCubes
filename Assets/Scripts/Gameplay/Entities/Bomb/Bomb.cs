using UnityEngine;

public class Bomb : InteractiveObject<Bomb>
{
    [SerializeField] private BombVisuals _visuals;

    public override void Activate()
    {
        StartCoroutine(DeactivatingRutine());
        _visuals.StartFade(_timeLife);
    }

    public override void ResetState() =>
        _visuals.ResetState();
}