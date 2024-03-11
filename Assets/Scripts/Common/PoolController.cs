using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolController : MonoSingleton<PoolController>
{
    public List<ObjectPool> objectPools;
    private void Start()
    {
        InitializePools();
    }
    private void InitializePools()
    {
        // Initialize and populate all pools referenced in inspector
        foreach (ObjectPool pool in objectPools)
        {
            pool.pool = new List<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                pool.pool.Add(obj);
            }
        }
    }
    public GameObject GetObject(string tag)
    {
        foreach (ObjectPool pool in objectPools)
        {
            if (pool.prefab.tag == tag)
            {
                foreach (GameObject obj in pool.pool)
                {
                    if (!obj.activeInHierarchy)
                    {
                        return obj;
                    }
                }

                // If all objects in the pool are in use, instantiate a new one
                GameObject newObj = Instantiate(pool.prefab);
                newObj.SetActive(false);
                pool.pool.Add(newObj);
                return newObj;
            }
        }

        // If the specified tag is not found, return null
        return null;
    }
}
