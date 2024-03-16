using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class EllipticalMotion_XZPlane : MonoBehaviour
{
    [SerializeField]
    private Transform centerPoint; // Center point around which the object rotates
    [SerializeField, Range(0f, 30f)]
    private float semiaxis_A, semiaxis_B = 2f;
    [SerializeField, Range(0f, 50f)]
    private float angularSpeed = 2f;
    [SerializeField]
    private bool clockwiseMotion = false;

    private float angle = 0.0f;

    private void Start()
    {
        Timing.RunCoroutine(_Move());
    }
    private IEnumerator<float> _Move()
    {
        while (true)
        {
            // relative vector from center to object
            Vector3 relativePos = transform.position - centerPoint.position;

            // Align rotation to radius direction vector, in order to always face the center object
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = rotation;

            // compute new position
            Vector3 newPosition = transform.position;
            newPosition.x = semiaxis_A * Mathf.Cos(angle);
            newPosition.z = semiaxis_B * Mathf.Sin(angle);
            transform.position = newPosition;

            angle += (clockwiseMotion ? (-1) : 1) * angularSpeed * Time.deltaTime;

            yield return Timing.WaitForOneFrame;
        }
    }
}
