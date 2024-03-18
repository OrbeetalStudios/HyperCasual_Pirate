using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public abstract class AbstractMotionToTarget : MonoBehaviour
{
    [SerializeField]
    protected Transform targetTransform; // target point to reach
    [SerializeField, Range(0f, 50f)]
    protected float speed;

    protected void Start()
    {
        Timing.RunCoroutine(Move());
    }

    protected abstract IEnumerator<float> Move();
}
