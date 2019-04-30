using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSneak : State
{
    [Header("Time")]
    [Tooltip("The time spent sinking into the ground")]
    public float length;
    [Tooltip("The time before fox pops out of the ground")]
    public float popUpLength;
    private float popUpTime;// The time when fox pops up
    private float endTime;// The time when we should end the shadow sneak
    public float cooldownLength;
    public float cooldownReleaseTime;
    [SerializeField] private float shadowSpeed;
    public GameObject mesh;
    public GameObject shadow;
    public GameObject poof;

    public override void Deinitialize()
    {
        shadow.SetActive(false);
        GetComponent<BoxCollider>().enabled = true;
        mesh.SetActive(true);
        cooldownLength = 10f;
        poof.SetActive(false);
    }
    public override void Initialize()
    {
        length = Time.time + .5f;
        cooldownReleaseTime = Time.time + cooldownLength;
        popUpTime = Time.time + popUpLength;
        endTime = Time.time + length+popUpTime;
        poof.SetActive(true);
        mesh.SetActive(false);
    }
    public override void Run()
    {
        if (Time.time > length)
        {
            StolenMove();
            GetComponent<BoxCollider>().enabled = false;
            mesh.SetActive(false);
            shadow.SetActive(true);
        }
        if (Time.time > popUpTime)
        {
            stateManager.TransitionToState(GetComponent<FoxPop>());
        }
    }
    public override bool TransitionConditionsMet()
    {
        return Time.time > cooldownReleaseTime && inputManager.GetSpecialButtonDown();//TODO 
    }

    private void StolenMove()
    {
        Vector3 inputVector = new Vector3(inputManager.InputVector.x, 0, inputManager.InputVector.y);
        transform.position += inputVector * shadowSpeed * Time.deltaTime;
        if (inputVector != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(inputVector);
        }
    }
}
