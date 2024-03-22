using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewBulletCollection",menuName ="Create New Bullet Collection")]
public class BulletCollection : ScriptableObject
{
    public List<BulletPrototype> bullets = new List<BulletPrototype>(); 

    public Bullet TakeBullet(BulletPrototype.eBulletID type)
    {
        List<BulletPrototype> possibleBullet = new List<BulletPrototype>();
        for (int i = 0; i < bullets.Count; i++)
        {
            if (bullets[i].bulletID == type)
            {
                possibleBullet.Add(bullets[i]);
            }

        }
        if (possibleBullet.Count == 0)
        {
            Debug.LogError("Nothing bullets type: " + type);
        }

        BulletPrototype defaultBullet = possibleBullet[Random.Range(0, possibleBullet.Count)];
        GameObject newObjBullet = GameObject.Instantiate(defaultBullet.bulletPrefab);
        Bullet bullet = newObjBullet.GetComponent<Bullet>();
        //bullet.SetupBullet(defaultBullet);
        return bullet;
    }

   
}
