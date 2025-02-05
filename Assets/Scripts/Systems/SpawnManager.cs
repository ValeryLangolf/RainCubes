using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawners")]
    [SerializeField] private CubeSpawner _spawnerCubes;
    [SerializeField] private BombSpawner _spawnerBombs;
    [SerializeField] private ParticleSpawner _spawnerParticles;

    [Header("Spawn zone")]
    [SerializeField] private LimitPoint _minimum;
    [SerializeField] private LimitPoint _maximum;

    [Header("Spawn parameters")]
    [SerializeField] private bool _isSpawningEnabled = true;
    [SerializeField, Min(0.01f)] private float _delayInSeconds;

    [Header("Eplosion parameters")]
    [SerializeField] private Detonator _detonator;
    [SerializeField] private SoundEffectPlayer _soundEffectPlayer;
    [SerializeField] private float _forceExplosion;
    [SerializeField] private float _radiusExplosion;

    private void OnEnable()
    {
        _spawnerCubes.OnDeactivated += OnSpawnBomb;
        _spawnerBombs.OnDeactivated += OnTurnExplosion;
    }

    private void OnDisable()
    {
        _spawnerCubes.OnDeactivated -= OnSpawnBomb;
        _spawnerBombs.OnDeactivated -= OnTurnExplosion;
    }

    private void Start() =>
        StartCoroutine(SpawnOverTime());

    private Vector3 GetRandomPosition()
    {
        return new(
            Random.Range(_minimum.transform.position.x, _maximum.transform.position.x),
            Random.Range(_minimum.transform.position.y, _maximum.transform.position.y),
            Random.Range(_minimum.transform.position.z, _maximum.transform.position.z));
    }

    private IEnumerator SpawnOverTime()
    {
        WaitForSeconds timeWait = new(_delayInSeconds);

        while (_isSpawningEnabled)
        {
            yield return timeWait;

            _spawnerCubes.Spawn(GetRandomPosition());
        }
    }

    private void OnSpawnBomb(Vector3 position) =>
        _spawnerBombs.Spawn(position);

    private void OnTurnExplosion(Vector3 position)
    {
        _spawnerParticles.Spawn(position);
        _detonator.Detonate(position, _forceExplosion, _radiusExplosion);
        _soundEffectPlayer.Play();
    }
}