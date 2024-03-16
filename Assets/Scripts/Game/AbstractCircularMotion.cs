using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public abstract class AbstractCircularMotion : AbstractMotionToTarget
{
    [SerializeField]
    protected bool clockwiseMotion = false;

    protected float angle = 0.0f;

    protected void Start()
    {
        Timing.RunCoroutine(Move());
    }
}
