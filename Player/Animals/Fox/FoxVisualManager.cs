using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manager of all things visual for the Fox.
/// </summary>
public class FoxVisualManager : AnimalVisualManager
{
    [Header("Particle Systems.")]
    [Tooltip("Particle System for the bark attack.")]
    public GameObject BarkParticleSystem;

    new void Update()
    {
        base.Update();
    }

    /// <summary>
    /// Begins the Fox's dash animation.
    /// </summary>
    public void DashVisual()
    {
        transform.Find(animatorObjectName).GetComponent<Animator>().SetTrigger("DashTrigger");
    }

    /// <summary>
    /// Begins the Fox's bark animation and triggers the bark particles.
    /// </summary>
    public void BarkVisual()
    {
        transform.Find(animatorObjectName).GetComponent<Animator>().SetTrigger("BarkTrigger");
        BarkParticleSystem.SetActive(false);
        BarkParticleSystem.SetActive(true);
    }

    /// <summary>
    /// Begins the Fox's dig animation.
    /// </summary>
    public void DigVisual()
    {
        transform.Find(animatorObjectName).GetComponent<Animator>().SetTrigger("DigTrigger");
    }

    public override void BasicAttackVisual()
    {
        BarkVisual();
    }
}
