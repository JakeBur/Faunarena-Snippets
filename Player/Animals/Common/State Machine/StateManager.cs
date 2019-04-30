using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Runs the active State every frame, along with any active Costates.
/// </summary>
public class StateManager : MonoBehaviour {

    [Header("Defaults")]
    public State defaultState;
    public List<Costate> defaultCostates;

    public State CurrentState
    {
        get
        {
            return currentState;
        }
    }

    // Current State and Costate Management.
    private State currentState;// The currently running State.
    private State previousState;// The State we were running before the current one.
    private List<Costate> currentCostates;// A list of the currently running Costates.
    private Queue<State> transitionQueue;// A list of States waiting to be transitioned to.

    // Useful references.
    private StunManager stunManager;// The StunManager attached to the current player.

    void Start ()
    {
        transitionQueue = new Queue<State>();
        currentState = defaultState;
        currentState.Initialize();
        currentCostates = defaultCostates;

        stunManager = GetComponent<StunManager>();
    }

    void FixedUpdate()
    {
        if(!stunManager.Stunned)
        {
            currentState.Run();
            RunCostates();
            TransitionUnderConditions();
            AdvanceTransitionQueue();
        }
	}

    /// <summary>
    /// Iterates through all current Costates and runs their behavior. 
    /// Also removes any complete Costates from the list of current Costates.
    /// </summary>
    private void RunCostates()
    {
        List<Costate> updatedCurrentCostates = new List<Costate>();
        foreach(Costate costate in currentCostates)
        {
            if(!costate.complete)
            {
                updatedCurrentCostates.Add(costate);
                costate.Run();
            }
        }
        currentCostates = updatedCurrentCostates;
    }

    /// <summary>
    /// Checks all adjacent States and Costates, transitioning to any States whose transition conditions are met,
    /// and triggering any Costates whose trigger conditions are met.
    /// </summary>
    private void TransitionUnderConditions()
    {
        foreach (State branch in currentState.branches)
        {
            if (branch.TransitionConditionsMet())
            {
                TransitionToState(branch);
            }
        }
        foreach (Costate costate in currentState.costates)
        {
            if(costate.TriggerConditionsMet())
            {
                LaunchCostate(costate);
            }
        }
    }

    /// <summary>
    /// Adds the given State to the transition queue, where it will wait to be transitioned to.
    /// </summary>
    /// <param name="transitionTo">The State to transition to.</param>
    public void TransitionToState(State transitionTo)
    {
        transitionQueue.Enqueue(transitionTo);
    }

    /// <summary>
    /// Transitions to the next State in the transition queue if there are any available.
    /// </summary>
    private void AdvanceTransitionQueue()
    {
        if(transitionQueue.Count != 0)
        {
            State transitionTo = transitionQueue.Dequeue();
            currentState.Deinitialize();
            previousState = currentState;
            currentState = transitionTo;
            transitionTo.Initialize();
        }
    }

    /// <summary>
    /// Does all housekeeping required to launch the given Costate.
    /// </summary>
    /// <param name="costate">The Costate to be launched.</param>
    public void LaunchCostate(Costate costate)
    {
        currentCostates.Add(costate);
        costate.complete = false;
        costate.Initialize();
    }

    /// <summary>
    /// Returns the State that was most recently transitioned from.
    /// </summary>
    public State GetPreviousState()
    {
        return previousState;
    }
}
