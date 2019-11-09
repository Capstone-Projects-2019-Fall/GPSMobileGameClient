using Mapbox.Unity.Location;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using SimpleJSON;
using System;
using System.Collections.Generic;
using UnityEngine;

/*
 * SpawnManager handles populating Nodes on the overworld map. On start it repeats a query to get surrounding nodes and
 * displays those nodes on the map.
 */ 
public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] private AbstractMap _map;
    [SerializeField] private int _nodeQueryInSeconds = 10;
    [SerializeField] private float _spawnScale = 20f;
    private Vector3 _spawnScaleVector;

    private Dictionary<GameObject, Vector2d> nodeLocations;

    // accessors
    public AbstractMap Map 
    {
        get => _map;
        set => _map = value;
    }
    public int NodeQueryInSeconds 
    {
        get => _nodeQueryInSeconds;
        set => _nodeQueryInSeconds = value;
    }
    public float SpawnScale 
    {
        get => _spawnScale;
        set {
            try
            {
                _spawnScale = value;
                _spawnScaleVector = new Vector3(_spawnScale, _spawnScale, _spawnScale);
            }
            catch (Exception e) { Debug.Log(e); }
        }
    }

    void Start()
    {
        NodeFactory.InitializeFactory();
        InvokeRepeating("QueryNodes", 3, NodeQueryInSeconds); // Calls QueryNodes() every "NodeQueryInSeconds" seconds 
                                                              // starting in 3 seconds. The 3 seconds is to allow the game to load
                                                              // before making the first call.
        nodeLocations = new Dictionary<GameObject, Vector2d>();
        _spawnScaleVector = new Vector3(SpawnScale, SpawnScale, SpawnScale);
    }

    /*
     * Every frame the nodes must be updated in order to preserve their correct display on the map.
     */
    private void Update()
    {
        foreach (KeyValuePair<GameObject, Vector2d> node in nodeLocations)
        {
            Vector3 position = Map.GeoToWorldPosition(node.Value, true);
            position.y = 5;
            node.Key.transform.localPosition = position;
            node.Key.transform.localScale = _spawnScaleVector;
        }
    }

    /*
     * Uses the APIWrapper to get the surrounding nodes as a JSONNode object.
     * This method is called repeatedly throughout the game's runtime.
     */
    private void QueryNodes()
    {
        Vector2d latLon = LocationProviderFactory.Instance.DefaultLocationProvider.CurrentLocation.LatitudeLongitude;

        // The third argument is an anonymous callback function that handles the received nodes.
        StartCoroutine(APIWrapper.getSurroundingNodes(latLon.x, latLon.y, (jsonNode) => {
            if (jsonNode != null)
            {
                SpawnNodes(jsonNode);
            }
        }));
    }

    /*
     * Takes a JSONNode object representing a list of surrounding nodes and spawns each one.
     * Before spawning new nodes, all currently populated nodes are removed from the overworld map
     * and replaced with the new set of surrounding nodes.
     */
    public void SpawnNodes(JSONNode jsonNode)
    {

        foreach (KeyValuePair<GameObject, Vector2d> node in nodeLocations)
        {
            Destroy(node.Key);
        }

        nodeLocations.Clear();

        for (int i = 0; i < jsonNode.Count; i++)
        {
            string nodeLongitude = jsonNode[i]["location"]["coordinates"][0];
            string nodeLatitude = jsonNode[i]["location"]["coordinates"][1];
            SpawnMarker(string.Format("{0}, {1}", nodeLatitude, nodeLongitude));
        }
    }

    /*
     * Spawns a node at a specified latitude and longitude location.
     * The locationString's format is "latitude, longitude"
     */
    public void SpawnMarker(string locationString)
    {
        Vector2d latLon = Conversions.StringToLatLon(locationString);

        // TODO: Get NodeStructure from API (currently calling random Node)
        GameObject myNode = NodeFactory.nCreateNode(locationString);
        // var instance = Instantiate(myNode);
        myNode.transform.localPosition = _map.GeoToWorldPosition(latLon, true);
        myNode.transform.localScale = _spawnScaleVector;

        nodeLocations.Add(myNode, latLon);
    }
}
