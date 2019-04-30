using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Faster move mode with a depleting charge meter.
/// </summary>
public class Sprint : State
{
    [Header("Behavior")]
    [Tooltip("Move speed while sprinting.")]
    public float speed;
    [Tooltip("Rate of sprint recharge as a percentage of real time.")]
    public float rechargeRate;// (For example, a recharge rate of 0.5 means that we regain one second of sprint time every two seconds while not charging.)

    [Header("Time")]
    [Tooltip("Amount of time we can sprint.")]
    public float length;

    public float RemainingTime // The current remaining time to sprint.
    {
        get
        {
            if(this == stateManager.CurrentState)
            {
                float timeSinceSprintBegan = Time.time - timeOfLastInitialization;
                return remainingTime - timeSinceSprintBegan;
            }
            else
            {
                float timeSinceLastSprint = Time.time - timeOfLastDeinitialization;
                return Math.Min(length, timeSinceLastSprint * rechargeRate + remainingTime);
            }
        }
    }

    // Time
    private float endTime;// The time when we should end the sprint.
    private float remainingTime;// The remaining charge for the sprint move.
    private float timeOfLastDeinitialization;// The time when we last deinitialized the sprint.
    private float timeOfLastInitialization;// The time when we last initialized the sprint.

    // References
    private Move moveState;// The Move State attached to the player character.

    new void Start()
    {
        base.Start();
        moveState = GetComponent<Move>();
        remainingTime = length;
    }

    public override void Deinitialize()
    {
        timeOfLastDeinitialization = Time.time;
        remainingTime = endTime - Time.time;
    }

    public override void Initialize()
    {
        timeOfLastInitialization = Time.time;
        float timeSinceLastSprint = Time.time - timeOfLastDeinitialization;
        remainingTime = Math.Min(length, timeSinceLastSprint * rechargeRate + remainingTime);
        endTime = Time.time + remainingTime;
    }

    public override void Run()
    {
        moveState.MoveAnimal(speed);

        if (Time.time > endTime || !inputManager.GetEvadeButton())
        {
            stateManager.TransitionToState(stateManager.defaultState);
        }
    }

    public override bool TransitionConditionsMet()
    {
        return inputManager.GetEvadeButtonDown();
    }
}
