using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manager of all things visual for the Ram.
/// </summary>
public class RamVisualManager : AnimalVisualManager
{
    [Header("Behavior")]
    [Tooltip("Determines by percentage how low the charge meter must be before sweat appears.")]
    public float sweatPercentageThreshold;

    [Header("Particle Systems")]
    public GameObject headbuttParticles;
    public GameObject sweatParticles;
    public GameObject windupParticles;
    public GameObject chargeParticles;

    new void Update()
    {
        base.Update();
        SprintVisuals();
    }

    public void HeadbuttVisual()
    {
        transform.GetChild(0).GetComponent<Animator>().SetTrigger("Attack");
    }

    public void TriggerHeadbuttParticles()
    {
        headbuttParticles.SetActive(false);
        headbuttParticles.SetActive(true);
    }

    public void ActivateSprintVisuals()
    {
        transform.GetChild(0).GetComponent<Animator>().SetBool("Dashing", true);
    }

    /// <summary>
    /// Determines whether the Ram should sweat based on percentage of its sprint charge.
    /// </summary>
    public void SprintVisuals()
    {
        Sprint sprint = GetComponent<Sprint>();
        if (sprint.RemainingTime/sprint.length < sweatPercentageThreshold)
        {
            sweatParticles.SetActive(true);
        }
        else
        {
            sweatParticles.SetActive(false);
        }
    }

    public void DeactivateSprintVisuals()
    {
        transform.GetChild(0).GetComponent<Animator>().SetBool("Dashing", false);
    }

    public void WindupParticles()
    {
        windupParticles.SetActive(false);
        windupParticles.SetActive(true);
    }

    public void ChargeParticles()
    {
        chargeParticles.SetActive(false);
        chargeParticles.SetActive(true);
    }
    public void HeadbuttParticles()
    {
        headbuttParticles.SetActive(false);
        headbuttParticles.SetActive(true);
    }

    public override void BasicAttackVisual()
    {
        HeadbuttVisual();
    }
}
