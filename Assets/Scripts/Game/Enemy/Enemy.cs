
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    [SerializeField, Range(0f, 180f)]//180 more rendering distance with this settings
    private float spawnRadius;
    public Vector3 resetPosition;
    private List<Vector3> spawnPoints = new List<Vector3>();
    public GameController gc;
    [SerializeField]
    private EnemyMovement movement;

    protected void Awake()
    {
        SetupEnemy();
    }
    public void SetupEnemy()
    {
        FindSpawnPoint();
        SetInitialPosition();
        FindAlternativePosition();
        //Damage
        //Speed
        //type

    }

    private void Start()
    {
        GameObject gameController = GameObject.FindWithTag("GameController"); // FindGameControllerForUI
        gc = gameController.GetComponent<GameController>();
    }

    protected void OnEnable()
    {
        FindAlternativePosition();
    }

    public void FindAlternativePosition()
    { 
        //Alternative position for respawn
        FindSpawnPoint();
        Vector3 randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        resetPosition = randomSpawnPoint;
    }

    public void SetInitialPosition()
    {
        Vector3 randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        transform.position = randomSpawnPoint;
    }
    public void FindSpawnPoint()
    {
        //Create Random Spawn Point
        for (int i = 0; i < 360; i++)//casual point for 360 degrees push in list!
        {
            float angle = Random.Range(0f, Mathf.PI * 2f); // Generating casual angle

            float x = spawnRadius * Mathf.Cos(angle);
            float z = spawnRadius * Mathf.Sin(angle);

            Vector3 randomPoint = new Vector3(x, 0f, z);
            spawnPoints.Add(randomPoint);
        }
    }

     void OnTriggerEnter(Collider other)
    {
       

        switch (other.tag)
        {
            case "Player":
                gc.UpdateLife();
                gameObject.SetActive(false);
                break;
            case "Bullet":
                gc.UpdateScore();
                gameObject.SetActive(false);
                other.gameObject.SetActive(false);//Deactivate Bullet
                break;
            case "Island":
                movement.StartPlunder();
                break;

        }        
     }
}
