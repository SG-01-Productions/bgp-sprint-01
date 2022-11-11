using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    public GameObject ObjectToSpawn;

    [SerializeField] private int radius = 80;
    [SerializeField] private int asteroidSpeed = 20;
    [SerializeField] private int minforce = 1000;
    [SerializeField] private int maxforce = 20000;
    [SerializeField] private int numOfAsteroidsInitially = 5;

    [SerializeField] private float spawnInterval;
    [SerializeField] private float initialSpawnDelay;

    void Start()
    {
        InvokeRepeating("InvokeMyMethod", initialSpawnDelay, spawnInterval);
        for (int i = 0; i < numOfAsteroidsInitially; i++)
        {
            SpawnObject(ObjectToSpawn, Random.Range(0, 360));
        }
    }

    private void SpawnObject(GameObject ObjectToSpawn, int angle)
    {
        Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.right;
        Vector3 spawnPosition = transform.position + direction * radius;
        var _asteroid = Instantiate(ObjectToSpawn, spawnPosition, Quaternion.identity);

        var _asteroidRigidBody = _asteroid.GetComponent<Rigidbody>();
        Vector3 _asteroiddirection = Quaternion.Euler(0, 0, Random.Range(0, 360)) * Vector3.right;
        var _asteroidTransform = _asteroid.GetComponent<Transform>();
        Vector3 asteroidVelocity = _asteroidTransform.position + _asteroiddirection * Random.Range(minforce, maxforce);

        _asteroidRigidBody.AddForce(asteroidVelocity, ForceMode.Impulse);
    }

    private void InvokeMyMethod()
    {
        SpawnObject(ObjectToSpawn, Random.Range(0, 360));
    }
}
