using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet Prototype", menuName = "Create Bullet Prototype")]
public class BulletPrototype : ScriptableObject
{
    public enum eBulletID
    {
        Default,
        Fiery
    }

    public eBulletID bulletID;
    public GameObject bulletPrefab;
}
