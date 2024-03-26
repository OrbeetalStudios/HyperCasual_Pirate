using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyCollection", menuName = "Create New Enemy Collection")]
public class EnemyCollection : ScriptableObject
{
    public int size;
    [HideInInspector] public List<GameObject> pool;
    public List<EnemyPrototype> enemies = new List<EnemyPrototype>();

    public Enemy TakeEnemy(EnemyPrototype.eEnemyID type)
    {
        List<EnemyPrototype> possibleEnemy = new List<EnemyPrototype>();
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].enemyID == type)
            {
                possibleEnemy.Add(enemies[i]);
            }

        }
        if (possibleEnemy.Count == 0)
        {
            Debug.LogError("Nothing enemies type: " + type);
        }

        EnemyPrototype defaultEnemy = possibleEnemy[Random.Range(0, possibleEnemy.Count)];
        GameObject newObjEnemy = GameObject.Instantiate(defaultEnemy.enemyPrefab);
        Enemy enemy = newObjEnemy.GetComponent<Enemy>();
        enemy.SetupEnemy(defaultEnemy);
        return enemy;
    }


}

