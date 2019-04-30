 using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Any object that needs to be launched by an attack or a hazard should extend this class.
/// </summary>
public abstract class Launchable : MonoBehaviour
{
    public abstract void Launch(float force, Vector3 direction);
}
