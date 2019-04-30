using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Runs in tandem with the StateManager's current State.
/// </summary>
public abstract class Costate : MonoBehaviour
{
    [HideInInspector]
    public bool complete;// Is false while the Costate is running, and true when the Costate is ready to end.

    protected InputManager inputManager;// A reference to the InputManager attached to the player character.

    protected void Start()
    {
        inputManager = GetComponent<InputManager>();
    }

    /// <summary>
    /// Is called every frame by the StateManager while this Costate is active.
    /// </summary>
    public abstract void Run();

    /// <summary>
    /// Is called when this Costate is triggered.
    /// </summary>
    public abstract void Initialize();

    /// <summary>
    /// Returns true when the conditions for triggering this state are met.
    /// </summary>
    public abstract bool TriggerConditionsMet();
}

