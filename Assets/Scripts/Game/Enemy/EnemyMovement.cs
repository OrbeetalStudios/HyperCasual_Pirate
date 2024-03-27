using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class EnemyMovement : Enemy
{
 
    
    [SerializeField]
    private Transform spawnPosition;
    [SerializeField]
    private float enemySpeed;
    [SerializeField]
    private GameObject model;
    private bool canMove = true;
    [SerializeField]
    private GameObject assaultArea;
    [SerializeField]    
    private int plunderTime;
    private int plunderDefault;
    private bool inPlunder = false;
    private float distanceTraveled;
    private float distanceThreshold = 150f;
    private float defaultSpeed;

    private void OnEnable()
    {
        canMove = true;
        Timing.RunCoroutine(Move());
    }

    private void Awake()
    {
        defaultSpeed=enemySpeed;
        plunderDefault = plunderTime;
        
    }

    public void StartPlunder()
    {
        canMove = false;
        StopCoroutine("Move");
        Vector3 relativePos = transform.position - Vector3.zero;
        Quaternion rotation = Quaternion.LookRotation(relativePos,Vector3.up);
        rotation *= Quaternion.Euler(0, 90, 0);
        transform.rotation = rotation;
        assaultArea.SetActive(true);
        inPlunder = true;   
        Timing.RunCoroutine(Plunder());


    }
    private  IEnumerator<float> Move()
    {
        while (canMove==true)
        {
            // relative vector from center to object
            Vector3 relativePos = transform.position - Vector3.zero;

            // Align rotation to radius direction vector, in order to always face the center object
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = rotation;

            // Update position
            transform.position -= relativePos.normalized * enemySpeed * Time.deltaTime;
            yield return Timing.WaitForOneFrame;
        }
    }

    protected IEnumerator<float> Plunder()
    {
        while (inPlunder==true)
        {
            if (plunderTime <= 0)
            {
                assaultArea.SetActive(false);
                Vector3 relativePos = transform.position - Vector3.zero;
                Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                rotation *= Quaternion.Euler(0, 180, 0);
                transform.rotation = rotation;
                inPlunder = false;
                Timing.RunCoroutine(ReturnOutsideMap(relativePos));
                StopCoroutine("Plunder");
            }
            plunderTime--;
            yield return Timing.WaitForSeconds(1f);
        }
    }


   protected IEnumerator<float> ReturnOutsideMap(Vector3 relativePos)
    {
        plunderTime = plunderDefault;
        while (this.isActiveAndEnabled)
        {
            transform.position += relativePos.normalized * enemySpeed * Time.deltaTime;
            distanceTraveled = (transform.position - Vector3.zero).magnitude;
            if (distanceTraveled >= distanceThreshold)
            {
                //DeactivateEnemyAtdistanceThreshold
                gameObject.SetActive(false);
                break;
            }
            yield return Timing.WaitForOneFrame;
        }
        StopCoroutine("ReturnOutsideMap");
      


    }

    private void OnDisable()
    {
        assaultArea.SetActive(false);
        canMove = false;
        StopAllCoroutines();
        plunderTime = plunderDefault;
        enemySpeed = defaultSpeed;
    }

}
