using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxPop : State
{
    float cooldown;
    float wait = .5f;
    public float radius;
    public float raynum;
    public float force;
    public GameObject hitpart;
    public override void Deinitialize()
    {
    }
    public override void Initialize()
    {
        cooldown = Time.time;
        List<GameObject> hitObjects = AttackRaycaster.Fan(360, radius, raynum, transform.position + new Vector3(0, .5f, 0), transform.forward, gameObject);
        foreach (GameObject obj in hitObjects)
        {
//            float distance = Vector3.Distance(transform.position, obj.transform.position);
            Vector3 direction = new Vector3(obj.transform.position.x - transform.position.x, 0, obj.transform.position.z - transform.position.z);

            if (obj.transform.tag == "Player")
            {
                obj.GetComponent<Launchable>().Launch(force, direction);
            }
            else
            {
                print("hit projectile");
                obj.GetComponent<Launchable>().Launch(force, direction);
            }
            Instantiate(hitpart, obj.transform);
        }
                   
    }
    public override void Run()
    {
        if (cooldown + wait < Time.time)
        {
            stateManager.TransitionToState(stateManager.defaultState);
        }
    }

    public override bool TransitionConditionsMet()
    {
        return Time.time > cooldown && inputManager.GetSpecialButtonDown();
    }
}
