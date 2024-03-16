using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMotionToTarget : MonoBehaviour
{
    [SerializeField]
    protected Transform targetTransform; // target point to reach
    [SerializeField, Range(0f, 50f)]
    protected float speed;

    protected abstract IEnumerator<float> Move();
}
