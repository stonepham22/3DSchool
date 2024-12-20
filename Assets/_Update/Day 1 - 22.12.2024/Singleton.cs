using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base Singleton class for ensuring only one instance of a MonoBehaviour-derived class.
/// </summary>
/// <typeparam name="T">The type of the class inheriting from Singleton.</typeparam>
public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T _instance;

    /// <summary>
    /// Provides access to the singleton instance. Logs an error if the instance is not set.
    /// </summary>
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError(typeof(T) + " is not present in the scene!");
            }
            return _instance;
        }
    }

    /// <summary>
    /// Ensures only one instance of the singleton exists and persists across scenes.
    /// </summary>
    protected virtual void Awake()
    {
        // Check if an instance already exists and is not the current one.
        if (_instance != null && _instance != this)
        {
            // Logs a warning to notify about duplicate instances and destroys the new one.
            Debug.LogWarning("Multiple instances of " + typeof(T) + " found. Destroying duplicate.");

            // Destroy the duplicate GameObject to enforce the singleton pattern.
            Destroy(gameObject);
            return;
        }

        // Assign this instance as the singleton instance.
        _instance = (T)this;

        // Ensures the GameObject persists across scenes.
        DontDestroyOnLoad(gameObject);
    }
}
