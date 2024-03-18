using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using UnityEngine.InputSystem;
public class EllipticalMotion_XZPlane : AbstractCircularMotion
{
    [SerializeField, Range(0f, 30f)]
    private float semiaxis_A, semiaxis_B = 2f;

    protected override IEnumerator<float> Move()
    {
        while (true)
        {
            // relative vector from center to object
            Vector3 relativePos = transform.position - targetTransform.position;

            // Align rotation to radius direction vector, in order to always face the center object
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = rotation;

            // compute new position
            Vector3 newPosition = transform.position;
            newPosition.x = semiaxis_A * Mathf.Cos(angle);
            newPosition.z = semiaxis_B * Mathf.Sin(angle);
            transform.position = newPosition;

            angle += (clockwiseMotion ? (-1) : 1) * currentSpeed * Time.deltaTime;

            yield return Timing.WaitForOneFrame;
        }
    }
}
