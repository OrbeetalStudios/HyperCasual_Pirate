using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    [SerializeField, Range(0f, 151f)]//151 more rendering distance with this settings
    private float spawnRadius;
    public Vector3 resetPosition;
    private List<Vector3> spawnPoints = new List<Vector3>();
    public void SetupEnemy(EnemyPrototype _defaultEnemy)
    {
        FindSpawnPoint();
        SetInitialPosition();   
        //Damage
        //Speed
        //type
    }

    public void SetInitialPosition()
    {
        Vector3 randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        Debug.Log("The point will start is " + randomSpawnPoint);
        transform.position = randomSpawnPoint;
        resetPosition= transform.position;
        //Debug.Log("Reset Position è " + resetPosition);
        
    }
    public void FindSpawnPoint()
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
    }

     void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "Player" && other.gameObject.name != "Island")
        {
           // Debug.Log("Trigger rilevato" + other.gameObject);
            other.gameObject.SetActive(false);//Deactivate Bullet
            gameObject.SetActive(false);//now the gameobject is deactivated in pool list
        }
          
       
    }


}
