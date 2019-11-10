using Mapbox.Unity.Map;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverworldController : Singleton<OverworldController>
{
    /// fields

    private AbstractMap _map;
    
    /// accessors
    public AbstractMap Map {
        get => _map;
    }

    //methods
    
    // Awake will be used to initialize static classes and unity singletons 
    private void Awake()
    {
        try
        {
            _map = (AbstractMap) FindObjectOfType(typeof(AbstractMap));
        } catch (Exception e) {
            Debug.Log("Unable to find an instance of an AbstractMap");
            // TODO: Configure and instantiate a new map
        }

        Debug.Log("OverworldCont: Initialize Static Classes.");
        // Static class initializations
        NodeFactory.InitializeFactory();
        GpsUtility.InitialzeUtility(_map);

    }

    // Start will validate that all necessary scene objects/classes have been instantiated/initialized
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {

    }

    /* SwitchScenes Description:
     * A wrapper method for the default Unity SceneManager.LoadScene(). This method should handle deconstructing
     * the overworld scene.
     */
    public void SwitchScenes(string path)
    {
        try
        {
            SceneManager.LoadScene(path);
        } catch (Exception e) {
            Debug.Log("Unable to load scene properly in OverworldController.SwitchScenes.");
            Debug.Log(e);
        }
    }
}
