using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandCollision : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
       other.gameObject.SetActive(false);//Deactivate Enemy
    }
}
