using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Substitution : State
{
    public GameObject logPrefab;
    public float autoTrigger;
    private float cooldownReleaseTime;
    private float smokeTriggerTime;
    private bool triggered;
    public float cooldownLength;

    public override void Deinitialize()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        triggered = false;
    }

    public override void Initialize()
    {
        smokeTriggerTime = Time.time + autoTrigger;
        GetComponent<Rigidbody>().isKinematic = true;
        cooldownReleaseTime = Time.time + cooldownLength;
    }

    public override void Run()
    {
        if (Time.time > smokeTriggerTime && !triggered)
        {
            stateManager.TransitionToState(stateManager.defaultState);
            Visual();
            //particle effects go off
        }
        if (GetComponent<LaunchableFox>().StoredForce != Vector3.zero && !triggered)
        {

            GetComponent<LaunchableFox>().StoredForce = Vector3.zero;
            triggered = true;

            // temp
            Vector3 dir = GetComponent<LaunchableFox>().info.direction;
            Vector3 newLogPos = transform.position;
            transform.position += -dir * 3f;
            GameObject log = Instantiate(logPrefab);
            log.transform.position = newLogPos;
            transform.LookAt(newLogPos);
            // temp

            stateManager.TransitionToState(stateManager.defaultState);
        }
    }
    private void Visual()
    {
        GetComponent<AnimalVisualManager>().BasicAttackVisual();
    }

    public override bool TransitionConditionsMet()
    {
        return Time.time > cooldownReleaseTime && inputManager.GetEvadeButtonDown();
    }
}
