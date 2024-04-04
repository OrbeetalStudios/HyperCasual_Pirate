using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : LinearMotionToTarget
{
    private void Awake()
    {
        targetTransform.position = Vector3.zero;

        GameObject island;
        if (island = GameObject.FindWithTag("Island"))
        {
            targetTransform.position = island.transform.position;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //manage power up

            this.gameObject.SetActive(false);
        }
        else if (other.tag == "Island")
        {
            this.gameObject.SetActive(false);
        }
    }
}
