using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Extends the BasicAttack State with Ram-specific visuals.
/// </summary>
public class Headbutt : BasicAttack
{
    private RamVisualManager visualManager;

    new void Start()
    {
        base.Start();
        visualManager = GetComponent<RamVisualManager>();
    }

    protected override void Visual()
    {
        visualManager.ChargeParticles();
    }
}
