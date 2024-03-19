using MEC;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class PoolController : MonoSingleton<PoolController>
{
    public List<GameObject> objectPools;
    public List<GameObject> bullet;
    public EnemyCollection enemyCollection;
    public BulletCollection bulletCollection;
    [SerializeField]
    private float spawnInterval=30f;
    private void Start()
    {
        objectPools = new List<GameObject>();
        Timing.RunCoroutine(SpawnEnemy());
    }

    public void SpawnBullet()
    {
        Bullet bulletInstance = bulletCollection.TakeBullet(default);
        GameObject bulletPrefab = bulletInstance.gameObject;
        bullet.Add(bulletPrefab);
    }
    protected  IEnumerator<float> SpawnEnemy()
    {
        while (true)
        {
            GameObject enemyPrefab;
            enemyPrefab=InstantiateEnemyFromCollection(enemyCollection);

            if(enemyPrefab != null )
            {

                objectPools.Add(enemyPrefab);

            }

            yield return Timing.WaitForSeconds(spawnInterval);
           // enemyPrefab.SetActive(false);
           //Find solution to Reuse
        }
    }

  
    GameObject InstantiateEnemyFromCollection(EnemyCollection collection)
    {
        if (collection != null)
        {
            Enemy enemyInstance = collection.TakeEnemy(default);

            if (enemyInstance != null)
            {
                return enemyInstance.gameObject;
            }
        }
        return null;

    }

    //  To be implemented with multiple prefabs

    /* EnemyPrototype.eEnemyID GetEnemyRandom()
     {
         int randomIndex = Random.Range(0, enemyCollection.enemies.Count);
         return enemyCollection.enemies[randomIndex].enemyID;
     }
    */


}
