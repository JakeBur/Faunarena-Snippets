using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Horizontal arm swipe that moves a hitbox around in a semi-circle in alternating directions.
/// </summary>
public class Swipe : Costate
{
    [Header("Behavior")]
    [Tooltip("Force with which objects are launched when hit by the swipe.")]
    public float swipeForce;
    [Tooltip("The speed at which the swipe hitbox moves.")]
    public float swipeSpeed;

    [Header("Time")]
    [Tooltip("The time to spend swiping.")]
    public float length;
    [Tooltip("The time before we can swipe again.")]
    public float cooldownLength;

    [Header("References")]
    public GameObject armHitbox;

    public bool SwapDirection
    {
        get
        {
            return swapDirection;
        }
    }


    private bool swapDirection;// Whether to swap the direction of the swipe on the next attack.
    private float cooldownReleaseTime;// The time when we can swipe again.
    private float endTime;// The time when we should stop swiping.

    // Initial positions and rotations for the swipe hitbox.
    private Vector3 rightPos = new Vector3(2f, 1f, 2.5f);
    private Quaternion rightRot = Quaternion.Euler(90, 45, 0);
    private Vector3 leftPos = new Vector3(-2f, 1f, 2.5f);
    private Quaternion leftRot = Quaternion.Euler(90, -45, 0);

    new void Start()
    {
        base.Start();
    }

    private void Deinitialize()
    {
        complete = true;
        swapDirection = !swapDirection;
        armHitbox.SetActive(false);
    }

    public override void Initialize()
    {
        /*GetComponent<AudioSource>().clip = swipeSound;
        GetComponent<AudioSource>().pitch = Random.Range(.8f, 1.2f);
        GetComponent<AudioSource>().Play();*/

        endTime = Time.time + length;
        cooldownReleaseTime = Time.time + cooldownLength;

        armHitbox.SetActive(true);
        if (swapDirection)
        {
            armHitbox.transform.localPosition = rightPos;
            armHitbox.transform.localRotation = rightRot;
        }
        else
        {
            armHitbox.transform.localPosition = leftPos;
            armHitbox.transform.localRotation = leftRot;
        }

        armHitbox.GetComponent<SwipeHitboxManager>().ClearHitPlayers();
    }

    public override void Run()
    {
        int swapSign = 1;
        if(swapDirection)
        {
            swapSign = -1;
        }
        armHitbox.transform.RotateAround(transform.position, Vector3.up, swapSign * swipeSpeed * Time.deltaTime);

        if (Time.time > endTime)
        {
            Deinitialize();
        }
    }

    public override bool TriggerConditionsMet()
    {
        return Time.time > cooldownReleaseTime && inputManager.GetAttackButtonDown();
    }
}
