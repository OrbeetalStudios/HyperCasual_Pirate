using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyPrototype.eEnemyID enemyType;
    [SerializeField, Range(0f, 30f)]
    private float spawnRadius;
    private List<Vector3> spawnPoints = new List<Vector3>();
    public void SetupEnemy(EnemyPrototype _defaultEnemy)
    {
        SpawnPosition();
        //Damage
        //Speed
        //type
    }
    public void Start()
    {
        Vector3 randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        transform.position = randomSpawnPoint;
    }
    public void SpawnPosition()
    {
        //Create Random Spawn Point
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = new Vector3(Random.Range(-spawnRadius, spawnRadius), 0f, Random.Range(-spawnRadius, spawnRadius));
            spawnPoints.Add(randomPoint);
        }

        //Spawn Object in random SpawnPoint
       // Vector3 randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        //Instantiate(gameObject, randomSpawnPoint, Quaternion.identity);
    }
}
