using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// When a player is stunned by anything, this class manages the behavior.
/// Depending on the stun, the player may be able to wiggle the stick to break free of the stun.
/// </summary>
public class StunManager : MonoBehaviour
{
    public bool Stunned
    {
        get
        {
            return stunned;
        }
    }


    private bool stunned;// Whether the player is currently stunned

    // Time
    private float maxStunEndTime;// The maximum length of the stun. The stun will not last longer than this.
    private float minStunEndTime;// The minimum length of the stun. If the player mashes successfully, this is the shortest the stun can last.

    // Mashing To Escape
    private bool escapable;// Whether or not the player can mash to escape the stun.
    private int warbleThreshold;// How many wiggles of the stick it takes to escape.
    private int warbleCount;// How many wiggles of the stick the player has already done.
    private bool warbleDirection;// Toggles every time the stick is wiggled to require the player to wiggle in the other direction.

    // References
    private InputManager inputManager;// The InputManager attached to the player.
    private StateManager stateManager;// The StateManager attached to the player.

    void Start()
    {
        stateManager = GetComponent<StateManager>();
        inputManager = GetComponent<InputManager>();
    }

    void Update()
    {
        if (stunned)
        {
            if (warbleDirection && inputManager.InputVector.x > 0.8f)
            {
                warbleCount++;
                warbleDirection = false;
            }
            else if (!warbleDirection && inputManager.InputVector.x < -0.8f)
            {
                warbleCount++;
                warbleDirection = true;
            }

            if (Time.time > maxStunEndTime || (warbleCount > warbleThreshold && Time.time > minStunEndTime))
            {
                EndStun();
            }
        }
    }

    /// <summary>
    /// Does all housekeeping related to entering a stunned state.
    /// </summary>
    /// <param name="maxStunLength">The maximum time the stun can last.</param>
    /// <param name="minStunLength">The minimum time the stun can last.</param>
    /// <param name="escapable">Whether this stun can be escaped by mashing.</param>
    /// <param name="warbleThreshold">How many wiggles of the stick it takes to escape from this stun.</param>
    public void EnterStun(float maxStunLength, float minStunLength, bool escapable = false, int warbleThreshold = 10)
    {
        stunned = true;
        warbleDirection = false;
        warbleCount = 0;

        maxStunEndTime = Time.time + maxStunLength;
        minStunEndTime = Time.time + minStunLength;
        this.escapable = escapable;
        this.warbleThreshold = warbleThreshold;

        stateManager.CurrentState.EnterStun();
    }

    /// <summary>
    /// Does all housekeeping related to exiting a stunned state;
    /// </summary>
    public void EndStun()
    {
        stunned = false;
        stateManager.CurrentState.EndStun();
    }
}
