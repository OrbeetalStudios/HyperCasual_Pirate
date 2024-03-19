using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class EnemyMovement : AbstractMotionToTarget
{
    
    protected override IEnumerator<float> Move()
    {
       
        GameObject targetObject = GameObject.FindWithTag("Island"); // FindTargetObjectwhenSpawn
        if (targetObject == null)
        {
            Debug.LogError("Island Target not found!");
            yield break; 
        }

        targetTransform = targetObject.transform; // Set Target



        while (true)
        {
            // relative vector from object to target
            Vector3 relativePos = targetTransform.position - transform.position;

            // Update position
            transform.position += relativePos.normalized * currentSpeed * Time.deltaTime;

            yield return Timing.WaitForOneFrame;
        }
    }
}
