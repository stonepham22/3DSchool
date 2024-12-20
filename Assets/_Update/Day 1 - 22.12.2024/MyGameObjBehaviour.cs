using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    protected abstract void LoadComponents();
}
