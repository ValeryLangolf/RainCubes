using UnityEngine;
using UnityEngine.Pool;

public class Pool : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private int _maxSize;

    private ObjectPool<Cube> _cubes;

    private void Start()
    {
        _cubes = new(
            OnCreate,
            maxSize: _maxSize
        );
    }

    public Cube GetCube()
    {
        Cube cube = _cubes.Get();

        return cube;
    }

    public void ReleaseCube(Cube cube)
    {
        _cubes.Release(cube);
        cube.transform.parent = transform;
    }

    private Cube OnCreate()
    {
        Cube cube = Instantiate(_prefab, transform);
        cube.gameObject.SetActive(false);

        return cube;
    }
}