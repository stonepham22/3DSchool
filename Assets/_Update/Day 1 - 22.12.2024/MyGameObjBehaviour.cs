using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for managing components in custom MonoBehaviours.
/// </summary>
public abstract class MyGameObjBehaviour : MonoBehaviour
{
    private void Reset()
    {
        LoadComponents();
    }

    private void Awake()
    {
        LoadComponents();
    }

    /// <summary>
    /// Abstract method to load components, called in Awake and Reset.
    /// </summary>
    protected abstract void LoadComponents();
}
