using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Places a hole in the ground which acts as a trap for other players.
/// </summary>
public class DigHole : State
{
    public AudioClip growlSound;

    [Header("Time")]
    [Tooltip("The time to spend placing the trap.")]
    public float length;
    [Tooltip("The time before we can place a trap again.")]
    public float cooldownLength;

    [Header("Prefrabs")]
    [Tooltip("Prefab for the trap object.")]
    public GameObject trapPrefab;

    // Time
    private float cooldownReleaseTime;// The time when we can place a trap again.
    private float endTime;// The time when we should end the Dig.

    public override void Deinitialize()
    {
        
    }

    public override void Initialize()
    {
        cooldownReleaseTime = Time.time + cooldownLength;
        endTime = Time.time + length;
        GetComponent<FoxVisualManager>().DigVisual();

        //Play sounds//TODO this audio code should be replaced with call to an FMOD wrapper class once we add FMOD
        GetComponent<AudioSource>().clip = growlSound;
        GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(.8f, 1.2f);
        GetComponent<AudioSource>().Play();

        // spawn trap and place it on player
        GameObject trapObject = Instantiate(trapPrefab);
        trapObject.transform.position = transform.position;
        trapObject.GetComponent<TrapScript>().dontTriggerPlayer = gameObject;
    }

    public override void Run()
    {
        if (Time.time > endTime)
        {
            stateManager.TransitionToState(stateManager.defaultState);
        }
    }

    public override bool TransitionConditionsMet()
    {
        return false;
        //return Time.time > cooldownReleaseTime && inputManager.GetSpecialButtonDown();
    }
}
