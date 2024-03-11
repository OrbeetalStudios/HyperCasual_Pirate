using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class CircularMotion_XZPlane : MonoBehaviour
{
    [SerializeField]
    private Transform centerPoint; // Center point around which the object rotates
    [SerializeField, Range(0f, 30f)]
    private float rotationRadius = 2f;
    [SerializeField, Range(0f, 300f)]
    private float angularSpeed = 2f;
    [SerializeField]
    private bool clockwiseMotion = false;

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
            Vector3 newPosition = centerPoint.position + transform.forward * rotationRadius;
            transform.position = newPosition;
            transform.RotateAround(centerPoint.position, Vector3.up, (clockwiseMotion ? -1 : 1) * angularSpeed * Time.deltaTime);

            yield return Timing.WaitForOneFrame;
        }
    }
}
