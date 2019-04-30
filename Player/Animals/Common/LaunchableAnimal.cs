using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchableAnimal : Launchable
{
    public Vector3 StoredForce
    {
        get
        {
            return storedForce;
        }
        set
        {
            storedForce = value;
        }
    }


    protected Vector3 storedForce;

    private StunManager stunManager;

    void Update()
    {
        if(!HitStop.HitStopActive && storedForce != Vector3.zero)
        {
            GetComponent<Rigidbody>().AddForce(storedForce, ForceMode.Impulse);
            storedForce = Vector3.zero;
        }
    }

    public override void Launch(float force, Vector3 direction)
    {
        if(!HitStop.HitStopActive) HitStop.Instance.ToggleHitStop();
        storedForce = direction.normalized * force;
        CameraShake.Instance.ShakeScreen(50, .2f);
    }
}
