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

    [SerializeField] private List<PoolObject> collections;

    [SerializeField]
    private float spawnInterval=30f;
    
    private void Start()
    {
        InitializeCollections();

        objectPool = new List<GameObject>();
        InizializeBulletList();
        Timing.RunCoroutine(SpawnEnemy().CancelWith(gameObject)); 
    }
    private void InitializeCollections()
    {
        foreach (PoolObject coll in collections)
        {
            coll.collection = new List<GameObject>();

            for (int i = 0; i < coll.numberOfObjects; i++)
            {
                GameObject obj = Instantiate(coll.prefab);
                obj.SetActive(false);
                coll.collection.Add(obj);
            }
        }
    }
    public GameObject GetObjectFromCollection(EPoolObjectType id)
    {
        foreach (PoolObject coll in collections)
        {
            if (coll.objID == id)
            {
                foreach (GameObject obj in coll.collection)
                {
                    if (!obj.activeSelf)
                    {
                        return obj;
                    }
                }

                // If all objects in the pool are in use, expand the pool
                GameObject newObj = Instantiate(coll.prefab);
                newObj.SetActive(false);
                coll.collection.Add(newObj);
                return newObj;
            }
        }

        // If the specified id is not found, return null
        return null;
    }
    private void InizializeBulletList()
    {
        bulletPool = new List<GameObject>();
        for (int i = 20; i>= bulletPool.Count; i--)
        {
            Bullet bulletInstance = bulletCollection.TakeBullet(default);
            GameObject bulletPrefab = bulletInstance.gameObject;
            bulletPrefab.SetActive(false);
            bulletPool.Add(bulletPrefab);
        }
    }

    public void SpawnBullet()
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
                        enemyPrefab.SetActive(true);
                        script.FindAlternativePosition();
                        CanRecycle = true;
                        break;
                    }
                }
            }
            if (!CanRecycle && enemyCollection != null) // If dont'have deactiveted enemy in list, take it from Collection
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
            Enemy enemyInstance = collection.TakeEnemy(default);//prende un proiettile con id default

            if (enemyInstance != null)
            {
                return enemyInstance.gameObject;
            }
        }
        return null;

    }



}
