using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyPrototype.eEnemyID enemyType;
    [SerializeField, Range(0f, 151f)]//151 more rendering distance with this settings
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
        for (int i = 0; i < 360; i++)//casual point for 360 degrees Valutate if it's necessary push in list!
        {
            float angle = Random.Range(0f, Mathf.PI * 2f); // Generating casual angle

            float x = spawnRadius * Mathf.Cos(angle);
            float z = spawnRadius * Mathf.Sin(angle);

            Vector3 randomPoint = new Vector3(x, 0f, z);
            spawnPoints.Add(randomPoint);
        }

        //Spawn Object in random SpawnPoint
       // Vector3 randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        //Instantiate(gameObject, randomSpawnPoint, Quaternion.identity);
    }
}
