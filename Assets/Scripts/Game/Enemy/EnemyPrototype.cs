using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Prototype", menuName = "Create Enemy Prototype")]
public class EnemyPrototype : ScriptableObject
{
    public enum eEnemyID
    {
        Default,
        
    }

    public eEnemyID enemyID;
    public GameObject enemyPrefab;
}

