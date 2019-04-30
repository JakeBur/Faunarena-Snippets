using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Body slam attack with stun component.
/// </summary>
public class Slam : State
{
    [Header("Time")]
    [Tooltip("Time before we can slam again.")]
    public float cooldownLength;
    [Tooltip("Time to spend slamming.")]
    public float length;
    [Tooltip("Check this to override the given length with the length of the given animation clip.")]
    public bool useAnimationForLength;

    [Header("References")]
    [Tooltip("Slam animation clip for the purposes of determining move length.")]
    public AnimationClip animationClip;
    [Tooltip("Hitbox for the inner part of the Slam.")]
    public GameObject innerHitbox;
    [Tooltip("Hitbox for the outer part of the Slam.")]
    public GameObject outerHitbox;

    // References
    private BearVisualManager visualManager;// The BearVisualManager attached to the player character.

    private float finishTime;// The time when we should stop slamming.
    private float cooldownReleaseTime;// The time when we can slam again.

    new void Start()
    {
        base.Start();
        visualManager = GetComponent<BearVisualManager>();
        if(useAnimationForLength)
        {
            length = animationClip.length;
        }
    }

    public override void Deinitialize()
    {
        DeactivateInnerHitbox();
        DeactivateOuterHitbox();
    }

    public override void Initialize()
    {
        finishTime = Time.time + length;
        cooldownReleaseTime = Time.time + cooldownLength;
        visualManager.StartSlamAnimation();
    }

    //TODO:
    // Temporary Message: You'll notice a conspicuous lack of any code here.
    // This is because management of the visuals and hitboxes are now managed as animation events.
    public override void Run()
    {
        if(Time.time > finishTime)
        {
            GetComponent<StateManager>().TransitionToState(GetComponent<StateManager>().defaultState);
        }
    }

    // Hitbox Management
    // To be called by the BearAnimationEventResponder.
    public void ActivateInnerHitbox()
    {
        innerHitbox.SetActive(true);
    }
    public void ActivateOuterHitbox()
    {
        outerHitbox.SetActive(true);
    }
    public void DeactivateInnerHitbox()
    {
        innerHitbox.SetActive(false);
    }
    public void DeactivateOuterHitbox()
    {
        outerHitbox.SetActive(false);
    }

    public override bool TransitionConditionsMet()
    {
        return Time.time > cooldownReleaseTime && inputManager.GetSpecialButtonDown();
    }
}
