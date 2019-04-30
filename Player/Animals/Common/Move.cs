using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : State
{
    public float speed;
    public bool goo;

    public override void Deinitialize()
    {

    }

    public override void Initialize()
    {

    }

    public override void Run()
    {
        MoveAnimal();
    }

    public void MoveAnimal(float moveSpeed = -1)
    {
        if(moveSpeed < 0)
        {
            moveSpeed = speed;
        }
        if (goo)
        {
            Vector3 inputVector = new Vector3(inputManager.InputVector.x, 0, inputManager.InputVector.y);
            transform.position += inputVector * 2.5f * Time.deltaTime;
            if (inputVector != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(inputVector);
            }
        }
        else
        {
            Vector3 inputVector = new Vector3(inputManager.InputVector.x, 0, inputManager.InputVector.y);
            transform.position += inputVector * moveSpeed * Time.deltaTime;
            if (inputVector != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(inputVector);
            }
        }
    }

    public override bool TransitionConditionsMet()
    {
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Goop(Clone)")
        {
            goo = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Goop(Clone)")
        {
            goo = false;
        }
    }
}
