using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Billboard Description:
 * Simple utility class that can be attached to a GameObject to continually face it towards the main camera.
 */

public class Billboard : MonoBehaviour
{
    // LateUpdate is called once per frame after other updates
    void LateUpdate()
    {
        gameObject.transform.rotation = Camera.main.transform.rotation;
    }
} 
