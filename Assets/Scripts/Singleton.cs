using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Generic singleton design pattern. Have Singleton objects in the project extend the Singleton class
 * Example of singleton definition: " public class NodeUpdateService : Singleton<NodeUpdateService>
 * 
 * NOTE: This implementation of Singleton depends on an object being a MonoBehaviour, so concrete Singletons
 * cannot have constructors and must be attached to a GameObject in the Scene they participate in
 */

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private T instance;

    public T Instance {
        get {
            if (instance == null) { // If the Singleton has not yet been created.
                instance = FindObjectOfType<T>();
            } else {
                Debug.Log("An attempt was made to make another instance of an existing Singleton: " + gameObject.name);
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject); // Maintain the Singleton between Scene transitions
            return instance;
        }
    }
}
