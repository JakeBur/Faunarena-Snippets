using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// For use with the animation event system.
/// </summary>
public class RamAnimationEventResponder : MonoBehaviour
{
    private RamVisualManager visualManager;

    void Start()
    {
        visualManager = GetComponentInParent<RamVisualManager>();
    }

    public void TriggerHeadbuttParticles()
    {
        visualManager.TriggerHeadbuttParticles();
    }
}
