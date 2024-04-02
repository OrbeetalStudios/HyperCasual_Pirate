using MEC;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform target;
    public float distance;
    public float speed;
    [SerializeField]
    private Rigidbody rb;
    private Vector3 centerPositionMap;
    private float distanceTraveled;
    private float distanceThreshold = 300f;


    public void Start()
    {
        var targetObject = GameObject.FindWithTag("Player"); 
        if (targetObject == null)
        {
            Debug.LogError("the bullet script dosen't find the player target!");
            return;
        }

        target = targetObject.transform; 
        transform.position = targetObject.transform.position - targetObject.transform.forward * distance; // position of bullet forward player
        centerPositionMap = Vector3.zero;
        distanceTraveled = 0f;
        Vector3 perpendicularDirection = Quaternion.Euler(0, 180, 0) * target.forward;//Instantiate the bullet towards the sides of the map
        Timing.RunCoroutine(Movement(perpendicularDirection));
    }

    public void ResetPosition()
    {
        Start();
    }

    protected IEnumerator<float> Movement(Vector3 perpendicularDirection)
    {
        while (true)
        {
            if (this != null)
            {
                
                rb.velocity = perpendicularDirection * speed;
                //Direction outside map
                
                distanceTraveled = (transform.position - centerPositionMap).magnitude;
                if (distanceTraveled >= distanceThreshold)
                {
                    //DeactivateBulletAtdistanceThreshold
                    gameObject.SetActive(false);
                    break;
                }
                yield return Timing.DeltaTime;
            }
        }
        StopCoroutine("Movement");
    }


}
