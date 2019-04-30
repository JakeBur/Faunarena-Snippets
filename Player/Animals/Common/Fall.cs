using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : Costate
{
    public float velocityGain; 

    public override void Initialize()
    {
        
    }

    public override void Run()
    {
        RaycastHit hit;
        if (!Physics.Raycast(transform.position, Vector3.down, out hit, .1f))
        {
            GetComponent<Rigidbody>().velocity -= Vector3.up * velocityGain * Time.deltaTime;
        }
    }

    public override bool TriggerConditionsMet()
    {
        return false;
    }
}
