using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyGetReference 
{
    public static void Get<T>(Transform transform, ref T component) where T : Component
    {
        if (component != null) return;
        component = transform.GetComponent<T>();
        if (component != null) 
        { 
            Debug.Log(transform.name + " has " + typeof(T).Name); 
        }
        else 
        { 
            Debug.LogWarning(transform.name + " does not have " + typeof(T).Name); 
        }
    }    
}
