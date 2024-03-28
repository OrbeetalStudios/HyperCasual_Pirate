using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField, Range(0f, 180f)]//180 more rendering distance with this settings
    private float spawnRadius;
    private List<Vector3> possibleSpawnPoints = new List<Vector3>();
    public float minSpawnDistance;
    public Vector3 spawnPoint;

    private void Awake()
    {
        CreateList();
    }
    void CreateList()
    {

        //Create Random Spawn Point
        for (int i = 0; i < 360; i++)//casual point for 360 degrees push in list!
        {
            float angle = Random.Range(0f, Mathf.PI * 2f); // Generating casual angle

            float x = spawnRadius * Mathf.Cos(angle);
            float z = spawnRadius * Mathf.Sin(angle);

            Vector3 randomPoint = new Vector3(x, 0f, z);
            possibleSpawnPoints.Add(randomPoint);

        }
        GetRandomSpawnPoint();
    }

    public void GetRandomSpawnPoint()
    {
        if (possibleSpawnPoints.Count == 0)
        {
            CreateList();
        }
        if (spawnPoint == null)//first iteraction
        {
            int randomIndex = Random.Range(0, possibleSpawnPoints.Count);
            spawnPoint = possibleSpawnPoints[randomIndex];
           // possibleSpawnPoints.RemoveAt(randomIndex);
        }
        else
        {
            List<Vector3> filteredSpawnPoints = possibleSpawnPoints.FindAll(point => Vector3.Distance(point, spawnPoint) >= minSpawnDistance);//chosepointnotnear

            if (filteredSpawnPoints.Count > 0)
            {
                int randomIndex = Random.Range(0, filteredSpawnPoints.Count);
                spawnPoint = filteredSpawnPoints[randomIndex];
              //  possibleSpawnPoints.RemoveAt(randomIndex);
            }
            else
            {
                CreateList();
            }
        }


    }


}