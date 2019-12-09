using Mapbox.Unity.Map;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverworldController : Singleton<OverworldController>
{
    /// fields
    private SpawnManager _mySpawnManager;
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

        _mySpawnManager = transform.Find("Spawn Manager").GetComponent<SpawnManager>();
        // Static class initializations
        NodeFactory.InitializeFactory();
        GpsUtility.InitialzeUtility(_map);

    }

    // Start will validate that all necessary scene objects/classes have been instantiated/initialized
    private void Start()
    {
        // Spawn the home base marker
        StartCoroutine(APIWrapper.getPlayer(PlayerPrefs.GetString(Player.usernameKey, "Bart"), (jsondata) => {
            string lon = jsondata["homebase"]["coordinates"][0];
            string lat = jsondata["homebase"]["coordinates"][1];
            string latLon = string.Format("{0}, {1}", lat, lon);
            Debug.Log(latLon);
            _mySpawnManager.SpawnHomeBase(latLon, "HomeBase", "HomeBase");
            }));
        
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
