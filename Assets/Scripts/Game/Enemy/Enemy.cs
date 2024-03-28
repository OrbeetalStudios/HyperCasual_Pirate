
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   
    public Vector3 resetPosition;
    [SerializeField]
    private GameController gc;
    [SerializeField]
    private EnemyMovement movement;
    [SerializeField]
    private EnemySpawnPoint logicSpawnPoint;
    
    public void SetupEnemy(EnemyPrototype _defaultEnemy)
    {
     
        SetInitialPosition();
        
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
        }
       
    }

    private void Awake()
    {
        GameObject pool = GameObject.FindWithTag("ObjectPool");
        logicSpawnPoint= pool.GetComponent<EnemySpawnPoint>();
    }

    //Crea una lista di 360 punti diversi
    //fai una ricerca binaria nella lista
    //scegli un punto e salvalo
    //cerca un altro punto abbastanza distante dal precedente
    //quando la lista finisce ripeti



    public void SetInitialPosition()
    {
        logicSpawnPoint.GetRandomSpawnPoint();
        transform.position= logicSpawnPoint.spawnPoint;
    }


  

    void OnTriggerEnter(Collider other)
    {

        switch (other.tag)
        {
            case "Player":
                
                StartCoroutine(UpdatelifeAfterDelay());
                break;
            case "Bullet":
                StartCoroutine(UpdateScoreAfterDelay());
                other.gameObject.SetActive(false);//Deactivate Bullet
                break;
            case "Island":
                movement.StartPlunder();
                break;

        }        
     }
     IEnumerator UpdateScoreAfterDelay()
    {
        // AttendForBugDubleAssign
        yield return new WaitForSeconds(0.01f);
        gc.UpdateScore();
        gameObject.SetActive(false);
    }

    IEnumerator UpdatelifeAfterDelay()
    {
        // AttendForBugDubleAssign
        yield return new WaitForSeconds(0.01f);
        gc.UpdateLife();
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        logicSpawnPoint.GetRandomSpawnPoint();
        transform.position = logicSpawnPoint.spawnPoint;
    }
}
