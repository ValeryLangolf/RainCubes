using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Pool _pool;
    [SerializeField] private TargetPosition _minimum;
    [SerializeField] private TargetPosition _maximum;
    [SerializeField, Min(0.01f)] private float _delayInSeconds;
    [SerializeField] private bool _isSpawn = true;

    private WaitForSeconds _timeWait;
    private List<Cube> _cubes = new List<Cube>();

    private Vector3 RandomPosition => new(
        Random.Range(_minimum.X, _maximum.Z),
        Random.Range(_minimum.Y, _maximum.Z),
        Random.Range(_minimum.Z, _maximum.Z));

    private void OnValidate()
    {
        _timeWait = new WaitForSeconds(_delayInSeconds);
    }

    private void Start()
    {
        StartCoroutine(Coroutine());
    }

    private void OnEnable()
    {
        foreach (Cube cube in _cubes)
        {
            Subscribe(cube);
        }
    }

    private void OnDisable()
    {
        foreach (Cube cube in _cubes)
        {
            Unsubscribe(cube);
        }
    }

    private IEnumerator Coroutine()
    {
        while (_isSpawn)
        {
            yield return _timeWait;

            Add();
        }
    }

    private void Subscribe(Cube cube)
    {
        cube.Deactivated += Remove;
    }

    private void Unsubscribe(Cube cube)
    {
        cube.Deactivated -= Remove;
    }

    private void Add()
    {
        Cube cube = _pool.GetCube();

        if (cube == null)
            return;

        Subscribe(cube);
        _cubes.Add(cube);

        cube.transform.parent = transform;
        cube.transform.position = RandomPosition;
        cube.Activate();
    }

    private void Remove(Cube cube)
    {
        if (cube == null)
            return;

        Unsubscribe(cube);
        _cubes.Remove(cube);
        _pool.ReleaseCube(cube);
    }
}