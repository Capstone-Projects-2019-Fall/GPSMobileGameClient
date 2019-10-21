using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Generic singleton design pattern. Have Singleton objects in the project extend the Singleton class
// Example of singleton definition: " public class NodeFactory : Singleton<NodeFactory>

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private T instance;

    public T Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<T>();
            } else {
                Debug.Log("An attempt was made to make another instance of an existing Singleton: " + gameObject.name);
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
            return instance;
        }
    }
}
