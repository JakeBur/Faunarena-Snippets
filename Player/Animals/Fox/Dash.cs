using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A short linear dash. If anything is hit, it is launched and the animal transitions to BackUp.
/// </summary>
public class Dash : State
{
    public AudioClip dashSound;

    [Header("Behavior")]
    [Tooltip("Move speed while dashing.")]
    public float speed;

    [Header("Time")]
    [Tooltip("Time to wait before beggining the dash proper.")]
    public float startupLength;

    [Tooltip("Total time to spend dashing.")]
    public float length;

    [Tooltip("Time before we can dash again.")]
    public float cooldownLength;


    [Header("Obstruction check raycast settings")]
    public float obstructionCheckWidth;
    public float obstructionCheckDistance;
    public float obstructionCheckRays;

    public float hitRaycastWidth;
    public float hitRaycastDistance;
    public float hitStrength;

    // Time
    private float startupReleaseTime;// The time when we should proceed with the dash proper.
    private float finishTime;// The time when the dash should finish.
    private float cooldownReleaseTime;// The time when the dash is off cooldown and can be used again.

    public GameObject hitpart;

    public override void Deinitialize()
    {
        GetComponent<Rigidbody>().isKinematic = false;
    }

    public override void Initialize()
    {
        Visual();

        //TODO this audio code should be replaced with call to an FMOD wrapper class once we add FMOD
        GetComponent<AudioSource>().clip = dashSound;
        GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(.8f, 1.2f);
        GetComponent<AudioSource>().Play();

        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().isKinematic = true;
        
        cooldownReleaseTime = Time.time + cooldownLength;
        finishTime = Time.time + length;
        startupReleaseTime = Time.time + startupLength;
    }

    protected virtual void Visual()
    {
        GetComponent<FoxVisualManager>().DashVisual();
    }

    public override void Run()
    {
        /*//play SFX
        GetComponent<AudioSource>().clip = dashSound;
        GetComponent<AudioSource>().pitch = Random.Range(.8f, 1.2f);
        GetComponent<AudioSource>().Play();*/

        if (Time.time < startupReleaseTime)
        {
            return;
        }
        
        transform.position += transform.forward * Time.fixedDeltaTime * speed;

        if (Time.time > finishTime)
        {
            stateManager.TransitionToState(stateManager.defaultState);
            return;
        }

        if (AttackRaycaster.Fan(obstructionCheckWidth, obstructionCheckDistance, obstructionCheckRays, transform.position, Vector3.zero, transform.forward, gameObject).Count != 0)
        {
            List<GameObject> hitObjects = AttackRaycaster.Fan(hitRaycastWidth, hitRaycastDistance, obstructionCheckRays, transform.position, Vector3.zero, transform.forward, gameObject);
            foreach (GameObject obj in hitObjects)
            {
                Vector3 direction = new Vector3(obj.transform.position.x - transform.position.x, 0, obj.transform.position.z - transform.position.z);
                print(obj);
                obj.GetComponent<Launchable>().Launch(hitStrength, direction);
                Instantiate(hitpart,obj.transform);
                Debug.Log(hitpart.transform.position);
            }
            stateManager.TransitionToState(GetComponent<BackUp>());
        }
    }

    public override bool TransitionConditionsMet()
    {
        return Time.time > cooldownReleaseTime && inputManager.GetAttackButtonDown();
    }
}
