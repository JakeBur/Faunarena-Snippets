using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// While in this state, the animal resists being moved by anything.
/// </summary>
public class Armor : State
{
    [Header("Time")]
    [Tooltip("Time to spend being Armored")]
    public float length;
    [Tooltip("Time before we can Armor again.")]
    public float cooldownLength;

    // Time
    private float cooldownReleaseTime;// The time when we can Armor again.
    private float endTime;// The time when we are done being Armored.

    // References
    private BearVisualManager visualManager;// The instance of BearVisualManager attached to the player character.

    new void Start()
    {
        base.Start();
        visualManager = GetComponent<BearVisualManager>();
    }

    public override void Deinitialize()
    {
        visualManager.DeactivateArmorVisuals();
    }

    public override void Initialize()
    {
        visualManager.ActivateArmorVisuals();
        cooldownReleaseTime = Time.time + cooldownLength;
        endTime = Time.time + length;
    }

    public override void Run()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        if (Time.time > endTime)
        {
            stateManager.TransitionToState(stateManager.defaultState);
        }
    }

    public override bool TransitionConditionsMet()
    {
        return Time.time > cooldownReleaseTime && inputManager.GetEvadeButtonDown();
    }
}
