
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    [SerializeField, Range(0f, 151f)]//151 more rendering distance with this settings
    private float spawnRadius;
    public Vector3 resetPosition;
    private List<Vector3> spawnPoints = new List<Vector3>();
    [SerializeField]
    private GameController gc;
    private EnemyMovement movement;
    private bool collisionDetected;
    public void SetupEnemy(EnemyPrototype _defaultEnemy)
    {
        FindSpawnPoint();
        SetInitialPosition();
        FindAlternativePosition();
        //Damage
        //Speed
        //type

    }

    private void OnEnable()
    {
        GameObject gameController = GameObject.FindWithTag("GameController"); // FindTargetObjectwhenSpawn
        if (gameController == null)
        {
            Debug.LogError("GameCOntroller for enemy not found!");
        }
        else
        {
            gc = gameController.GetComponent<GameController>();
            Debug.Log("GameCOntrollerEnemy assigned");
        }
       
    }

    public void FindAlternativePosition()
    {  //Alternative position for respawn
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
        if(other.gameObject.name=="Player")
        {
            StartCoroutine(UpdatelifeAfterDelay());
           
        }
        else
        {
            if(other.gameObject.name != "Island")
            {
                Debug.Log("Ho colliso con " + other.gameObject.name);
                StartCoroutine(UpdateScoreAfterDelay());
                other.gameObject.SetActive(false);//Deactivate Bullet
            }
           
         
            
        }
                 
     }
    IEnumerator UpdateScoreAfterDelay()
    {
        // 1FrameLater
        yield return new WaitForSeconds(0.5f);
        gc.UpdateScore();
        gameObject.SetActive(false);
    }

    IEnumerator UpdatelifeAfterDelay()
    {
        // 1FrameLater
        yield return new WaitForSeconds(0.5f);
        gc.UpdateLife();
        gameObject.SetActive(false);
    }


}
