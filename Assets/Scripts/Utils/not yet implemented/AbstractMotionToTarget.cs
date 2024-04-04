using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public abstract class AbstractMotionToTarget : MonoBehaviour
{
    protected Transform targetTransform; // target point to reach
    [SerializeField, Range(0f, 20f)]
    private float speed;

    protected float currentSpeed;

    protected void Start()
    {
        currentSpeed = speed;
        Timing.RunCoroutine(Move());
    }

    protected abstract IEnumerator<float> Move();
    public void Stop() => currentSpeed = currentSpeed != 0f ? 0f : speed;
}
