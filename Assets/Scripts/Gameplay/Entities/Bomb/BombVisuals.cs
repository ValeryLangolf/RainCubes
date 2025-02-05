using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombVisuals : MonoBehaviour
{
    private const float StartAlphaColor = 1;
    private const float EndAlphaColor = 0;

    [SerializeField] private List<Renderer> _renderers;

    private readonly List<Material> _materials = new();
    private readonly List<Color> _initialColors = new();

    private void Awake()
    {
        foreach (var renderer in _renderers)
        {
            if (renderer == null)
                throw new ArgumentException(ExceptionData.Params.NullExceptionMessage);

            _materials.Add(renderer.material);
            _initialColors.Add(renderer.material.color);
        }
    }

    public void StartFade(float timeInSeconds) =>
        StartCoroutine(ChangeTransparencyOverTime(timeInSeconds));

    public void ResetState() =>
        SetAlpha(StartAlphaColor);

    private void SetAlpha(float alpha)
    {
        for (int i = 0; i < _materials.Count; i++)
            _materials[i].color = new Color(_initialColors[i].r, _initialColors[i].g, _initialColors[i].b, alpha);
    }    

    private IEnumerator ChangeTransparencyOverTime(float timeInSeconds)
    {
        float elapsedTime = 0f;

        while (elapsedTime < timeInSeconds)
        {
            yield return null;

            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / timeInSeconds;
            float currentAlpha = Mathf.Lerp(StartAlphaColor, EndAlphaColor, progress);

            SetAlpha(currentAlpha);
        }

        SetAlpha(EndAlphaColor);
    }
}