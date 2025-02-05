using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class CubeVisuals : MonoBehaviour
{
    private Material _material;
    private Color _defaultColor;

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
        _defaultColor = _material.color;
    }

    public void UpdateColorToDefault() =>
        SetColor(_defaultColor);

    public void SetRandomColor() =>
        SetColor(new(Random.value, Random.value, Random.value));

    private void SetColor(Color color) =>
        _material.color = color;
}