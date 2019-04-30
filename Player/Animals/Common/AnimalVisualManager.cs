using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages all things visual for animals.
/// Extend this with animal specific methods.
/// </summary>
public abstract class AnimalVisualManager : MonoBehaviour
{
    [Tooltip("The name of the object with the animal's Animator attached.")]
    public string animatorObjectName;

    protected StateManager stateManager;
    protected InputManager inputManager;

    private ParticleSystem dustCloudParticleSystem;

    protected void Start()
    {
        dustCloudParticleSystem = transform.Find("RunSmoke").GetComponent<ParticleSystem>();
        stateManager = GetComponent<StateManager>();
        inputManager = GetComponent<InputManager>(); 
    }

    protected void Update()
    {
        MoveVisual();
        DustVisual();
    }

    /// <summary>
    /// Sets the animal's Run animation bool based on whether the player is moving or not.
    /// </summary>
    public void MoveVisual()
    {
        if(stateManager.CurrentState == GetComponent<Move>() && inputManager.InputVector != Vector2.zero)
        {
            transform.Find(animatorObjectName).GetComponent<Animator>().SetBool("Running", true);
        }
        else
        {
            transform.Find(animatorObjectName).GetComponent<Animator>().SetBool("Running", false);
        }
    }

    /// <summary>
    /// Sets the animal's dust particles based on whether the player is moving or not.
    /// </summary>
    public void DustVisual()
    {
        if(stateManager.CurrentState == GetComponent<Dash>() || (stateManager.CurrentState == GetComponent<Move>() && inputManager.InputVector != Vector2.zero))
        {
            if (!dustCloudParticleSystem.isPlaying)
            {
                dustCloudParticleSystem.Play();
            }
        }
        else
        {
            if (dustCloudParticleSystem.isPlaying)
            {
                dustCloudParticleSystem.Stop();
            }
        }
    }

    /// <summary>
    /// Since the behavior in BasicAttack is used on multiple animals, the visuals for it must be overridden here.
    /// </summary>
    public abstract void BasicAttackVisual();
}
