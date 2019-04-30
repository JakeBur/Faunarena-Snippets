using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMove : State
{
    public float speed;
    private Bounds bounds;

    public override void Deinitialize()
    {

    }

    public override void Initialize()
    {
        //Getting the bounds of the arena object to check for distance from the center later
        GameObject arena = GameObject.Find("Arena Boundaries");
       //print(arena);
        bounds = arena.GetComponent<MeshRenderer>().bounds;
        //print("Center of arena: " + bounds.center);
    }

    public override void Run()
    {
        MoveCursor();
    }

    public void MoveCursor(float moveSpeed = -1)
    {
        if (moveSpeed < 0)
        {
            moveSpeed = speed;
        }

        Vector3 inputVector = new Vector3(inputManager.InputVector.x, 0, inputManager.InputVector.y);

        //Check to see if the new position will be within the desired range, and if it is then allow movement of cursor
        if (CheckDistance(transform.position + inputVector) ) {
            transform.position += inputVector * moveSpeed * Time.deltaTime;
        }
        else {
            //DEBUG
            //print("out of bounds: ");
        }

        }

    private bool CheckDistance(Vector3 inputVector) {
        //Good 'ol distance formula to check how far from the center the Death Cursor is
        double d = Math.Sqrt( Math.Pow((bounds.center.x - inputVector.x), 2) + Math.Pow((bounds.center.z - inputVector.z), 2) );
        //print(d);

        if (d < 12.5)
            return true;
        else
            return false;
    }
    public override bool TransitionConditionsMet()
    {
        return false;
    }
}