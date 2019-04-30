using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// State superclass for use with the StateManager class.
/// Its behavior is run every frame by the StateManager when it is active.
/// </summary>
public abstract class State : MonoBehaviour {

    [Header("Adjacent States")]
    [Tooltip("States that we can branch to from the current state.")]
    public List<State> branches;// a list of states that should branch off of this one as soon as their input criteria are met

    [Tooltip("Costates that can be launched to run concurrently with the current State.")]
    public List<Costate> costates;// a list of states that should branch off of this one as soon as their input criteria are met

    protected StateManager stateManager;// The StateManager attached to the current player character.
    protected InputManager inputManager;// The inputManager attached to the current player character.

    protected void Start()
    {
        stateManager = GetComponent<StateManager>();
        inputManager = GetComponent<InputManager>();
    }

    /// <summary>
    /// Is called every frame by the StateManager when this State is active;
    /// </summary>
    public abstract void Run();

    /// <summary>
    /// Is called when this State is transitioned to.
    /// </summary>
    public abstract void Initialize();

    /// <summary>
    /// Is called when this State is transitioned away from.
    /// </summary>
    public abstract void Deinitialize();

    /// <summary>
    /// Returns true when the conditions for transition to the State are met.
    /// </summary>
    public virtual bool TransitionConditionsMet()
    {
        return false;
    }

    /// <summary>
    /// Runs when the player character is stunned. Any special things that must be done when we are stunned in this State should go here.
    /// </summary>
    public virtual void EnterStun() { }

    /// <summary>
    /// Runs when the player character exits the stunned state. Any special things that must be done when we exit stun in this State should go here.
    /// </summary>
    public virtual void EndStun() { }

    /// <summary>
    /// Tells the StateManager to transition to this State.
    /// </summary>
    public void TransitionToMe ()
    {
        GetComponent<StateManager>().TransitionToState(this);
    }
}
