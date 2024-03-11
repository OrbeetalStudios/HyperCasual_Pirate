using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectPool", menuName = "Custom/ObjectPool")]
public class ObjectPool : ScriptableObject
{
    public GameObject prefab;
    public int size;
    [HideInInspector] public List<GameObject> pool;
}
