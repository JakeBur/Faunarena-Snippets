using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manager of all things visual for the Ram.
/// </summary>
public class BearVisualManager : AnimalVisualManager
{
    [Header("Effects Objects")]
    [Tooltip("Effects for the inner Slam hitbox.")]
    public GameObject slamIFX;

    [Tooltip("Effects for the outer Slam hitbox.")]
    public GameObject slamOFX;

    [Tooltip("Effects for the Armor State.")]
    public GameObject armorFX;

    public void StartSlamAnimation()
    {
        transform.GetChild(0).GetComponent<Animator>().SetTrigger("BodySlam");
    }

    public void ActivateSlamVisuals()
    {
        slamIFX.SetActive(true);
        slamOFX.SetActive(true);
    }

    public void DeactivateSlamVisuals()
    {
        slamIFX.SetActive(false);
        slamOFX.SetActive(false);
    }

    public void ActivateArmorVisuals()
    {
        armorFX.SetActive(true);
        transform.GetChild(0).GetComponent<Animator>().SetTrigger("ArmorTrigger");
        transform.GetChild(0).GetComponent<Animator>().SetBool("Armor", true);
    }

    public void DeactivateArmorVisuals()
    {
        armorFX.SetActive(false);
        transform.GetChild(0).GetComponent<Animator>().SetBool("Armor", false);
    }

    public override void BasicAttackVisual()
    {
        
    }
}
