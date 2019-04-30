using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A slow slide backwards that chains into the Charge State.
/// Is interruptible.
/// </summary>
public class Windup : State
{
    [Header("Behavior")]
    [Tooltip("The speed at which the animal moves backwards.")]
    public float speed;

    [Header("Time")]
    [Tooltip("The time to spend in the state before transitioning to Charge.")]
    public float length;
    [Tooltip("The time to wait before beginning to back up.")]
    public float startingPause;
    [Tooltip("Time to wait before we can use the Windup again.")]
    public float cooldownLength;

    // Time
    private float startingPauseReleaseTime;// The time when we should begin to back up.
    private float cooldownReleaseTime;// The time when we can begin the Windup again.
    private float endTime;// The time when we should transition to the Charge State.

    private RamVisualManager visualManager;

    new void Start()
    {
        base.Start();
        visualManager = GetComponent<RamVisualManager>();
    }

    public override void Deinitialize()
    {
        GetComponent<Rigidbody>().isKinematic = false;
    }

    public override void Initialize()
    {
        startingPauseReleaseTime = Time.time + startingPause;
        cooldownReleaseTime = Time.time + cooldownLength;
        endTime = Time.time + length;
        Visual();
        GetComponent<Rigidbody>().isKinematic = true;
        
    }

    private void Visual()
    {
        visualManager.WindupParticles();
    }

    public override void Run()
    {
        if (GetComponent<LaunchableAnimal>().StoredForce != Vector3.zero)// If we are hit by something, we are inturrupted, and must return to the default State.
        {
            stateManager.TransitionToState(stateManager.defaultState);
        }
        if (Time.time > startingPauseReleaseTime)
        {
            transform.position -= transform.forward * speed * Time.fixedDeltaTime;
            if(Time.time > endTime)
            {
                stateManager.TransitionToState(GetComponent<Charge>());
            }
        }
    }

    public override bool TransitionConditionsMet()
    {
        return Time.time > cooldownReleaseTime && inputManager.GetSpecialButtonDown();
    }
}
