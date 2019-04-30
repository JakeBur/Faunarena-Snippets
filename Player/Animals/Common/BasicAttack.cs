using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : State
{
    public AudioClip sound;

    public float cooldownLength;
    public float length;
    public float width;
    public float range;
    public float force;
    public float maxForce;
    public float selfForce;
    public int numberOfRaycasts;
    public GameObject hitpart;

    //private float forceValue = 80;
    //private float maxForce = 40;
    private float raycastDistance = 6;
    private float rayTime = .1f;

    private float cooldownReleaseTime;
    private float endTime;

    public override void Initialize()
    {
        print("here");
        endTime = Time.time + length;
        Visual();
        Audio();//TODO this audio code should be replaced with call to an FMOD wrapper class once we add FMOD
        cooldownReleaseTime = Time.time + cooldownLength;

        List<GameObject> hitObjects = AttackRaycaster.Fan(width, range, numberOfRaycasts, transform.position+new Vector3(0,.5f,0), transform.forward, gameObject);
        LaunchHitObjects(hitObjects);
    }   

    protected virtual void Visual()
    {
        GetComponent<AnimalVisualManager>().BasicAttackVisual();
    }

    private void LaunchHitObjects(List<GameObject> hitObjects)
    {
        // hit everything in range
        foreach (GameObject obj in hitObjects)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            Vector3 direction = new Vector3(obj.transform.position.x - transform.position.x, 0, obj.transform.position.z - transform.position.z);

            if (obj.transform.tag == "Player")
            {
                obj.GetComponent<Launchable>().Launch(Mathf.Clamp(force, 0, maxForce), direction);
            }
            else
            {
                print("hit projectile");
                obj.GetComponent<Launchable>().Launch(Mathf.Clamp(force / distance, 0, maxForce), direction);
            }
        }

        if (hitObjects.Count != 0)
        {          
            Vector3 direction = transform.forward*-1;
            GetComponent<Launchable>().Launch(selfForce, direction);
        }
    }

    private void Audio()
    {
        GetComponent<AudioSource>().clip = sound;
        GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(.8f, 1.2f);
        GetComponent<AudioSource>().Play();
    }

    public override void Run()
    {
        if(Time.time > endTime)
        {
            stateManager.TransitionToState(stateManager.defaultState);
        }
    }

    public override void Deinitialize()
    {

    }

    public override bool TransitionConditionsMet()
    {
        return false;
    }
}
