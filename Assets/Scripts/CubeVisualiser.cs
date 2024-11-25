using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class CubeVisualiser : MonoBehaviour
{
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void PaintDefault()
    {
        Paint(Color.white);
    }

    public void Repaint()
    {
        Color color = new(Random.value, Random.value, Random.value);
        Paint(color);
    }

    private void Paint(Color color)
    {
        _renderer.material.color = color;
    }
}