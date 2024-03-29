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
        // Cerca direttamente il nemico con l'ID specificato
        foreach (var enemy in enemies)
        {
            if (enemy.enemyID == type)
            {
                // Se trova il nemico, lo istanzia e lo restituisce
                GameObject newObjEnemy = GameObject.Instantiate(enemy.enemyPrefab);
                Enemy enemyComponent = newObjEnemy.GetComponent<Enemy>();
                enemyComponent.SetupEnemy(enemy);
                return enemyComponent;
            }
        }

        // Se non viene trovato un nemico con l'ID specificato, genera un errore
        Debug.LogError("No enemy found with type: " + type);
        return null;
    }
}