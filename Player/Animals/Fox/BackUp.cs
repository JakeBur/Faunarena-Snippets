using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackUp : State
{
    public float distance;
    public float length;
    private Vector3 endPos;
    private Vector3 startPos;

    private float startTime;
    private float endTime;

    public override void Deinitialize()
    {

    }
    public override void Initialize()
    {
        endPos = transform.position + (transform.forward * -distance);
        startPos = transform.position;
        startTime = Time.time;
        endTime = Time.time + length;
    }

    public override void Run()
    {
        transform.position = Vector3.Lerp(startPos, endPos, (Time.time - startTime) / length);
        //transform.position = endPos;
        if (Time.time > endTime)
        {
            stateManager.TransitionToState(stateManager.defaultState);
        }
    }
}
