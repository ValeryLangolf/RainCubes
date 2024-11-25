using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private TargetPosition _minimum;
    [SerializeField] private TargetPosition _maximum;
    [SerializeField] private int _maxSizePool;
    [SerializeField] private bool _isSpawn = true;
    [SerializeField, Min(0.01f)] private float _delayInSeconds;

    private ObjectPool<Cube> _pool;

    private Vector3 RandomPosition => new(
        Random.Range(_minimum.X, _maximum.Z),
        Random.Range(_minimum.Y, _maximum.Z),
        Random.Range(_minimum.Z, _maximum.Z));

    private void Awake()
    {
        _pool = new(
            OnCreate,
            OnGet,
            OnRelease,
            maxSize: _maxSizePool);
    }

    private void Start()
    {
        StartCoroutine(Spawning());
    }

    private Cube OnCreate()
    {
        Cube cube = Instantiate(_prefab, transform);
        cube.gameObject.SetActive(false);

        return cube;
    }

    private void OnGet(Cube cube)
    {
        cube.Deactivated += OnRelease;
        cube.Activate(RandomPosition);
    }

    private void OnRelease(Cube cube)
    {
        cube.Deactivated -= OnRelease;
    }

    private IEnumerator Spawning()
    {
        WaitForSeconds timeWait = new WaitForSeconds(_delayInSeconds);

        while (_isSpawn)
        {
            yield return timeWait;

            _pool.Get();
        }
    }
}