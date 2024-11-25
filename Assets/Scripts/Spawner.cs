using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private TargetPosition _minimum;
    [SerializeField] private TargetPosition _maximum;
    [SerializeField, Min(0.01f)] private float _delayInSeconds;
    [SerializeField] private int _maxSizePool;
    [SerializeField] private bool _isSpawn = true;

    private ObjectPool<Cube> _pool;
    private List<Cube> _cubes = new List<Cube>();

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
        StartCoroutine(Spawn());
    }

    private void OnEnable()
    {
        foreach (Cube cube in _cubes)
            Subscribe(cube);
    }

    private void OnDisable()
    {
        foreach (Cube cube in _cubes)
            Unsubscribe(cube);
    }

    private void Subscribe(Cube cube)
    {
        cube.Deactivated += OnRelease;
    }

    private void Unsubscribe(Cube cube)
    {
        cube.Deactivated -= OnRelease;
    }

    private Cube OnCreate()
    {
        Cube cube = Instantiate(_prefab, transform);
        cube.gameObject.SetActive(false);

        return cube;
    }

    private void OnGet(Cube cube)
    {
        Subscribe(cube);
        _cubes.Add(cube);
        cube.transform.position = RandomPosition;
        cube.Activate(RandomPosition);
    }

    private void OnRelease(Cube cube)
    {
        Unsubscribe(cube);
        _cubes.Remove(cube);
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds timeWait = new WaitForSeconds(_delayInSeconds);

        while (_isSpawn)
        {
            yield return timeWait;

            _pool.Get();
        }
    }
}