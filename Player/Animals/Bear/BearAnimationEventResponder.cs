using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// For use with the animation event system.
/// </summary>
public class BearAnimationEventResponder : MonoBehaviour
{
    // References
    private Slam slam;// The Slam State atatched to the player character.
    private BearVisualManager visualManager;// The BearVisualManager atatched to the player character.

    void Start()
    {
        slam = GetComponentInParent<Slam>();
        visualManager = GetComponentInParent<BearVisualManager>();
    }

    // Slam Hitboxes
    public void ActivateSlamInnerHitbox()
    {
        slam.ActivateInnerHitbox();
    }
    public void ActivateSlamOuterHitbox()
    {
        slam.ActivateOuterHitbox();
    }
    public void DectivateSlamInnerHitbox()
    {
        slam.DeactivateInnerHitbox();
    }
    public void DectivateSlamOuterHitbox()
    {
        slam.DeactivateOuterHitbox();
    }

    // Slam Visuals
    public void ActivateSlamVisuals()
    {
        visualManager.ActivateSlamVisuals();
    }
    public void DeactivateSlamVisuals()
    {
        visualManager.DeactivateSlamVisuals();
    }
}
