using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgementNut : State
{
    public GameObject shadow;
    public float ultSpeed;
    List<Vector3> targetPositions;
    Vector3 centerPos;
    int index = 0;

    [Header("Obstruction check raycast settings")]
    public float obstructionCheckWidth;
    public float obstructionCheckDistance;
    public float obstructionCheckRays;

    public float hitRaycastWidth;
    public float hitRaycastDistance;
    public float hitStrength;

    public GameObject hitpart;

    public override void Deinitialize()
    {
        GetComponent<BoxCollider>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
        shadow.transform.SetParent(transform);
        shadow.transform.localPosition = Vector3.zero;
        shadow.transform.localScale = new Vector3(1, 1, 1);
        shadow.SetActive(false);
        index = 0;
    }

    public override void Initialize()
    {
        targetPositions = GetComponent<JudgementPlacement>().Positions;
        GetComponent<Rigidbody>().isKinematic = true;
        targetPositions.Add(GetComponent<JudgementPlacement>().stageCenter.transform.position);
    }

    public override void Run()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPositions[index], Time.fixedDeltaTime * ultSpeed);
        transform.LookAt(new Vector3(targetPositions[index].x, transform.position.y, targetPositions[index].z));

        if (transform.position == targetPositions[index])
        {
            index++;
        }

        if(index == targetPositions.Count) stateManager.TransitionToState(stateManager.defaultState);

        if (AttackRaycaster.Fan(obstructionCheckWidth, obstructionCheckDistance, obstructionCheckRays, transform.position, Vector3.zero, transform.forward, gameObject).Count != 0)
        {
            List<GameObject> hitObjects = AttackRaycaster.Fan(hitRaycastWidth, hitRaycastDistance, obstructionCheckRays, transform.position, Vector3.zero, transform.forward, gameObject);
            foreach (GameObject obj in hitObjects)
            {
                Vector3 direction = new Vector3(obj.transform.position.x - transform.position.x, 0, obj.transform.position.z - transform.position.z);
                print(obj);
                obj.GetComponent<Launchable>().Launch(hitStrength, direction);
                Instantiate(hitpart, obj.transform);
                Debug.Log(hitpart.transform.position);
            }
        }
    }
}
