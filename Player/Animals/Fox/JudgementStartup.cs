using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgementStartup : State
{
    public GameObject shadow;
    public float shadowExpansion;
    public float cooldown = 2;
    float timer;
    public Vector2 Direction
    {
        get;
        private set;
    }

    public Vector2 Position
    {
        get;
        private set;
    }


    public override void Deinitialize()
    {
        
    }

    public override void Initialize()
    {
        print("JudgementStartup");
        Direction = inputManager.InputVector;
        shadow.SetActive(true);
        Position = Vector2.zero;
        timer = Time.time;
    }

    public override void Run()
    {
        ExpandShadow();
        if(timer + cooldown <= Time.time)
        {
            stateManager.TransitionToState(GetComponent<JudgementPlacement>());
        }
    }

    private void ExpandShadow()
    {
        shadow.transform.localScale += new Vector3(shadowExpansion, shadowExpansion, shadowExpansion);
    }

    public override bool TransitionConditionsMet()
    {
        return inputManager.GetUltButtonDown() && GetComponent<Ultimate>().CheckCanUlt();
    }
}
