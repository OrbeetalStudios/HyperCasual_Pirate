using MEC;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class PoolController : MonoSingleton<PoolController>
{
    
    public List<GameObject> objectPool;
    public List<GameObject> bulletPool;
    public EnemyCollection enemyCollection;
    public BulletCollection bulletCollection;
    [SerializeField]
    private float spawnInterval=30f;
    private void Start()
    {
        objectPool = new List<GameObject>();
        Timing.RunCoroutine(SpawnEnemy());
    }

    public void SpawnBullet()
    {
     
            if (bulletPool.Count > 0)
            {
                foreach (GameObject obj in bulletPool)
                {
                    if (!obj.activeSelf)
                    {
                        GameObject bulletPrefab;
                        bulletPrefab = obj;
                        var script = bulletPrefab.GetComponent<Bullet>();
                        script.ResetPosition();//Reset to initial position of prefab
                        bulletPrefab.SetActive(true);
                        //Debug.Log("Sono un proiettile riciclato");
                      
                        break;
                    }
                    else
                    {
                        Bullet bulletInstance = bulletCollection.TakeBullet(default);
                        GameObject bulletPrefab = bulletInstance.gameObject;
                        bulletPool.Add(bulletPrefab);
                
                    }

                }
            }
            else
            {
                Bullet bulletInstance = bulletCollection.TakeBullet(default);
                GameObject bulletPrefab = bulletInstance.gameObject;
                bulletPool.Add(bulletPrefab);
            }

    }
    protected  IEnumerator<float> SpawnEnemy()
    {
        while (true)
        {
            GameObject enemyPrefab;
            bool CanRecycle = false;
            if (objectPool.Count > 0)//If have deactive enemy in list take it
            {
                foreach (GameObject obj in objectPool)
                {
                    if (!obj.activeSelf)
                    {
                        enemyPrefab = obj;
                        var script= enemyPrefab.GetComponent<Enemy>();
                        script.transform.position=script.resetPosition;//Reset to initial position of prefab
                        enemyPrefab.SetActive(true);
                        Debug.Log("Sono stato riciclato");
                        CanRecycle = true;
                        break;
                    }
                }
            }
            if (!CanRecycle && enemyCollection != null) // If dont'have deactive enemy in list take it from Collection
            {
                enemyPrefab = InstantiateEnemyFromCollection(enemyCollection);
                objectPool.Add(enemyPrefab);
            }
            yield return Timing.WaitForSeconds(spawnInterval);
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
