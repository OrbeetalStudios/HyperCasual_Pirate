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
    private float timeBulletToSetInActive=10f;
    [SerializeField]
    private Rigidbody rb;
    public void SetupBullet(BulletPrototype _defaultBullet)
    {
        
    }

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
        Timing.RunCoroutine(Movement());
    }

    public void ResetPosition()
    {
        Start();

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
                float timeInAir = timeBulletToSetInActive;
                yield return Timing.WaitForSeconds(timeInAir);
                gameObject.SetActive(false);//to implement reuse in objectpool
                StopCoroutine(Movement());
            }
        }
    }
}
