using MEC;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private BulletPrototype.eBulletID bulletType;
    public Transform target;
    public float distance;
    public float speed;
    [SerializeField]
    private Rigidbody rb;
    public void SetupBullet(BulletPrototype _defaultBullet)
    {
        
    }

    private void Start()
    {
        var targetObject = GameObject.FindWithTag("Player"); 
        if (targetObject == null)
        {
            Debug.LogError("the bullet script dosen't find the player target!");
            return;
        }

        target = targetObject.transform; 
        transform.position = targetObject.transform.position - targetObject.transform.forward * distance; // position of bullet forward player

        Timing.RunCoroutine(Movement());
    }

    protected IEnumerator<float> Movement()
    {
        while (true)
        {
            rb.velocity = -transform.forward * speed;//Direction outside map
            yield return Timing.WaitForSeconds(10f);
            Destroy(gameObject);//to implement reuse


        }
    }
}
