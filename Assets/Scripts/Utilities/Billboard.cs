using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Billboard Description:
 * Simple utility class that can be attached to a GameObject to continually face it towards the main camera.
 */

public class Billboard : MonoBehaviour
{
    public Camera _camera;

    public void Start()
    {
        _camera = Camera.main;
    }

    // LateUpdate is called once per frame after other updates
    void LateUpdate()
    {
        transform.LookAt(transform.position + _camera.transform.rotation * Vector3.forward, _camera.transform.rotation * Vector3.up);
    }
} 
