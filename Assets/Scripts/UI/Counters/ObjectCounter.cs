using UnityEngine;

public class ObjectCounter<T> : MonoBehaviour where T : MonoBehaviour, IActivatable<T>
{
    [SerializeField] private ObjectCounterUI _viewer;
    [SerializeField] private Spawner<T> _spawner;
    [SerializeField] private Pool<T> _pool;

    private int _countTotalSpawned;
    private int _countCreatedObjects;
    private int _countObjectsInScene;

    private void OnEnable()
    {
        _spawner.Spawned += IncreaseSpawnCount;
        _spawner.OnDeactivated += DecreaseObjectsInScene;
        _pool.OnObjectCreated += IncreaseCreatedObjects;
    }

    private void OnDisable()
    {
        _spawner.Spawned -= IncreaseSpawnCount;
        _spawner.OnDeactivated -= DecreaseObjectsInScene;
        _pool.OnObjectCreated -= IncreaseCreatedObjects;
    }

    private void IncreaseSpawnCount()
    {
        _viewer.UpdateTotal(++_countTotalSpawned);
        IncreaseObjectsInScene();
    }

    private void IncreaseObjectsInScene() =>
        _viewer.UpdateInSceneObjects(++_countObjectsInScene);

    private void DecreaseObjectsInScene(Vector3 _) =>
        _viewer.UpdateInSceneObjects(--_countObjectsInScene);

    private void IncreaseCreatedObjects() =>
        _viewer.UpdateCreatedObjects(++_countCreatedObjects);
}