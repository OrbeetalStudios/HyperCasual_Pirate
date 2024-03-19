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

        Timing.RunCoroutine(Movement().CancelWith(gameObject));
    }

    protected IEnumerator<float> Movement()
    {
        while (true)
        {
            if (this != null)
            {
                Vector3 perpendicularDirection = Quaternion.Euler(0, 180, 0) * target.forward;//Instantiate the bullet towards the sides of the map
                rb.velocity = perpendicularDirection * speed;
                //Direction outside map
                yield return Timing.WaitForSeconds(10f);
                Destroy(gameObject);//to implement reuse
            }
        }
    }
}
