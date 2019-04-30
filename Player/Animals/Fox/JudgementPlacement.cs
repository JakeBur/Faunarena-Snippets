using System.Collections.Generic;
using UnityEngine;

public class JudgementPlacement : State
{
    public float cooldown;
    public float stageRadius;

    public GameObject stageCenter;
    public GameObject mesh;
    public GameObject shadow;

    public float lineTime;

    // privates
    private Vector2 direction;
    private float cooldownReleaseTime;
    private List<Vector3> positions;
    private Vector3 targetPos;
    private float numberAllowed = 20;
    public float angleCheck = 30;

    public List<Vector3> Positions
    {
        get
        {
            return positions;
        }
        set
        {
            positions = value;
        }
    }

    private new void Start()
    {
        base.Start();
        stageCenter = GameObject.Find("stageCenter");//TODO temp
    }

    public override void Deinitialize()
    {
        shadow.transform.SetParent(null);
        mesh.SetActive(true);
    }

    public override void Initialize()
    {
        GetComponent<Ultimate>().numberOfOrbs = 0;
        // turn into a shadow
        shadow.SetActive(true);
        GetComponent<BoxCollider>().enabled = false;
        mesh.SetActive(false);

        Positions = new List<Vector3>();
        direction = new Vector2(0, 1); // start up

        Positions.Add(stageCenter.transform.position);

        // initial target position
        targetPos = stageCenter.transform.position + (new Vector3(direction.x, 0, direction.y) * stageRadius);
        Positions.Add(targetPos);
    }

    public override void Run()
    {
        // repear until 10 lines are formed
        if(numberAllowed > Positions.Count)
        {
            // as long as your holding a direction
            if(inputManager.InputVector != Vector2.zero)
            {
                float angle = Vector2.Angle(inputManager.InputVector, direction);

                if (angle > angleCheck)
                {
                    direction = inputManager.InputVector;
                    targetPos = stageCenter.transform.position + (new Vector3(direction.x, 0, direction.y) * stageRadius);
                    Positions.Add(targetPos);
                    Debug.DrawLine(Positions[Positions.Count - 2], Positions[Positions.Count - 1], Color.red, lineTime);
                }
            }
        }else
        {
            stateManager.TransitionToState(GetComponent<JudgementNut>());
        }
    }
}

