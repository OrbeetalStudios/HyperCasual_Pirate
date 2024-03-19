using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class LinearMotion : AbstractMotionToTarget
{
    protected override IEnumerator<float> Move()
    {
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
