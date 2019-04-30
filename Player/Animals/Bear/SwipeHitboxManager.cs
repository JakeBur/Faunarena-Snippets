using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Hitbox behavior for the swipe attack.
/// </summary>
public class SwipeHitboxManager : MonoBehaviour
{
    private List<GameObject> hitPlayers = new List<GameObject>();// A list of players we've already hit with this swipe.

    private void OnTriggerEnter(Collider other)
    {
        if(hitPlayers.Contains(other.gameObject))
        {
            return;
        }
        print("hit " + other.gameObject);

        if (other.transform.tag == "Player")
        {
            hitPlayers.Add(other.gameObject);
        }

        Vector3 direction = GetComponentInParent<Swipe>().transform.right;
        if (GetComponentInParent<Swipe>().SwapDirection)
        {
            direction *= -1;
        }
        other.GetComponent<Launchable>().Launch(GetComponentInParent<Swipe>().swipeForce, direction);
    }

    /// <summary>
    /// Clears the list of players hit by this swipe.
    /// </summary>
    public void ClearHitPlayers()
    {
        hitPlayers.Clear();
    }
}
