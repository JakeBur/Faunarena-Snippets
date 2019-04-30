using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Utility class for raycasting to find players and projectiles that should be hit by an attack.
/// </summary>
public static class AttackRaycaster
{
    private static float xOffset = 0;
    private static float yOffset = 0.5f;
    private static float zOffset = 0;

    /// <summary>
    /// Fires a series of raycasts in a horizontal fan shape.
    /// </summary>
    /// <param name="width">The width of the fan in degrees.</param>
    /// <param name="distance">The distance of each raycast.</param>
    /// <param name="numberOfRaycasts">The number of raycasts that make up the fan shape.</param>
    /// <param name="origin">The position the fan originates from.</param>
    /// <param name="direction">The direction the fan points.</param>
    /// <param name="toIgnore">The object to ignore while raycasting. Typically the animal that made the attack.</param>
    /// <returns>A List of players and projectiles hit by the attack.</returns>
    public static List<GameObject> Fan(float width, float distance, float numberOfRaycasts, Vector3 origin, Vector3 direction, GameObject toIgnore)
    {
        List<GameObject> hitObjects = new List<GameObject>();
        Vector3 positionOffset = origin;
        positionOffset.y += 1;
        Vector3 castDirection;
        RaycastHit hit;
        
        // raycast in a cone
        for (int i = 0; i < numberOfRaycasts; ++i)
        {
            castDirection = Quaternion.AngleAxis(-(width/2) + ((width / numberOfRaycasts+1) * i), Vector3.up) * direction;
            Debug.DrawRay(origin, castDirection * distance, Color.red, 0.1f);
            if (Physics.Raycast(positionOffset, castDirection, out hit, distance))
            {
                if ((hit.transform.tag == "Player" || hit.transform.tag == "projectile") && hit.transform.gameObject != toIgnore)
                {
                    if (!hitObjects.Contains(hit.transform.gameObject))
                    {
                        hitObjects.Add(hit.transform.gameObject);
                    }
                }
            }
        }

        return hitObjects;
    }

    /// <summary>
    /// Fires a series of raycasts in a horizontal fan shape.
    /// </summary>
    /// <param name="width">The width of the fan in degrees.</param>
    /// <param name="distance">The distance of each raycast.</param>
    /// <param name="numberOfRaycasts">The number of raycasts that make up the fan shape.</param>
    /// <param name="origin">The position the fan originates from.</param>
    /// <param name="offset">Offset from origin point. Set to Vector3.zero to use default.</param>
    /// <param name="direction">The direction the fan points.</param>
    /// <param name="toIgnore">The object to ignore while raycasting. Typically the animal that made the attack.</param>
    /// <returns>A List of players and projectiles hit by the attack.</returns>
    public static List<GameObject> Fan(float width, float distance, float numberOfRaycasts, Vector3 origin, Vector3 offset, Vector3 direction, GameObject toIgnore)
    {
        if(offset == Vector3.zero)
        {
            offset = new Vector3(xOffset, yOffset, zOffset);
        }
        return Fan(width, distance, numberOfRaycasts, origin + offset, direction, toIgnore);
    }
}
