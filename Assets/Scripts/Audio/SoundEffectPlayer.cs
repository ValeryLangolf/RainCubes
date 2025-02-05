using System.Collections.Generic;
using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;
    [SerializeField] private float _minPitch;
    [SerializeField] private float _maxPitch;
    [SerializeField] private int _maxAudioSources;

    private readonly List<AudioSource> _sources = new();

    public void Play()
    {
        AudioSource source = Get();

        if (source == null)
            return;

        source.pitch = Random.Range(_minPitch, _maxPitch);
        source.Play();
    }

    private AudioSource Get()
    {
        foreach (AudioSource audioSource in _sources)
            if (audioSource.isPlaying == false)
                return audioSource;

        if (_sources.Count >= _maxAudioSources)
            return null;

        AudioSource source = gameObject.AddComponent<AudioSource>();

        source.playOnAwake = false;
        source.clip = _clip;
        _sources.Add(source);
        return source;
    }
}