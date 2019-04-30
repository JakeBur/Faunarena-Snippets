using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A short linear dash that chains into the Headbutt State.
/// </summary>
public class Charge : Dash
{
    public override void Deinitialize()
    {
        base.Deinitialize();
        stateManager.TransitionToState(GetComponent<Headbutt>());
    }

    protected override void Visual()
    {
        // this remains empty in order to not call the fox-specific dash Visual()
    }
}
