using Mapbox.Unity.Location;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using SimpleJSON;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/*
 * SpawnManager handles populating Nodes on the overworld map. On start it repeats a query to get surrounding nodes and
 * displays those nodes on the map.
 */ 
public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] private AbstractMap _map;
    [SerializeField] private int _nodeQueryInSeconds = 10;
    [SerializeField] private float _spawnScale = 2f;
    [SerializeField] private int maxDistance = 2000;

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
        InvokeRepeating("QueryNodes", 1, NodeQueryInSeconds); // Calls QueryNodes() every "NodeQueryInSeconds" seconds 
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
                // Assume all currently populated nodes are not surrounding the player
                // and should be removed from the map after new nodes are created.
                List<GameObject> outOfBoundsNodes = new List<GameObject>(nodeLocations.Keys);

                for (int i = 0; i < jsonNode.Count; i++)
                {
                    string name = jsonNode[i]["name"];
                    string nodeStruct = jsonNode[i]["structure"];

                    // Check if a populated node with the same name already exists.
                    GameObject populatedNodeGameObject = getPopulatedNodeByName(name);                    

                    // There is an existing populated node.
                    if(populatedNodeGameObject != null)
                    {            
                        // The already populated node is still near the player
                        // so remove it from the list of out of bounds nodes.           
                        outOfBoundsNodes.Remove(populatedNodeGameObject);

                        Node populatedNode = populatedNodeGameObject.GetComponent<Node>();
                        //  Update the node's structure if it has changed since the last query.
                        // i.e. Friendly to Enemy
                        if(!populatedNode.NodeStruct.Type.Equals(nodeStruct))
                        {
                            populatedNode.NodeStruct = NodeFactory.GetNodeStructureByString(nodeStruct);
                        }                        
                    }
                    else // Create a new node
                    {
                        string nodeLongitude = jsonNode[i]["location"]["coordinates"][0];
                        string nodeLatitude = jsonNode[i]["location"]["coordinates"][1];
                        SpawnMarker(string.Format("{0}, {1}", nodeLatitude, nodeLongitude), nodeStruct, name);
                    }
                }

                // Only remove and destroy nodes that are no longer near the player.
                foreach(GameObject outOfBoundsNode in outOfBoundsNodes)
                {
                    nodeLocations.Remove(outOfBoundsNode);
                    Destroy(outOfBoundsNode);
                }
            }
        }, maxDistance));
    }

    /*
     * Spawns a node at a specified latitude and longitude location.
     * The locationString's format is "latitude, longitude"
     */
    public void SpawnMarker(string locationString, string nodeStruct, string name)
    {
        Vector2d latLon = Conversions.StringToLatLon(locationString);

        GameObject myNode = NodeFactory.nCreateNode(locationString, NodeFactory.GetNodeStructureByString(nodeStruct), name);
        // var instance = Instantiate(myNode);
        myNode.transform.localPosition = _map.GeoToWorldPosition(latLon, true);
        myNode.transform.localScale = _spawnScaleVector;

        nodeLocations.Add(myNode, latLon);
    }

    /*
     * Gets an already populated node GameObject based on the node's name.
     * Returns a GameObject or null if there is no existing node with that name.
     */
    public GameObject getPopulatedNodeByName(string name)
    {
        foreach (KeyValuePair<GameObject, Vector2d> node in nodeLocations)
        {
            Node populatedNode = node.Key.GetComponent<Node>();
            if(populatedNode.Name.Equals(name))
            {
                return node.Key;
            }
        }
        return null;
    }
}
