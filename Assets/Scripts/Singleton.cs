using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Generic singleton design pattern. Have Singleton objects in the project extend the Singleton class

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private T instance;

    public T Instance {
        get {
            if (instance == null) {
                instance == FindObjectOfType<T>();
            } else {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
            return instance;
        }
    }
}
