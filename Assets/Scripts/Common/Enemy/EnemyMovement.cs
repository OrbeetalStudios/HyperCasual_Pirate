using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using TreeEditor;
using UnityEngine.UIElements;

public class EnemyMovement : Enemy
{
    [SerializeField] 
    private Transform targetPosition;
    [SerializeField]
    private Transform spawnPosition;
    LineRenderer lr;//LINE FOR PROTOTYPING
    public int Resolution;
    [SerializeField, Range (0f, 100000f)]
    public float Duration;
    float startTime;
    public Transform Marker;
    [SerializeField]
    private float speedRotationToTarget;

    
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
        lr = GetComponent<LineRenderer>();   
        lr.positionCount= Resolution;
        for(int i = 0; i < Resolution; i++)
        {
            // relative vector from object to target
            float t = (float)i/(float) Resolution;
            Vector3 pos = Vector3.Lerp(spawnPosition.position, targetPosition.position, t);
            lr.SetPosition(i, pos);
        }

        //Rotation to target
        Vector3 direction = targetPosition.position - transform.position;
        float targetAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, -targetAngle, 0));
        startTime = Time.time;
        transform.rotation = targetRotation;

        Timing.RunCoroutine(Move());
    }
    protected  IEnumerator<float> Move()
    {
        while (true)
        {
           
            float t = (Time.time - startTime) / Duration;
            // Update position
            Marker.position=Vector3.Lerp(spawnPosition.position, targetPosition.position, t);
            yield return Timing.WaitForOneFrame;

        }
    }


}
