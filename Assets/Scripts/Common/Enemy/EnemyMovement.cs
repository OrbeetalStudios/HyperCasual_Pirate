using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class EnemyMovement : Enemy
{
    [SerializeField]
    private Transform targetPosition;
    [SerializeField]
    private Transform spawnPosition;
    [SerializeField]
    private float enemySpeed;
    
    private void Start()
    {
        spawnPosition = transform;
        GameObject targetObject = GameObject.FindWithTag("Island"); // FindTargetObjectwhenSpawn
        if (targetObject == null)
        {
            Debug.LogError("Island Target not found!");
        }
        else
        {
            targetPosition = targetObject.transform; // Set Target
        }
        

        Timing.RunCoroutine(Move());
    }
    protected  IEnumerator<float> Move()
    {
        while (true)
        {
            // relative vector from center to object
            Vector3 relativePos = transform.position - targetPosition.position;

            // Align rotation to radius direction vector, in order to always face the center object
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = rotation;

            // Update position
            transform.position -= relativePos.normalized * enemySpeed * Time.deltaTime;
            yield return Timing.WaitForOneFrame;
        }
    }


}
