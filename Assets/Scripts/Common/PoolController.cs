using MEC;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity.VisualScripting;
using UnityEngine;

public class PoolController : MonoBehaviour
{
    [SerializeField]
    private GameController gc;
    public List<GameObject> objectPool;
    public List<GameObject> bulletPool;
    public EnemyCollection enemyCollection;
    public BulletCollection bulletCollection;
    [SerializeField]
    private float spawnInterval=30f;
    public int bulletRecharge=0;
    //private bool foundInactiveBullet=false;
    private void Start()
    {
        objectPool = new List<GameObject>();
        InizializeBulletList();
        Timing.RunCoroutine(SpawnEnemy());
    }

    private  void InizializeBulletList()
    {
        bulletPool = new List<GameObject>();
        for (int i = 10; i>= bulletPool.Count; i--)
        {
            Bullet bulletInstance = bulletCollection.TakeBullet(default);
            GameObject bulletPrefab = bulletInstance.gameObject;
            bulletPrefab.SetActive(false);
            bulletPool.Add(bulletPrefab);
        }
    }

    public void SpawnBullet(int spawnCount)
    {
        foreach (GameObject obj in bulletPool)
        {
            if (!obj.activeSelf)
            {
                GameObject bulletPrefab = obj;
                var script = bulletPrefab.GetComponent<Bullet>();
                script.ResetPosition();
                bulletPrefab.SetActive(true);
                break;
            }
            else continue;
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
                        var scriptMov = enemyPrefab.GetComponent<EnemyMovement>();
                        script.transform.position=script.resetPosition;//Reset to new position
                        enemyPrefab.SetActive(true);
                        script.FindAlternativePosition();//prepareAlternativePositionNextRespawn(TOCONFIRM)
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
